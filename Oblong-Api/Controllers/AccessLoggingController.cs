using Microsoft.AspNetCore.Mvc;
using Oblong_Api.Models;
using System.Net;

namespace Oblong_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessLoggingController : ControllerBase
    {
        public static readonly string[] accessTypes = new[]
        {
            "enter", "exit"
        };

        [HttpPost(Name ="AccessSite")]
        public string Post(int accessType, string? token = null)
        {
            HttpContext context = this.HttpContext;
            //enter
            if(accessType == 0)
            {
                //string ip = context.Connection.RemoteIpAddress;
                IPAddress ip = context.Connection.RemoteIpAddress;
                var x = 0;
                SiteAccess access = new SiteAccess();
                return "test-token";

            }//exit
            else if(accessType == 1)
            {
                return "test-token";
            }
            else
            {
                //?
                return "test-token";
            }
        }
    }
}
