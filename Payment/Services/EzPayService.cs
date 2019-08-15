using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Payment.Services
{
    public class EzPayInvoiceService : PaymentServiceBase
    {
        public EzPayInvoiceService(int environment = 0) : base(environment)
        {
        }

        public EzPayInvoiceIssueResponse IssueInvoice(EzPayInvoiceIssueInfo ezPayInvoiceIssueInfo)
        {
            ezPayInvoiceIssueInfo.Version = string.IsNullOrEmpty(ezPayInvoiceIssueInfo.Version) ? _configService.EzPayInvoiceIssueVersion : ezPayInvoiceIssueInfo.Version;
            EzPayInvoiceIssueResponse response = null;

            var request = new EzPayInvoiceRequest()
            {
                MerchantID_ = _configService.EzPayMerchantId,
                PostData_ = HashService.EncryptAESHex(ezPayInvoiceIssueInfo.GetDataString(), _configService.EzPayHashKey, _configService.EzPayHashIv)
            };

            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>(nameof(request.MerchantID_), request.MerchantID_),
                new KeyValuePair<string, string>(nameof(request.PostData_), request.PostData_)
            });

            string responseBody = HttpService.PostForm(_configService.EzPayInvoiceIssue, formContent);
            if (!string.IsNullOrEmpty(responseBody))
            {
                var deserializeObj = JsonConvert.DeserializeObject<JObject>(responseBody);
                response = new EzPayInvoiceIssueResponse()
                {
                    Message = deserializeObj.GetValue("Message").ToString(),
                    Status = deserializeObj.GetValue("Status").ToString()
                };

                if (response.IsSucceed)
                {
                    response.Result = JsonConvert.DeserializeObject<EzPayInvoiceIssueResult>(deserializeObj.GetValue("Result").ToString());

                    var checkcode = GetCheckCode(response.Result.GetCheckCodeInfo(), _configService.EzPayHashKey, _configService.EzPayHashIv);
                    if (!Equals(response.Result.CheckCode, checkcode))
                    {
                        throw new Exception("CheckCode 檢驗失敗");
                    }
                }
            }

            return response;
        }

        public EzPayInvoiceInvalidResponse InvalidInvoice(ref EzPayInvoiceInvalidInfo ezPayInvoiceInvalidInfo)
        {
            ezPayInvoiceInvalidInfo.Version = string.IsNullOrEmpty(ezPayInvoiceInvalidInfo.Version) ? _configService.EzPayInvoiceInvalidVersion : ezPayInvoiceInvalidInfo.Version;
            ezPayInvoiceInvalidInfo.RespondType = "JSON";
            ezPayInvoiceInvalidInfo.TimeStamp = UnixDateTimeService.GetUNIX(DateTime.Now).ToString();

            EzPayInvoiceInvalidResponse response = null;

            var request = new EzPayInvoiceRequest()
            {
                MerchantID_ = _configService.EzPayMerchantId,
                PostData_ = HashService.EncryptAESHex(ezPayInvoiceInvalidInfo.GetDataString(), _configService.EzPayHashKey, _configService.EzPayHashIv)
            };

            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>(nameof(request.MerchantID_), request.MerchantID_),
                new KeyValuePair<string, string>(nameof(request.PostData_), request.PostData_)
            });

            string responseBody = HttpService.PostForm(_configService.EzPayInvoiceInvalid, formContent);
            if (!string.IsNullOrEmpty(responseBody))
            {
                var deserializeObj = JsonConvert.DeserializeObject<JObject>(responseBody);
                response = new EzPayInvoiceInvalidResponse()
                {
                    Message = deserializeObj.GetValue("Message").ToString(),
                    Status = deserializeObj.GetValue("Status").ToString(),
                };

                if (response.IsSucceed)
                {
                    response.Result = JsonConvert.DeserializeObject<EzPayInvoiceInvalidResult>(deserializeObj.GetValue("Result").ToString());
                }
            }

            return response;
        }

        public EzPayInvoiceInvalidResponse InvalidInvoice(ref EzPayInvoiceInvalidInfo ezPayInvoiceInvalidInfo, string checkcode)
        {
            var response = InvalidInvoice(ref ezPayInvoiceInvalidInfo);
            if (!Equals(response.Result.CheckCode, checkcode))
            {
                throw new Exception("CheckCode 比對失敗");
            }

            return response;
        }

        public EzPayInvoiceAllowanceResponse AllowanceIssueInvoice(ref EzPayInvoiceAllowanceIssueInfo ezPayInvoiceAllowanceIssueInfo)
        {
            ezPayInvoiceAllowanceIssueInfo.Version = string.IsNullOrEmpty(ezPayInvoiceAllowanceIssueInfo.Version) ? _configService.EzPayInvoiceAllowanceIssueVersion : ezPayInvoiceAllowanceIssueInfo.Version;
            ezPayInvoiceAllowanceIssueInfo.RespondType = "JSON";
            ezPayInvoiceAllowanceIssueInfo.TimeStamp = UnixDateTimeService.GetUNIX(DateTime.Now).ToString();

            EzPayInvoiceAllowanceResponse response = null;

            var request = new EzPayInvoiceRequest()
            {
                MerchantID_ = _configService.EzPayMerchantId,
                PostData_ = HashService.EncryptAESHex(ezPayInvoiceAllowanceIssueInfo.GetDataString(), _configService.EzPayHashKey, _configService.EzPayHashIv)
            };

            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>(nameof(request.MerchantID_), request.MerchantID_),
                new KeyValuePair<string, string>(nameof(request.PostData_), request.PostData_)
            });

            string responseBody = HttpService.PostForm(_configService.EzPayInvoiceAllowanceIssue, formContent);
            if (!string.IsNullOrEmpty(responseBody))
            {
                var deserializeObj = JsonConvert.DeserializeObject<JObject>(responseBody);
                response = new EzPayInvoiceAllowanceResponse()
                {
                    Message = deserializeObj.GetValue("Message").ToString(),
                    Status = deserializeObj.GetValue("Status").ToString()
                };

                if (response.IsSucceed)
                {
                    response.Result = JsonConvert.DeserializeObject<EzPayInvoiceAllowanceResult>(deserializeObj.GetValue("Result").ToString());
                }
            }

            return response;
        }

        public EzPayInvoiceAllowanceResponse AllowanceIssueInvoice(ref EzPayInvoiceAllowanceIssueInfo ezPayInvoiceAllowanceIssueInfo, string checkcode)
        {
            var response = AllowanceIssueInvoice(ref ezPayInvoiceAllowanceIssueInfo);
            if (!Equals(response.Result.CheckCode, checkcode))
            {
                throw new Exception("CheckCode 比對失敗");
            }

            return response;
        }

        private string GetCheckCode(string checkCodeInfoString, string hashKey, string hashIv)
        {
            return HashService.EncryptSHA256($"HashIV={hashIv}&{checkCodeInfoString}&HashKey={hashKey}");
        }
    }
}
