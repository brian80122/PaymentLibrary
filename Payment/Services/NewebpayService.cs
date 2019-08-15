using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Payment.Services
{
    // ref https://harry-lin.blogspot.com/2019/01/c-spgateway.html
    public class NewebpayService: PaymentServiceBase
    {
        public NewebpayService(int environment = 0) : base(environment)
        {

        }

        public string GetMerchantId()
        {
            return _configService.MerchantId;
        }

        public NewebpayQueryTradeInfoResult QueryTradeInfo(string merchantOrderNo, int amt)
        {
            var postData = new NewebpayQueryTradeInfoInfo()
            {
                MerchantID = _configService.MerchantId,
                RespondType = "JSON",
                Version = _configService.QueryTradeInfoVersion,
                TimeStamp = UnixDateTimeService.GetUNIX(DateTime.Now).ToString(),
                MerchantOrderNo = merchantOrderNo,
                Amt = amt
            };
            postData.CheckValue = GetQueryTradeInfoCheckValue($"{nameof(postData.Amt)}={postData.Amt}&{nameof(postData.MerchantID)}={postData.MerchantID}&{nameof(postData.MerchantOrderNo)}={postData.MerchantOrderNo}",
                _configService.HashKey, _configService.HashIv);

            return DoPostQueryTradeInfo(postData);
        }


        public NewebpayQueryTradeInfoResult DoPostQueryTradeInfo(NewebpayQueryTradeInfoInfo info)
        {
            NewebpayQueryTradeInfoResult result = null;
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>(nameof(info.MerchantID), info.MerchantID),
                new KeyValuePair<string, string>(nameof(info.RespondType), info.RespondType),
                new KeyValuePair<string, string>(nameof(info.Version), info.Version),
                new KeyValuePair<string, string>(nameof(info.TimeStamp), info.TimeStamp),
                new KeyValuePair<string, string>(nameof(info.MerchantOrderNo), info.MerchantOrderNo),
                new KeyValuePair<string, string>(nameof(info.Amt), info.Amt.ToString()),
                new KeyValuePair<string, string>(nameof(info.CheckValue), info.CheckValue)
            });

            string responseBody = HttpService.PostForm(_configService.NewebpayQueryTradeInfoApi, formContent);
            if (!string.IsNullOrWhiteSpace(responseBody))
            {
                var response = JsonConvert.DeserializeObject<NewebpayQueryTradeInfoResponse>(responseBody);

                if (response.IsSucceed)
                {
                    result = response.Result;
                }
                else
                {
                    throw new Exception($"查詢失敗");
                }
            }

            return result;
        }

        /// <summary>
        /// 進行信用卡請退款
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <param name="amt"></param>
        /// <param name="closeType">請款=1, 退款=2</param>
        /// <returns></returns>
        public NewebPayCreditCardCloseResponse CreditCardCloseByTradeNo(string tradeNo, int amt,int closeType)
        {
            var postData = new NewebpayCreditCardClosePostDataInfo()
            {
                TradeNo = tradeNo,
                Version = _configService.CreditCardCloseVersion,
                Amt = amt,
                CloseType = closeType,
                IndexType = 2,
                RespondType = "JSON",
                TimeStamp = UnixDateTimeService.GetUNIX(DateTime.Now).ToString(),
                MerchantOrderNo = ""
            };
            return DoCreditCardClose(postData);
        }

        /// <summary>
        /// 進行信用卡請退款
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <param name="amt"></param>
        /// <param name="closeType">請款=1, 退款=2</param>
        /// <returns></returns>
        public NewebPayCreditCardCloseResponse CreditCardCloseByMerchantOrderNo(string merchantOrderNo, int amt, int closeType)
        {
            var postData = new NewebpayCreditCardClosePostDataInfo()
            {
                TradeNo = "",
                Version = _configService.CreditCardCloseVersion,
                Amt = amt,
                CloseType = closeType,
                IndexType = 1,
                RespondType = "JSON",
                TimeStamp = UnixDateTimeService.GetUNIX(DateTime.Now).ToString(),
                MerchantOrderNo = merchantOrderNo
            };
            return DoCreditCardClose(postData);
        }

        public NewebPayCreditCardCloseResponse DoCreditCardClose(NewebpayCreditCardClosePostDataInfo postData)
        {
            var request = ProcessCreditCardClosePostData(postData);
            NewebPayCreditCardCloseResponse result = null;
            FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>(nameof(request.MerchantID_), request.MerchantID_),
                new KeyValuePair<string, string>(nameof(request.PostData_), request.PostData_)
            });

            string responseBody = HttpService.PostForm(_configService.NewebpayCreditCardApi, formContent);

            if (!string.IsNullOrWhiteSpace(responseBody))
            {
                var response = JsonConvert.DeserializeObject<NewebPayCreditCardCloseResponse>(responseBody);

                if (response.Status != null && string.Equals(response.Status, "SUCCESS"))
                {
                    result = response;
                }
                else
                {
                    throw new Exception($"請款或退款失敗, response: {JsonConvert.SerializeObject(response)}");
                }
            }

            return result;
        }

        private NewebpayBasicRequest ProcessCreditCardClosePostData(NewebpayCreditCardClosePostDataInfo postData)
        {
            var request = new NewebpayBasicRequest()
            {
                MerchantID_ = _configService.MerchantId,
                PostData_ = HashService.EncryptAESHex(postData.GetDataString(), _configService.HashKey, _configService.HashIv)
            };
            return request;
        }

        public NewebpayRequest ProcessTradeInfo(NewebpayTradeInfo tradeInfo)
        {
            tradeInfo.MerchantID = string.IsNullOrEmpty(tradeInfo.MerchantID) ? _configService.MerchantId : tradeInfo.MerchantID;
            tradeInfo.Version = string.IsNullOrEmpty(tradeInfo.Version) ? _configService.MpgVersion : tradeInfo.Version;
            var tradeInfoString = tradeInfo.GetDataString();
            var hashKey = _configService.HashKey;
            var hashIv = _configService.HashIv;
            var aesTradeInfoString = HashService.EncryptAESHex(tradeInfoString, hashKey, hashIv);
            var request = new NewebpayRequest()
            {
                MerchantID = tradeInfo.MerchantID,
                TradeInfo = aesTradeInfoString,
                TradeSha = GetTradeSha(aesTradeInfoString, hashKey, hashIv),
                Version = tradeInfo.Version
            };

            return request;
        }

        private string GetTradeSha(string aesTradeInfoString, string hashKey, string hashIv)
        {
            return HashService.EncryptSHA256($"HashKey={hashKey}&{aesTradeInfoString}&HashIV={hashIv}");
        }

        private string GetQueryTradeInfoCheckValue(string queryTradeInfo, string hashKey, string hashIv)
        {
            return HashService.EncryptSHA256($"IV={hashIv}&{queryTradeInfo}&Key={hashKey}");
        }

        public string ProcessTradeInfoGetPostTotalPageFormString(NewebpayTradeInfo tradeInfo)
        {
            var request = ProcessTradeInfo(tradeInfo);
            StringBuilder s = new StringBuilder();
            s.Append("<html>");
            s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
            s.AppendFormat("<form name='form' action='{0}' method='post'>", _configService.NewebpayMpgGateway);
            foreach (var prop in request.GetType().GetProperties())
            {
                s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", prop.Name, prop.GetValue(request));
            }

            s.Append("</form></body></html>");

            return s.ToString();
        }

        public string ProcessTradeInfoGetPostFormString(NewebpayTradeInfo tradeInfo)
        {
            var request = ProcessTradeInfo(tradeInfo);
            StringBuilder s = new StringBuilder();
            s.AppendFormat("<form name='form' action='{0}' method='post'>", _configService.NewebpayMpgGateway);
            foreach (var prop in request.GetType().GetProperties())
            {
                s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", prop.Name, prop.GetValue(request));
            }

            s.Append("</form>");

            return s.ToString();
        }

        public NewebpayTakeNumberResponse DecryptNewebPayResponseForTakeNumberResonse(NewebpayResponse newebpayResponse)
        {
            var compareString = GetTradeSha(newebpayResponse.TradeInfo, _configService.HashKey, _configService.HashIv);
            if (!string.Equals(compareString, newebpayResponse.TradeSha))
            {
                throw new Exception("SHA 檢核失敗");
            }

            var decryptString = HashService.DecryptAESHex(newebpayResponse.TradeInfo, _configService.HashKey, _configService.HashIv);
            var takeNumberResponse = JsonConvert.DeserializeObject<NewebpayTakeNumberResponse>(decryptString);
            return takeNumberResponse;
        }

        public NewebpayPaymentResponse DecryptNewebPayResponseForPaymentResonse(NewebpayResponse newebpayResponse)
        {
            var compareString = GetTradeSha(newebpayResponse.TradeInfo, _configService.HashKey, _configService.HashIv);
            if (!string.Equals(compareString, newebpayResponse.TradeSha))
            {
                throw new Exception("SHA 檢核失敗");
            }

            var decryptString = HashService.DecryptAESHex(newebpayResponse.TradeInfo, _configService.HashKey, _configService.HashIv);
            var paymentResponse = JsonConvert.DeserializeObject<NewebpayPaymentResponse>(decryptString);
            return paymentResponse;
        }
    }
}
