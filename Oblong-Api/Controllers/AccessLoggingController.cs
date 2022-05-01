using Microsoft.AspNetCore.Mvc;
using Oblong_Api.Data;
using Oblong_Api.Models;
using System.Net;

namespace Oblong_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessLoggingController : ControllerBase
    {
        private readonly PersonalDbContext _dbContext;

        public AccessLoggingController(PersonalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static readonly string[] accessTypes = new[]
        {
            "enter", "exit"
        };
        [HttpGet]
        public int Get(string? siteName = null)
        {
            if (String.IsNullOrEmpty(siteName))
            {
                return _dbContext.SiteAccesses.Count();
            }
            else
            {
                return _dbContext.SiteAccesses.Where(el => el.SiteName == siteName).Count();
            }
        }

        [HttpPost(Name ="AccessSite")]
        public string Post(string? token = null)
        {

            HttpContext context = this.HttpContext;
            //enter
            if(String.IsNullOrEmpty(token))
            {
                //string ip = context.Connection.RemoteIpAddress;

                SiteAccess access = new SiteAccess();

                IPAddress ip = context.Connection.RemoteIpAddress;
                Uri? referer = Request.GetTypedHeaders().Referer;
                if (referer != null)
                {
                    access.SiteName = referer.ToString();
                }

                access.IP = ip.ToString();
                access.TimeEntered = DateTime.Now;
                access.Token = GenerateToken();

                _dbContext.SiteAccesses.Add(access);
                _dbContext.SaveChanges();

                return access.Token;

            }
            else
            {
                SiteAccess access = new SiteAccess();

                IPAddress ip = context.Connection.RemoteIpAddress;
                Uri? referer = Request.GetTypedHeaders().Referer;

                if (referer != null)
                {
                    access.SiteName = referer.ToString();
                }

                access.IP = ip.ToString();

                SiteAccess fromDb = _dbContext.SiteAccesses.Where(el =>
                    el.IP == access.IP && el.SiteName == access.SiteName && el.Token == token && el.TimeExited == null
                ).FirstOrDefault();

                if (fromDb != null)
                {
                    fromDb.TimeExited = DateTime.Now;

                    _dbContext.Update(fromDb);
                    _dbContext.SaveChanges();
                }




                return "On Exit";
            }
        }

        public string GenerateToken()
        {
            using var csprng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var bytes = new byte[16];

            csprng.GetNonZeroBytes(bytes);

            string token = string.Join("", bytes.Select(b => b.ToString("x2")));

            SiteAccess fromDb = _dbContext.SiteAccesses.Where(el => el.Token == token).FirstOrDefault();

            if (fromDb == null)
            {
                return token;
            }
            else
            {
                return GenerateToken();
            }
        }
    }
}
