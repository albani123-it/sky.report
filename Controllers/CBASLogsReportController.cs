using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using sky.report.Libs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sky.report.Controllers
{
    [Route("skyreport/[controller]")]
    public class CBASLogsReportController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lReport lr = new lReport();

        [HttpPost("List/Filter")]
        public JObject ListFilterCollectionSummary([FromBody]JObject json)
        {
            var data = new JObject();
            try
            {
                var app_date_from = json.GetValue("app_date_star").ToString();
                var app_date_to = json.GetValue("app_date_end").ToString();
                var status = json.GetValue("status").ToString();

                var joParams = new JObject();
                joParams.Add("app_date_star", app_date_from);
                joParams.Add("app_date_end", app_date_to);
                joParams.Add("status", status);

                var jaListData = lr.ListCBASLogsReport(joParams);
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
