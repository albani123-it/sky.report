using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sky.report.Libs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sky.report.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("skyreport/[controller]")]
    public class SlikReportController : Controller
    {
        private BaseController bc = new BaseController();
        private MessageController mc = new MessageController();
        private lConvert lc = new lConvert();
        private lDbConn dbcon = new lDbConn();

        [HttpGet("GenerateFile")]
        public JObject GenerateFile()
        {
            JObject jResult = new JObject();
            var data = new JObject();
            var ctx = HttpContext;
            var foldername = "";
            var filename = "";

            try
            {
                data.Add("name", "Generate File SLIK");
                var configFolder = Path.GetFullPath("files/slik/Header/");
                var generateFolder = Path.GetFullPath("files/slik/");
                string allText = "";

                using (StreamReader sr = new StreamReader(configFolder + "header_config.json"))
                {
                    allText = sr.ReadToEnd();
                }

                var joConfig = JObject.Parse(allText);
                var strHeader = joConfig.GetValue("config").ToString();
                var jaHeader = JArray.Parse(strHeader);

                if (jaHeader.Count > 0)
                {
                    var jaFilename = new JArray();
                    for (int i = 0; i < jaHeader.Count; i++)
                    {
                        var joFilename = new JObject();
                        var csvFile = new StringBuilder();
                        var currentMonth = DateTime.Now.ToString("yyyyMM");
                        var joSegmen = JObject.Parse(jaHeader[i].ToString());
                        foldername = joSegmen.GetValue("prefix").ToString() + joSegmen.GetValue("segmen").ToString() + "/";
                        filename = joSegmen.GetValue("segmen").ToString() + currentMonth + ".csv";

                        var joHeaderConfig = JObject.Parse(joSegmen.GetValue("header").ToString());

                        if (joHeaderConfig.Count > 0)
                        {
                            for (int a = 0; a < joHeaderConfig.Count; a++)
                            {
                                var propname = Convert.ToString(a + 1);
                                if (a == joHeaderConfig.Count - 1)
                                {
                                    csvFile.Append(joHeaderConfig.GetValue(propname).ToString());
                                }
                                else
                                {
                                    csvFile.Append(joHeaderConfig.GetValue(propname).ToString() + "|");
                                }
                            }
                        }

                        var jaData = GenerateData(joSegmen.GetValue("segmen").ToString());

                        // extract data by segmen to csv file
                        var valData = new JObject();
                        if (jaData.Count > 0)
                        {
                            csvFile.AppendLine();
                            for (int b = 0; b < jaData.Count; b++)
                            {
                                var strRowData = "";
                                var joRowData = JObject.Parse(jaData[b].ToString());

                                foreach (var item in joRowData)
                                {
                                    strRowData += item.Value.ToString() + "|";
                                }

                                strRowData = strRowData.Substring(0, strRowData.Length - 1);
                                csvFile.AppendLine(strRowData);
                            }
                        }

                        System.IO.File.Delete(generateFolder + foldername + filename);
                        using (StreamWriter outputFile = new StreamWriter(generateFolder + foldername + filename, true))
                        {
                            outputFile.WriteLine(csvFile.ToString());
                            outputFile.Dispose();
                        }
                        joFilename.Add("filename", filename);
                        jaFilename.Add(joFilename);
                    }

                    data.Add("data", jaFilename);
                }
            }
            catch (Exception ex)
            {
                data.Add("message", ex.Message);
            }

            return data;
        }

        [HttpGet("ListLJKFile")]
        public JObject ListLJKFile()
        {
            JObject jResult = new JObject();
            string allText = "";
            var foldername = "";

            var configFolder = Path.GetFullPath("files/slik/Header/");
            var generateFolder = Path.GetFullPath("files/slik/");


            using (StreamReader sr = new StreamReader(configFolder + "header_config.json"))
            {
                allText = sr.ReadToEnd();
            }

            var joConfig = JObject.Parse(allText);
            var strHeader = joConfig.GetValue("config").ToString();
            var jaHeader = JArray.Parse(strHeader);
            var jaData = new JArray();


            if (jaHeader.Count > 0)
            {
                for (int i = 0; i < jaHeader.Count; i++)
                {
                    string path = "";
                    JArray jaDataSegment = new JArray();
                    JObject joDataSegment = new JObject();
                    var joSegmen = JObject.Parse(jaHeader[i].ToString());
                    foldername = joSegmen.GetValue("prefix").ToString() + joSegmen.GetValue("segmen").ToString();
                    path = generateFolder + foldername;

                    var spr = dbcon.SeparatorFileSlik();

                    string[] fileList = System.IO.Directory.GetFiles(path);
                    foreach (var file in fileList)
                    {
                        var strFile = file;
                        var arrFile = strFile.Split(spr);
                        var filename = arrFile[arrFile.Length - 1];
                        var arrFilename = filename.Split(".");

                        var periode = "";
                        var periodename = "";
                        JObject joData = new JObject();

                        if (arrFilename.Length > 0)
                        {
                            if ((arrFilename[1].ToString()).ToLower() == "csv")
                            {
                                periode = (arrFilename[0].ToString()).Substring((arrFilename[0].ToString()).Length - 6, 6);
                                periodename = PeriodeYearMonth(periode);

                                joData.Add("segment", joSegmen.GetValue("segmen").ToString());
                                joData.Add("periode", periode);
                                joData.Add("periodename", periodename);
                                joData.Add("filename", filename);
                                joData.Add("path", file);
                                jaDataSegment.Add(joData);
                            }
                        }
                    }
                    joDataSegment.Add("segmentname", joSegmen.GetValue("segmen").ToString());
                    joDataSegment.Add("data", jaDataSegment);
                    jaData.Add(joDataSegment);
                }

            }
            jResult.Add("data", jaData);

            return jResult;
        }

        [HttpGet("DownloadFIle/{filename}")]
        public async Task<IActionResult> DownloadFIle(string filename)
        {
            filename = filename.ToUpper();
            var file = filename.Split(".");

            if (file.Length > 1)
            {
                filename = file[0].ToString() + "." + (file[1].ToString()).ToLower();
            }

            var segment = filename.Substring(0, 3);
            var getFoleder = "";
            //getFoleder = Path.GetFullPath("files/slik/");
            getFoleder = dbcon.PathFileSlik();
            var path = getFoleder + "SLIK" + segment + "/" + filename;

            var memory = new MemoryStream();
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            return File(memory, GetContentType(path), filename);
            //return File(memory, "application/x-msdownload", filename);
            //return File(memory, "application/octet-stream");
        }

        public string PeriodeYearMonth(string periode)
        {
            string strResult = "";
            var year = periode.Substring(0, 4);
            var month = periode.Substring(4, 2);
            var monthname = lc.ConvertStringMonth(year, month);

            strResult = monthname + " " + year;

            return strResult;
        }

        [HttpGet("Download/{filename}")]
        public string Download(string filename)
        {
            //filename = filename.ToUpper();
            //var segment = filename.Substring(0, 3);
            //var getFoleder = "";
            ////getFoleder = Path.GetFullPath("files/slik/");
            //getFoleder = dbcon.PathFileSlik();
            //var path = getFoleder + "SLIK" + segment + "/" + filename;
            //return path; 

            filename = filename.ToUpper();
            var file = filename.Split(".");

            if (file.Length > 1)
            {
                filename = file[0].ToString() + "." + (file[1].ToString()).ToLower();
            }

            var segment = filename.Substring(0, 3);
            var getFoleder = "";
            getFoleder = dbcon.PathFileSlik();
            var path = getFoleder + "SLIK" + segment + "/" + filename;

            return path;

        }

        public JArray GenerateData(string segmen)
        {
            var retObject = new List<dynamic>();
            var spname = "";
            spname = "public.collect_data_" + segmen.ToLower();
            retObject = bc.getDataToObject(spname);

            var valDataReturn = lc.convertDynamicToJArray(retObject);
            return valDataReturn;
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
