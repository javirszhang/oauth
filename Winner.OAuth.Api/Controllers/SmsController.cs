using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.OAuth.Api.Models;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade;

namespace Winner.OAuth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : OAuthControllerBase
    {
        [HttpPost]
        public ResponseResult Send(SmsSendRequestDTO request)
        {
            Log.Info("UserCode={0}&PWDTYPE={1}", request.MobileNo, request.SmsType);
            SmsValidateProvider valid = new SmsValidateProvider(request.MobileNo, (SmsValidateType)request.SmsType);
            if (!valid.SendCode())
            {
                return Fail(valid.PromptInfo.CustomMessage);
            }
            return Success();
        }
    }
}
