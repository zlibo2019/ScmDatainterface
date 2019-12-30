using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ScmDataInterFace
{
    class Rest
    {
        public static string postUrlJson(string url, string data,ref string json)
        {
            string sResult = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.ContentType = "application/json;charset=UTF-8";


                byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
                request.ContentLength = byteData.Length;

                //以流的形式附加参数
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                json = getResponseString(response);
                return sResult;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return sResult;
            }


        }
        private static string getResponseString(HttpWebResponse response)
        {
            string json = null;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }
    }
}
