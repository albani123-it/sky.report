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
    public class PopStabilityReportController : Controller
    {
        private lMessage mc = new lMessage();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
        private TokenController tc = new TokenController();
        private lReport lr = new lReport();


        #region Get

        [HttpGet("getscccode")]
        public JObject getScorecardCodeModel()
        {

            var data = new JObject();
            try
            {
                data = new JObject();


                var jaListData = lr.getScorecardCodeReport();
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", jaListData);
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }

        #endregion

        #region Post

        [HttpPost("getratingmodel")]
        public JObject getRatingModel([FromBody] JObject json)
        {

            var data = new JObject();
            try
            {
                data = new JObject();
                var dataappled = lr.GetAppliedData(json);

                var jaListData = lr.getdataratingmodelReport(dataappled);
                data.Add("status", mc.GetMessage("api_output_ok"));
                data.Add("message", mc.GetMessage("process_success"));
                data.Add("data", jaListData);
            }
            catch (Exception ex)
            {
                data = new JObject();
                data.Add("status", mc.GetMessage("api_output_not_ok"));
                data.Add("message", ex.Message);
            }
            return data;
        }


        [HttpPost("List")]
        public JObject ListFilterPopStabilityReport([FromBody]JObject json)
        {
            var data = new JObject();

            try
            {

                var scc_code = json.GetValue("scc_code").ToString();
                var str_date = json.GetValue("strdate").ToString();
                var end_date = json.GetValue("enddate").ToString();

                var jaListData = lr.ListPopStabilityReport(scc_code, str_date, end_date);
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