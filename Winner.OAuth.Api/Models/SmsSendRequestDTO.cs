using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.OAuth.Entities;

namespace Winner.OAuth.Api.Models
{
    public class SmsSendRequestDTO
    {
        public string MobileNo { get; internal set; }
        public SmsValidateType SmsType { get; internal set; }
    }
}
