using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.Framework.Utils;
using Winner.OAuth.Entities;
using Winner.OAuth.Facade;

namespace Winner.OAuth.GrpcService.Controllers
{
    public class SmsService : Sms.SmsBase
    {
        public override Task<SmsSendResponseDTO> Send(SmsSendRequestDTO request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                SmsSendResponseDTO response = new SmsSendResponseDTO();
                Log.Info("UserCode={0}&PWDTYPE={1}", request.MobileNo, request.SmsType);
                SmsValidateProvider valid = new SmsValidateProvider(request.MobileNo, (SmsValidateType)request.SmsType);
                if (!valid.SendCode())
                {
                    response.RetCode = "0500";
                    response.RetMsg = valid.PromptInfo.CustomMessage;
                    return response;
                }
                response.RetCode = "0000";
                response.RetMsg = "ok";
                return response;
            });
        }
    }
}
