using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Winner.Framework.Utils.Model;

namespace Winner.OAuth.Api.Models
{
    public class OAuthControllerBase : ControllerBase
    {
        protected ResponseResult Success()
        {
            return new ResponseResult { RetCode = "0000", RetMsg = "ok" };
        }
        protected ResponseResult<T> Success<T>(T data)
        {
            return new ResponseResult<T> { RetCode = "0000", RetMsg = "ok", Data = data };
        }
        protected ResponseResult<T> Fail<T>(string message, string code = "0500")
        {
            return new ResponseResult<T> { RetCode = code, RetMsg = message };
        }
        protected ResponseResult Fail(string message, string code = "0500")
        {
            return new ResponseResult { RetCode = code, RetMsg = message };
        }
        protected ResponseResult FromFuncResult(FuncResult result)
        {
            int code = result.StatusCode;
            if (!result.Success && result.StatusCode == 0)
            {
                code = 500;
            }
            return new ResponseResult { RetCode = code.ToString().PadLeft(4, '0'), RetMsg = result.Message };
        }
        protected ResponseResult<T> FromFuncResult<T>(FuncResult<T> result)
        {
            int code = result.StatusCode;
            if (!result.Success && result.StatusCode == 0)
            {
                code = 500;
            }
            return new ResponseResult<T> { RetCode = code.ToString().PadLeft(4, '0'), RetMsg = result.Message, Data = result.Content };
        }
    }
}
