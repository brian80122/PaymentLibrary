using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class BasicInfo
    {
        public string GetDataString()
        {
            var properties = GetType().GetProperties();
            var results = properties.Where(c => c.GetValue(this) != null)
                                    .Select(c => $"{c.Name}={c.GetValue(this)}");
            var resultString = string.Join("&", results);
            return resultString;
        }
    }
}
