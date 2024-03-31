using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sky.report.Libs;
using Newtonsoft.Json.Linq;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sky.report.Controllers
{
    [Route("skyreport/[controller]")]
    public class TokenController : Controller
    {
        private BaseController bc = new BaseController();
        private lDbConn dbconn = new lDbConn();

        public JObject GetClientToken()
        {
            JObject jOutput = new JObject();
            var WebAPIURL = dbconn.domainGetApi("urlAPI_idccore");
            string requestStr = WebAPIURL + "token";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("username", dbconn.domainGetTokenCredential("UserNameClient"));
            client.DefaultRequestHeaders.Add("password", dbconn.domainGetTokenCredential("PasswordClient"));
            var contentData = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = client.PostAsync(requestStr, contentData).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            jOutput = JObject.Parse(result);
            client.Dispose();
            return jOutput;
        }

        public JObject GetLMSToken()
        {
            JObject jOutput = new JObject();
            var WebAPIURL = dbconn.domainGetApi("urlAPI_idccore");
            string requestStr = WebAPIURL + "token";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("username", dbconn.domainGetTokenCredential("UserName"));
            client.DefaultRequestHeaders.Add("password", dbconn.domainGetTokenCredential("Password"));
            var contentData = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = client.PostAsync(requestStr, contentData).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            jOutput = JObject.Parse(result);
            client.Dispose();
            return jOutput;
        }

        public JObject GetCUSTToken()
        {
            JObject jOutput = new JObject();
            var WebAPIURL = dbconn.domainGetApi("urlAPI_idccore");
            string requestStr = WebAPIURL + "token";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("username", dbconn.domainGetTokenCredential("UserName"));
            client.DefaultRequestHeaders.Add("password", dbconn.domainGetTokenCredential("Password"));
            var contentData = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = client.PostAsync(requestStr, contentData).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            jOutput = JObject.Parse(result);
            client.Dispose();
            return jOutput;
        }

        public JObject GetENToken()
        {
            JObject jOutput = new JObject();
            var WebAPIURL = dbconn.domainGetApi("urlAPI_idccore");
            string requestStr = WebAPIURL + "token";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("username", dbconn.domainGetTokenCredential("UserName"));
            client.DefaultRequestHeaders.Add("password", dbconn.domainGetTokenCredential("Password"));
            var contentData = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = client.PostAsync(requestStr, contentData).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            jOutput = JObject.Parse(result);
            client.Dispose();
            return jOutput;
        }

        public JObject GetKBIJToken()
        {
            JObject jOutput = new JObject();
            var WebAPIURL = dbconn.domainGetApi("urlAPI_idccore");
            string requestStr = WebAPIURL + "token";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("username", dbconn.domainGetTokenCredential("UserName"));
            client.DefaultRequestHeaders.Add("password", dbconn.domainGetTokenCredential("Password"));
            var contentData = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = client.PostAsync(requestStr, contentData).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            jOutput = JObject.Parse(result);
            client.Dispose();
            return jOutput;
        }

    }
}
