using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class NewebpayQueryTradeInfoResult
    {
        public string MerchantID { get; set; }
        public string Amt { get; set; }
        public string TradeNo { get; set; }
        public string MerchantOrderNo { get; set; }

        /// <summary>
        /// 0=未付款
        /// 1=付款成功
        /// 2=付款失敗
        /// 3=取消付款
        /// </summary>
        public int TradeStatus { get; set; }

        /// <summary>
        /// CREDIT=信用卡付款
        /// VACC=銀行 ATM 轉帳付款
        /// WEBATM = 網路銀行轉帳付款
        /// BARCODE=超商條碼繳費
        /// CVS = 超商代碼繳費
        /// </summary>
        public string PaymentType { get; set; }
        public DateTime? CreateTime { get;set;}
        public DateTime? PayTime { get; set; }
        public string CheckCode { get; set; }

        /// <summary>
        /// 預計撥款日
        /// </summary>
        public string FundTime { get; set; }


        #region 信用卡專屬欄位
        /// <summary>
        /// 金融機構回應碼       
        /// 1.由金融機構所回應的回應碼
        /// 2.若交易送至金融機構授權時已是失敗狀態，則本欄位的值會以空值回傳。
        /// </summary>
        public string RespondCode { get; set; }

        /// <summary>
        /// 授權碼
        /// 1.由金融機構所回應的回應碼
        /// 2.若交易送至金融機構授權時已是失敗狀態，則本欄位的值會以空值回傳。
        /// </summary>
        public string Auth { get; set; }

        /// <summary>
        /// ECI
        /// 1.3D 回傳值 eci=1,2,5,6，代表為 3D 交易。
        /// 2.若交易送至金融機構授權時已是失敗狀態，則本欄位的值會以空值回傳。
        /// </summary>
        public string ECI { get; set; }

        /// <summary>
        /// 請款金額 
        /// 此筆交易設定的請款金額
        /// </summary>
        public int? CloseAmt { get; set; }

        /// <summary>
        /// 請款狀態
        /// 0=未請款
        /// 1=等待提送請款至收單機構
        /// 2=請款處理中
        /// 3=請款完成
        /// </summary>
        public int CloseStatus { get; set; }

        /// <summary>
        /// 可退款餘額
        /// 此筆交易尚可退款餘額，若此筆交易未請款則此處金額為 0
        /// </summary>
        public int? BackBalance { get; set; }
        /// <summary>
        /// 退款狀態
        /// 0=未退款
        /// 1=等待提送退款至收單機構
        /// 2=退款處理中
        /// 3=退款完成
        /// </summary>
        public int BackStatus { get; set; }

        /// <summary>
        /// 授權結果訊息
        /// 文字，銀行回覆此次信用卡授權結果狀態。
        /// </summary>
        public string RespondMsg { get; set; }

        /// <summary>
        /// 分期-期別
        /// 信用卡分期交易期別。
        /// </summary>
        public int Inst { get; set; }

        /// <summary>
        /// 分期-首期金額
        /// 信用卡分期交易首期金額。
        /// </summary>
        public int InstFirst { get; set; }

        /// <summary>
        /// 分期-每期金額
        /// 信用卡分期交易每期金額。
        /// </summary>
        public int InstEach { get; set; }

        /// <summary>
        /// 交易類別
        /// CREDIT = 台灣發卡機構核發之信用卡
        /// FOREIGN = 國外發卡機構核發之卡
        /// NTCB = 國民旅遊卡
        /// UNIONPAY = 銀聯卡
        /// APPLEPAY = ApplePay
        /// GOOGLEPAY = GooglePay
        /// SAMSUNGPAY = SamsungPay
        /// </summary>
        public string PaymentMethod { get; set; }
        #endregion


        #region 超商代碼、超商條碼、ATM 轉帳專屬欄位
        /// <summary>
        /// 付款資訊
        /// 1.付款方式為超商代碼(CVS)時，此欄位為超商繳款代碼。
        /// 2.付款方式為條碼(BARCODE)時，此欄位為繳款條
        /// 碼。此欄位會將三段條碼資訊用逗號”,”組合後回傳。
        /// 3.付款方式為 ATM 轉帳時，此欄位為金融機構的轉
        /// 帳帳號，括號內為金融機構代碼，例：(031)1234567890。
        /// </summary>
        public string PayInfo { get; set; }

        /// <summary>
        /// 繳費有效期限
        /// 1.格式為 Y-m-d H:i:s 。
        /// 例：2014-06-29 23:59:59。
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        #endregion
    }
}
