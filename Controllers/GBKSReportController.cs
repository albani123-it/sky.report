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
    public class GBKSReportController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lReport lr = new lReport();
        #region Post
        [HttpPost("List")]
        public JObject ListFiltergbksReport([FromBody]JObject json)
        {
            var data = new JObject();

            try
            {
                var scc_code = json.GetValue("scc_code").ToString();
                var period_from = json.GetValue("strdate").ToString();
                var period_to = json.GetValue("enddate").ToString();

                var jaListData = lr.GBKSReport(scc_code, period_from, period_to);
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
        #endregion
    }
}