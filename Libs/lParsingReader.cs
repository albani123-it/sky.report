using sky.report.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sky.report.Libs
{
    public class lParsingReader
    {
        private lDbConn dbconn = new lDbConn();
        private lConvert lc = new lConvert();
        private BaseController bc = new BaseController();
       
        public ArrayList CleansingRawData(string rawdata)
        {
            var arrData = rawdata.Split("\n");

            ArrayList listData = new ArrayList();

            if (arrData.Length > 0)
            {
                for (int i = 0; i < arrData.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(arrData[i].ToString()))
                    {
                        listData.Add(arrData[i].ToString());
                    }
                }
            }

            return listData;
        }
        


    }
}
