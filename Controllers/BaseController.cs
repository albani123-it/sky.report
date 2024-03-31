using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Npgsql;
using System.Data;
using System.Dynamic;
using sky.report.Libs;
using System.Net.Http;
using System.Globalization;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sky.report.Controllers
{
    public class BaseController : Controller
    {
        private lDbConn dbconn = new lDbConn();
        private MessageController mc = new MessageController();
        private int timeout = 5;

        public List<dynamic> getDataToObject(string spname, params string[] list)
        {
            var conn = dbconn.conString();
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            try
            {
                nconn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        var pars = item.Split(',');

                        if (pars.Count() > 2)
                        {
                            if (pars[2] == "i")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                            }
                            else if (pars[2] == "s")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                            }
                            else if (pars[2] == "d")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(pars[0], pars[1]);
                            }
                        }
                        else if (pars.Count() > 1)
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[0]);
                        }
                    }
                }

                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    nconn.Close();
                    NpgsqlConnection.ClearPool(nconn);
                    return retObject;
                }

                retObject = GetDataObj(dr);

                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }
            catch (Exception ex)
            {
                dynamic DyObj = new ExpandoObject() as IDictionary<string, object>;
                DyObj["success"] = false;
                DyObj["message"] = ex.Message;
                retObject.Add(DyObj);

                return retObject;
            }
        }
        
        public void execSqlWithExecption(string spname, params string[] list)
        {
            var conn = dbconn.conString();
            string message = "";
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            nconn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var pars = item.Split(',');

                    if (pars.Count() > 2)
                    {
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                        }
                        else if (pars[2] == "b")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToBoolean(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                    }
                    else if (pars.Count() > 1)
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[1]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[0]);
                    }
                }
            }
            try
            {
                cmd.ExecuteNonQuery();
                message = "success";
            }
            catch (NpgsqlException e)
            {
                message = e.Message;
            }
            finally
            {
                if (nconn.State.Equals(ConnectionState.Open))
                {
                    nconn.Close();
                }
                NpgsqlConnection.ClearPool(nconn);
            }
            //return message;
        }

        public List<dynamic> getDataToObjectDynamic(string dbname, string spname, params string[] list)
        {
            var conn = dbconn.conStringDynamic(dbname);
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            nconn.Open();
            //NpgsqlTransaction tran = nconn.BeginTransaction();
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var pars = item.Split(',');

                    if (pars.Count() > 2)
                    {
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                        }
                        else if (pars[2] == "b")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToBoolean(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                    }
                    else if (pars.Count() > 1)
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[1]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[0]);
                    }
                }
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr == null || dr.FieldCount == 0)
            {
                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }

            retObject = GetDataObj(dr);

            nconn.Close();
            NpgsqlConnection.ClearPool(nconn);
            return retObject;
        }

        public void execSqlWithExecptionReport(string spname, params string[] list)
        {
            var conn = dbconn.conString();
            string message = "";
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            nconn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var pars = item.Split(',');

                    if (pars.Count() > 2)
                    {
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                        }
                        else if (pars[2] == "b")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToBoolean(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                    }
                    else if (pars.Count() > 1)
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[1]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[0]);
                    }
                }
            }
            try
            {
                cmd.ExecuteNonQuery();
                message = "success";
            }
            catch (NpgsqlException e)
            {
                message = e.Message;
            }
            finally
            {
                if (nconn.State.Equals(ConnectionState.Open))
                {
                    nconn.Close();
                }
                NpgsqlConnection.ClearPool(nconn);
            }
            //return message;
        }

        public List<dynamic> getDynamicDataToObject(string spname, string parameter)
        {
            var conn = dbconn.conString();
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;
            var retObject = new List<dynamic>();

            try
            {
                nconn.Open();
                var data = parameter.Split('|');
                if (data.Count() > 1)
                {
                    for (int i = 0; i < data.Count() - 1; i++)
                    {
                        var pars = data[i].Split(',');
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                    }
                }

                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    nconn.Close();
                    NpgsqlConnection.ClearPool(nconn);
                    return retObject;
                }

                retObject = GetDataObj(dr);
                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }
            catch (Exception ex)
            {
                dynamic DyObj = new ExpandoObject() as IDictionary<string, object>;
                DyObj["success"] = false;
                DyObj["message"] = ex.Message;
                retObject.Add(DyObj);
                return retObject;
            }

        }

        public List<dynamic> GetDataObj(NpgsqlDataReader dr)
        {
            var retObject = new List<dynamic>();
            while (dr.Read())
            {
                var dataRow = new ExpandoObject() as IDictionary<string, object>;
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dataRow.Add(
                           dr.GetName(i),
                           dr.IsDBNull(i) ? null : dr[i] // use null instead of {}
                   );
                }
                retObject.Add((ExpandoObject)dataRow);
            }

            return retObject;
        }

        public string execExtAPIPostWithToken(string api, string path, string json, string credential)
        {
            string result = "";
            var WebAPIURL = dbconn.domainGetApi(api);
            string requestStr = WebAPIURL + path;

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Add("Authorization", credential);
            //var contentData = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            ////contentData.Headers.Add("Authorization", credential);   

            //HttpResponseMessage response = client.PostAsync(requestStr, contentData).Result;
            //string result = response.Content.ReadAsStringAsync().Result;

            result = execExtAPIPostWithTokenAwait(api, path, json, credential).Result;

            return result;
        }

        public string execExtAPIGetWithToken(string api, string path, string credential)
        {
            var WebAPIURL = dbconn.domainGetApi(api);
            string requestStr = WebAPIURL + path;

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", credential);
            //HttpResponseMessage response = client.GetAsync(requestStr).Result;
            //string result = response.Content.ReadAsStringAsync().Result;
            //return result;

            var serviceProvider = new ServiceCollection().AddHttpClient()
           .Configure<HttpClientFactoryOptions>("HttpClientWithSSLUntrusted", options =>
               options.HttpMessageHandlerBuilderActions.Add(builder =>
                   builder.PrimaryHandler = new HttpClientHandler
                   {
                       ServerCertificateCustomValidationCallback = (m, crt, chn, e) => true
                   }))
           .BuildServiceProvider();
            var _httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            HttpClient client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            client.BaseAddress = new Uri(requestStr);
            client.Timeout = TimeSpan.FromMinutes(timeout);
            if (!client.DefaultRequestHeaders.Contains("Authorization"))
            {
                client.DefaultRequestHeaders.Add("Authorization", credential);
            }
            else
            {
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", credential);
            }

            HttpResponseMessage response = client.GetAsync(requestStr).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            client.Dispose();
            return result;
        }

        public List<dynamic> getDataToObjectReport(string spname, params string[] list)
        {
            var conn = dbconn.conStringReport();
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            nconn.Open();
            //NpgsqlTransaction tran = nconn.BeginTransaction();
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var pars = item.Split(',');

                    if (pars.Count() > 2)
                    {
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                        }
                        else if (pars[2] == "dt")
                        {
                            cmd.Parameters.AddWithValue(pars[0], DateTime.ParseExact(pars[1], "yyyy-MM-dd", CultureInfo.InvariantCulture));
                        }
                        else if (pars[2] == "b")
                        {
                            cmd.Parameters.AddWithValue(pars[0], Convert.ToBoolean(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                    }
                    else if (pars.Count() > 1)
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[1]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(pars[0], pars[0]);
                    }
                }
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr == null || dr.FieldCount == 0)
            {
                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }

            retObject = GetDataObj(dr);

            nconn.Close();
            NpgsqlConnection.ClearPool(nconn);
            return retObject;
        }

        // add function bani
        public List<dynamic> getDataToObject_flows(string dbprv, string strname, string spname, params string[] list)
        {
            var conn = dbconn.constringList_v2(dbprv, strname);
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            try
            {
                nconn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        var pars = item.Split(',');

                        if (pars.Count() > 2)
                        {
                            if (pars[2] == "i")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                            }
                            else if (pars[2] == "s")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                            }
                            else if (pars[2] == "d")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(pars[0], pars[1]);
                            }
                        }
                        else if (pars.Count() > 1)
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[0]);
                        }
                    }
                }

                /*NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    nconn.Close();
                    NpgsqlConnection.ClearPool(nconn);
                    return retObject;
                }

                retObject = GetDataObj(dr);

                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);*/

                try
                {
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    if (dr == null || dr.FieldCount == 0)
                    {
                        nconn.Close();
                        if (nconn.State.Equals(ConnectionState.Open))
                        {
                            nconn.Close();
                        }
                        NpgsqlConnection.ClearPool(nconn);
                        return retObject;
                    }

                    retObject = GetDataObjPgsql(dr);
                    nconn.Close();
                    if (nconn.State.Equals(ConnectionState.Open))
                    {
                        nconn.Close();
                    }
                    NpgsqlConnection.ClearPool(nconn);

                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    nconn.Close();
                    if (nconn.State.Equals(ConnectionState.Open))
                    {
                        nconn.Close();
                    }
                    NpgsqlConnection.ClearPool(nconn);
                }

                return retObject;
            }
            catch (Exception ex)
            {
                dynamic DyObj = new ExpandoObject() as IDictionary<string, object>;
                DyObj["success"] = false;
                DyObj["message"] = ex.Message;
                retObject.Add(DyObj);
                nconn.Close();
                return retObject;
            }
        }

        public List<dynamic> execSQLWithOutputDynamicPrm(string str)
        {
            var dbprv = dbconn.sqlprovider();
            var strname = dbconn.constringName("skyen");
            var conn = dbconn.constringList_v2(dbprv, strname);
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();
            nconn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(str, nconn);
            cmd.CommandType = CommandType.Text;


            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr == null || dr.FieldCount == 0)
            {
                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }
            retObject = GetDataObj(dr);
            nconn.Close();
            NpgsqlConnection.ClearPool(nconn);
            return retObject;
        }

        // end 
        //str Generation
        public void ExecSqlWithoutReturnCustomSplit(string dbprv, string strname, string cstsplit, string schema, string spname, params string[] list)
        {
            var retObject = new List<dynamic>();
            string message = "";
            StringBuilder sb = new StringBuilder();
            //var conn = dbconn.constringList(dbprv, strname);
            var conn = dbconn.constringList_v2(dbprv, strname);

            if (dbprv == "postgresql")
            {
                spname = schema + "." + spname;
                NpgsqlConnection nconn = new NpgsqlConnection(conn);
                nconn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        var pars = item.Split(cstsplit);

                        if (pars.Count() > 2)
                        {
                            if (pars[2] == "i")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToInt32(pars[1]));
                            }
                            else if (pars[2] == "s")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToString(pars[1]));
                            }
                            else if (pars[2] == "d")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToDecimal(pars[1]));
                            }
                            else if (pars[2] == "b")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToBoolean(pars[1]));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[1]);
                            }
                        }
                        else if (pars.Count() > 1)
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[0]);
                        }
                    }
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    message = mc.GetMessage("execdb_success");
                }
                catch (NpgsqlException e)
                {
                    message = e.Message;
                }
                finally
                {
                    nconn.Close();
                }
            }
            else if (dbprv == "sqlserver")
            {
                SqlConnection nconn = new SqlConnection(conn);
                nconn.Open();
                SqlCommand cmd = new SqlCommand(spname, nconn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        var pars = item.Split(cstsplit);

                        if (pars.Count() > 2)
                        {
                            if (pars[2] == "i")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                            }
                            else if (pars[2] == "s")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                            }
                            else if (pars[2] == "d")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                            }
                            else if (pars[2] == "b")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToBoolean(pars[1]));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(pars[0], pars[1]);
                            }
                        }
                        else if (pars.Count() > 1)
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[0]);
                        }
                    }
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    message = mc.GetMessage("execdb_success");
                }
                catch (NpgsqlException e)
                {
                    message = e.Message;
                }
                finally
                {
                    nconn.Close();
                }
            }
        }


        public List<dynamic> ExecSqlWithReturnCustomSplit(string dbprv, string strname, string cstsplit, string schema, string spname, params string[] list)
        {
            var retObject = new List<dynamic>();
            StringBuilder sb = new StringBuilder();
            var conn = dbconn.constringList_v2(dbprv, strname);

            spname = schema + "." + spname;
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            nconn.Open();
            //NpgsqlTransaction tran = nconn.BeginTransaction();
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var pars = item.Split(cstsplit);

                    if (pars.Count() > 2)
                    {
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), (Convert.ToString(pars[1])));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToDecimal(pars[1]));
                        }
                        else if (pars[2] == "dt")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), DateTime.ParseExact(pars[1], "yyyy-MM-dd", CultureInfo.InvariantCulture));
                        }
                        else if (pars[2] == "b")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToBoolean(pars[1]));
                        }
                        else if (pars[2] == "bg")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToInt64(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[1]);
                        }
                    }
                    else if (pars.Count() > 1)
                    {
                        cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[1]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[0]);
                    }
                }
            }

            //NpgsqlDataReader dr = cmd.ExecuteReader();

            //if (dr == null || dr.FieldCount == 0)
            //{
            //    nconn.Close();
            //    NpgsqlConnection.ClearPool(nconn);
            //    return retObject;
            //}

            //retObject = GetDataObjPgsql(dr);
            //nconn.Close();
            //NpgsqlConnection.ClearPool(nconn);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    nconn.Close();
                    if (nconn.State.Equals(ConnectionState.Open))
                    {
                        nconn.Close();
                    }
                    NpgsqlConnection.ClearPool(nconn);
                    return retObject;
                }

                retObject = GetDataObjPgsql(dr);
                nconn.Close();
                if (nconn.State.Equals(ConnectionState.Open))
                {
                    nconn.Close();
                }
                NpgsqlConnection.ClearPool(nconn);

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                nconn.Close();
                if (nconn.State.Equals(ConnectionState.Open))
                {
                    nconn.Close();
                }
                NpgsqlConnection.ClearPool(nconn);
            }

            return retObject;
        }


        public List<dynamic> ExecSqlWithReturnCustomSplitDynamic(string dbname, string strname, string cstsplit, string schema, string spname, params string[] list)
        {
            var retObject = new List<dynamic>();
            StringBuilder sb = new StringBuilder();
            var conn = dbconn.constringListDynamic(dbname, strname);

            spname = schema + "." + spname;
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            nconn.Open();
            //NpgsqlTransaction tran = nconn.BeginTransaction();
            NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var pars = item.Split(cstsplit);

                    if (pars.Count() > 2)
                    {
                        if (pars[2] == "i")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToInt32(pars[1]));
                        }
                        else if (pars[2] == "s")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), (Convert.ToString(pars[1])));
                        }
                        else if (pars[2] == "d")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToDecimal(pars[1]));
                        }
                        else if (pars[2] == "dt")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), DateTime.ParseExact(pars[1], "yyyy-MM-dd", CultureInfo.InvariantCulture));
                        }
                        else if (pars[2] == "b")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToBoolean(pars[1]));
                        }
                        else if (pars[2] == "bg")
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), Convert.ToInt64(pars[1]));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[1]);
                        }
                    }
                    else if (pars.Count() > 1)
                    {
                        cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[1]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue((pars[0].ToString()).Replace("@", "p_"), pars[0]);
                    }
                }
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr == null || dr.FieldCount == 0)
            {
                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }

            retObject = GetDataObjPgsql(dr);
            nconn.Close();
            NpgsqlConnection.ClearPool(nconn);
            return retObject;
        }

        public List<dynamic> GetDataObjPgsql(NpgsqlDataReader dr)
        {
            var retObject = new List<dynamic>();
            while (dr.Read())
            {
                var dataRow = new ExpandoObject() as IDictionary<string, object>;
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dataRow.Add(
                           dr.GetName(i),
                           dr.IsDBNull(i) ? null : dr[i] // use null instead of {}
                   );
                }
                retObject.Add((ExpandoObject)dataRow);
            }

            return retObject;
        }

        public List<dynamic> GetDataObjSqlsvr(SqlDataReader dr)
        {
            var retObject = new List<dynamic>();
            while (dr.Read())
            {
                var dataRow = new ExpandoObject() as IDictionary<string, object>;
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dataRow.Add(
                           dr.GetName(i),
                           dr.IsDBNull(i) ? null : dr[i] // use null instead of {}
                   );
                }
                retObject.Add((ExpandoObject)dataRow);
            }

            return retObject;
        }

        public List<dynamic> ExecSqlWithReturnCustomSplitReport(string spname, params string[] list)
        {
            var conn = dbconn.conStringReport();
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            try
            {
                nconn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        var pars = item.Split("|");

                        if (pars.Count() > 2)
                        {
                            if (pars[2] == "i")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                            }
                            else if (pars[2] == "s")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                            }
                            else if (pars[2] == "d")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                            }
                            else if (pars[2] == "dt")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()), DateTime.ParseExact(pars[1], "yyyy-MM-dd", CultureInfo.InvariantCulture));
                            }
                            else if (pars[2] == "b")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()), Convert.ToBoolean(pars[1]));
                            }

                            else
                            {
                                cmd.Parameters.AddWithValue(pars[0], pars[1]);
                            }
                        }
                        else if (pars.Count() > 1)
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[0]);
                        }
                    }
                }

                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    nconn.Close();
                    NpgsqlConnection.ClearPool(nconn);
                    return retObject;
                }

                retObject = GetDataObj(dr);

                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }
            catch (Exception ex)
            {
                dynamic DyObj = new ExpandoObject() as IDictionary<string, object>;
                DyObj["success"] = false;
                DyObj["message"] = ex.Message;
                retObject.Add(DyObj);

                return retObject;
            }
        }

        public List<dynamic> ExecSqlWithReturnCustomSplitnotsymbol(string spname, params string[] list)
        {
            var conn = dbconn.conString();
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            try
            {
                nconn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        var pars = item.Split(";");

                        if (pars.Count() > 2)
                        {
                            if (pars[2] == "i")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                            }
                            else if (pars[2] == "s")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                            }
                            else if (pars[2] == "d")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                            }
                            else if (pars[2] == "dt")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()), DateTime.ParseExact(pars[1], "yyyy-MM-dd", CultureInfo.InvariantCulture));
                            }
                            else if (pars[2] == "b")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()), Convert.ToBoolean(pars[1]));
                            }

                            else
                            {
                                cmd.Parameters.AddWithValue(pars[0], pars[1]);
                            }
                        }
                        else if (pars.Count() > 1)
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[0]);
                        }
                    }
                }

                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    nconn.Close();
                    NpgsqlConnection.ClearPool(nconn);
                    return retObject;
                }

                retObject = GetDataObj(dr);

                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }
            catch (Exception ex)
            {
                dynamic DyObj = new ExpandoObject() as IDictionary<string, object>;
                DyObj["success"] = false;
                DyObj["message"] = ex.Message;
                retObject.Add(DyObj);

                return retObject;
            }
        }

        public List<dynamic> ExecSqlWithReturnCustomSplitnotsymbolReport(string spname, params string[] list)
        {
            var conn = dbconn.conStringReport();
            StringBuilder sb = new StringBuilder();
            NpgsqlConnection nconn = new NpgsqlConnection(conn);
            var retObject = new List<dynamic>();

            try
            {
                nconn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(spname, nconn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (list != null && list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        var pars = item.Split(";");

                        if (pars.Count() > 2)
                        {
                            if (pars[2] == "i")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToInt32(pars[1]));
                            }
                            else if (pars[2] == "s")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToString(pars[1]));
                            }
                            else if (pars[2] == "d")
                            {
                                cmd.Parameters.AddWithValue(pars[0], Convert.ToDecimal(pars[1]));
                            }
                            else if (pars[2] == "dt")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()), DateTime.ParseExact(pars[1], "yyyy-MM-dd", CultureInfo.InvariantCulture));
                            }
                            else if (pars[2] == "b")
                            {
                                cmd.Parameters.AddWithValue((pars[0].ToString()), Convert.ToBoolean(pars[1]));
                            }

                            else
                            {
                                cmd.Parameters.AddWithValue(pars[0], pars[1]);
                            }
                        }
                        else if (pars.Count() > 1)
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pars[0], pars[0]);
                        }
                    }
                }

                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr == null || dr.FieldCount == 0)
                {
                    nconn.Close();
                    NpgsqlConnection.ClearPool(nconn);
                    return retObject;
                }

                retObject = GetDataObj(dr);

                nconn.Close();
                NpgsqlConnection.ClearPool(nconn);
                return retObject;
            }
            catch (Exception ex)
            {
                dynamic DyObj = new ExpandoObject() as IDictionary<string, object>;
                DyObj["success"] = false;
                DyObj["message"] = ex.Message;
                retObject.Add(DyObj);

                return retObject;
            }
        }

        public async Task<string> execExtAPIPostWithTokenAwait(string api, string path, string json, string credential)
        {
            #region call others api version : v.3
            string result = "";
            var WebAPIURL = dbconn.domainGetApi(api);
            string requestStr = WebAPIURL + path;

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", credential);
            var serviceProvider = new ServiceCollection().AddHttpClient()
           .Configure<HttpClientFactoryOptions>("HttpClientWithSSLUntrusted", options =>
               options.HttpMessageHandlerBuilderActions.Add(builder =>
                   builder.PrimaryHandler = new HttpClientHandler
                   {
                       ServerCertificateCustomValidationCallback = (m, crt, chn, e) => true
                   }))
           .BuildServiceProvider();
            var _httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            HttpClient client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            client.BaseAddress = new Uri(requestStr);
            client.Timeout = TimeSpan.FromMinutes(timeout);
            if (!client.DefaultRequestHeaders.Contains("Authorization"))
            {
                client.DefaultRequestHeaders.Add("Authorization", credential);
            }
            else
            {
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", credential);
            }

            var contentData = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(requestStr, contentData);
            result = await response.Content.ReadAsStringAsync();
            client.Dispose();
            #endregion

            return result;
        }
    }
}
