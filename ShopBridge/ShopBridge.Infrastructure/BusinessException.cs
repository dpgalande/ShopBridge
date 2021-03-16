using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Infrastructure
{
    public class BusinessException : Exception
    {
        public string Code { get; set; }
        public string MessageCategory { get; set; }
        public string MessageProperty { get; set; }
        public bool IsSuccessMsg { get; set; }
        public string CustomMessage { get; set; }

        public BusinessException(string msgCategory, string msgProperty, string code, bool isSuccessMsg = false)
        {
            MessageCategory = msgCategory;
            MessageProperty = msgProperty;
            Code = code;
            IsSuccessMsg = isSuccessMsg;
        }
        public BusinessException(string message)
        {
            CustomMessage = message;
        }
    }
}
