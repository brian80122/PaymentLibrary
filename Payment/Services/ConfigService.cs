using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Services
{
    internal class ConfigService
    {
        //測試環境=0, 正式環境=1
        private int _environment = 0;

        internal ConfigService(int environment)
        {
            _environment = environment;
        }

        internal string NewebpayMpgGateway
        {
            get
            {
                return GetValue(nameof(NewebpayMpgGateway));
            }
        }

        internal string NewebpayCreditCardApi
        {
            get
            {
                return GetValue(nameof(NewebpayCreditCardApi));
            }
        }

        internal string NewebpayQueryTradeInfoApi
        {
            get
            {
                return GetValue(nameof(NewebpayQueryTradeInfoApi));
            }
        }

        internal string HashKey
        {
            get
            {
                return GetValue(nameof(HashKey));
            }
        }

        internal string HashIv
        {
            get
            {
                return GetValue(nameof(HashIv));
            }
        }

        internal string MpgVersion
        {
            get
            {
                return GetValue(nameof(MpgVersion));
            }
        }

        internal string CreditCardCloseVersion
        {
            get
            {
                return GetValue(nameof(CreditCardCloseVersion));
            }
        }

        internal string QueryTradeInfoVersion
        {
            get
            {
                return GetValue(nameof(QueryTradeInfoVersion));
            }
        }

        internal string MerchantId
        {
            get
            {
                return GetValue(nameof(MerchantId));
            }
        }
        
        internal string EzPayInvoiceIssue
        {
            get
            {
                return GetValue(nameof(EzPayInvoiceIssue));
            }
        }

        internal string EzPayInvoiceIssueVersion
        {
            get
            {
                return GetValue(nameof(EzPayInvoiceIssueVersion));
            }
        }

        internal string EzPayInvoiceInvalid
        {
            get
            {
                return GetValue(nameof(EzPayInvoiceInvalid));
            }
        }

        internal string EzPayInvoiceInvalidVersion
        {
            get
            {
                return GetValue(nameof(EzPayInvoiceInvalidVersion));
            }
        }

        internal string EzPayInvoiceAllowanceIssue
        {
            get
            {
                return GetValue(nameof(EzPayInvoiceAllowanceIssue));
            }
        }

        internal string EzPayInvoiceAllowanceIssueVersion
        {
            get
            {
                return GetValue(nameof(EzPayInvoiceAllowanceIssueVersion));
            }
        }

        internal string EzPayHashKey
        {
            get
            {
                return GetValue(nameof(EzPayHashKey));
            }
        }

        internal string EzPayHashIv
        {
            get
            {
                return GetValue(nameof(EzPayHashIv));
            }
        }

        internal string EzPayMerchantId
        {
            get
            {
                return GetValue(nameof(EzPayMerchantId));
            }
        }

        public string GetValue(string key)
        {
            var processedKey = $"{(_environment == 0 ? "Test" : "Prod")}{key}";
            string result = string.Empty;
            var uri = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase));
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = Path.Combine(uri.LocalPath, Assembly.GetExecutingAssembly().FullName.Split(',')[0] + ".dll.config") };
            var assemblyConfig = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            if (assemblyConfig.HasFile)
            {
                AppSettingsSection section = (assemblyConfig.GetSection("appSettings") as AppSettingsSection);
                result = section.Settings[processedKey].Value;
            }

            return result;
        }
    }
}
