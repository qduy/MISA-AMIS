using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiSA.Fresher.Amis.Core.Exceptions
{
    /// <summary>
    /// Lấy ra các lỗi exception
    /// </summary>
    /// CreatedBy: DQDUY (20/12/2021)
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException httpResponseException)
            {
                // result để chứa thông báo lỗi
                var result = new
                {
                    devMsg = Properties.VNResources.BadRequest,
                    userMgs = Properties.VNResources.ExceptionError,
                    data = httpResponseException.Value,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = 400
                };

                context.ExceptionHandled = true;
            }
            else if(context.Exception != null)
            {
                var result = new
                {
                    devMsg = Properties.VNResources.Internal_Server_Error,
                    userMgs = Properties.VNResources.ExceptionError,
                    data = DBNull.Value,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = 500
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
