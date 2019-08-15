using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Models
{
    public class NewebpayTakeNumberResponse: BasicResponse
    {
        public NewebpayTakeNumberResult Result { get; set; }
    }
}
