using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using sky.report.Libs;


namespace sky.report.Controllers
{
    [Route("skyreport/[controller]")]
    public class GLReportController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lReport lr = new lReport();



        [HttpPost("List/Filter")]
        public JObject ListFilter([FromBody]JObject json)
        {
            var data = new JObject();

            try
            {
                var start_date = json.GetValue("start_date").ToString();
                var end_date = json.GetValue("end_date").ToString();

                var joParams = new JObject();
                joParams.Add("start_date", start_date);
                joParams.Add("end_date", end_date);

                var jaListData = lr.ListGLReport(joParams); 
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("data", jaListData);

            }
            catch (Exception ex)
            {
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }

            return data;
        }

    }
}