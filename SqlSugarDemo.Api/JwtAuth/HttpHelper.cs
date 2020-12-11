using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.JwtAuth
{
    public class HttpHelper
    {
        /// <summary>
        /// 调用WEBAPI方法
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="body">参数</param>
        /// <returns></returns>
        public static string HttpPost(string url, string body, string method = "POST")
        {
            try
            {
                JwtMaker jwtMaker = new JwtMaker();
                string token = "";// jwtMaker.Make();
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                byte[] buffer = encoding.GetBytes(body);
                request.ContentLength = buffer.Length;
                if (method.Equals("POST"))
                {
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {

                HttpWebResponse response = (HttpWebResponse)ex.Response;
                if (response != null)
                {
                    Console.WriteLine("Error code: {0}", response.StatusCode);

                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
                    {
                        if (reader != null)
                        {
                            string text = reader.ReadToEnd();
                            int start = text.IndexOf("System.Exception:");
                            if (start >= 0)
                            {
                                string s = text.Length >= 2000 ? text.Substring(start, 2000) : text.Substring(start);
                                throw new Exception("错误信息：" + s);
                            }
                            else
                            {
                                throw new Exception("错误信息" + ex.Message);
                            }
                        }
                        else
                        {
                            throw new Exception("服务器异常");
                        }
                    }
                }
                else
                {
                    throw new Exception("XX" + ex.Message);
                }

            }
        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json, DataTable dt)
        {
            DataTable result;
            try
            {
                ArrayList arrayList = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayList>(json);
                int row = 0;
                foreach (var ks in JArray.Parse(json))
                {
                    Console.WriteLine(ks);
                    DataRow dr = dt.NewRow();
                    IEnumerable<JProperty> pp = JObject.Parse(ks.ToString()).Properties();

                    int drRow = 0;
                    foreach (JProperty item in pp)
                    {
                        Type dataType = dt.Columns[drRow].DataType;

                        if (dataType.Name == "Int")
                        {
                            dr[drRow] = Convert.ToInt16(item.Value);
                        }
                        else if (dataType.Name == "Int32")
                        {
                            dr[drRow] = Convert.ToInt32(item.Value);
                        }
                        else if (dataType.Name == "Int64")
                        {
                            dr[drRow] = Convert.ToInt64(item.Value);
                        }
                        else
                        {
                            dr[drRow] = item.Value;
                        }
                        drRow++;
                    }

                    dt.Rows.Add(dr);
                    row++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            result = dt;
            return result;
        }
    }
}
