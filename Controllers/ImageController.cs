//using System;
//using System.Collections.Generic;
using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Web;
using System.Web.Hosting;
//using System.Web.Http;
//using ReactFinalProject.Models;
using ParkingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ParkingProject.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>

        public HttpResponseMessage Post(string filePathDes)
        {

            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Images/" + filePathDes + "/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    //docfiles.Add(filePath);
                    docfiles.Add("../Images/" + filePathDes + "/" + postedFile.FileName);

                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}