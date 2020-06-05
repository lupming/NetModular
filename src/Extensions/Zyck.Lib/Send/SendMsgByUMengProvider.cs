using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Zyck.Frame.Extensions
{
    public class SendMsgByUMengProvider
    {
        //文档说明地址 https://developer.umeng.com/docs/66632/detail/68343

        private string url = "http://msg.umeng.com/api/send";
        private string app_key = "5c93406b3fc195c6290002da";
        private string app_master_secret = "lqfifrspdqu563arhiwv4j5jcjsqec2g";

        public string TestIOS()
        {
            string _type = "broadcast";         //"unicast";//"broadcast";
            string _device_tokens = "";
            string _description = "4this is test ios description";
            string _content = "44this is test ios alert";
            string _production_mode = "false";

            JsonIOSData jsonData = new JsonIOSData()
            {
                appkey = app_key,
                timestamp = GetTimeStamp(),
                type = _type,
                device_tokens = _device_tokens,
                description = _description,
                payload = new PayloadIOS()
                {
                    aps = new Aps()
                    {
                        alert = _content
                    }
                },
                production_mode = _production_mode
            };
            string jsonStr = JsonConvert.SerializeObject(jsonData);
            ReturnJsonClass r = SendMethod(jsonStr);
            return r.ret;
        }

        public string TestAndroid()
        {
            string _type = "broadcast";
            string _device_tokens = "";
            string _description = "this is test Android description";
            string _display_type = "notification";//"notification";
            string _ticker = "必填 通知栏提示文字";
            string _title = "必填 通知标题";     // 必填 通知栏提示文字
            string _text = "必填 通知文字描述";     // 必填 通知栏提示文字
            string _after_open = "go_app";// 必填 值可以为: "go_app": 打开应用   "go_url": 跳转到URL   "go_activity": 打开特定的activity   "go_custom": 用户自定义内容。
            string _production_mode = "false";

            JsonAndroidData jsonData = new JsonAndroidData()
            {
                appkey = app_key,
                timestamp = GetTimeStamp(),
                type = _type,
                device_tokens = _device_tokens,
                payload = new PayloadAndroid()
                {
                    display_type = _display_type,
                    body = new ContentBody
                    {
                        ticker = _ticker,
                        title = _title,
                        text = _text,
                        after_open = _after_open
                    }
                },
                description = _description,
                production_mode = _production_mode
            };
            string jsonStr = JsonConvert.SerializeObject(jsonData);
            ReturnJsonClass r = SendMethod(jsonStr);
            return r.ret;
        }

        public ReturnJsonClass SendMethod(string jsonStr)
        {
            string newUrl = url;
            try
            {
                string mysign = getMD5Hash("POST" + newUrl + jsonStr + app_master_secret);
                newUrl = newUrl + "?sign=" + mysign;

                HttpWebRequest reuqest = CreateRequest(newUrl);
                HttpWebResponse response = GetRequestResponse(reuqest, jsonStr);
                ReturnJsonClass jsonResult = GetJsonResult(response);
                return jsonResult;
            }
            catch (WebException e)
            {
                string retString = "";
                //Console.WriteLine("This program is expected to throw WebException on successful run." +
                //                                   "\n\nException Message :" + e.Message);

                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    //Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                    //Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                    Stream myResponseStream = ((HttpWebResponse)e.Response).GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    retString = myStreamReader.ReadToEnd();
                    //Console.WriteLine("return content:", retString);
                }
                return new ReturnJsonClass() { ret = "FAIL", data = new ResultInfo() { error_code = retString } };
            }

        }

        public HttpWebRequest CreateRequest(string _url)
        {
            HttpWebRequest request = WebRequest.Create(_url) as HttpWebRequest;
            request.Method = "POST";
            return request;
        }

        public HttpWebResponse GetRequestResponse(HttpWebRequest request, string data)
        {
            byte[] bs = Encoding.UTF8.GetBytes(data);
            request.ContentLength = bs.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
                reqStream.Close();
            }
            return (HttpWebResponse)request.GetResponse();
        }

        public ReturnJsonClass GetJsonResult(HttpWebResponse response)
        {
            Stream s = response.GetResponseStream();
            StreamReader reader = new StreamReader(s);
            string result = reader.ReadToEnd();
            //ReturnJsonClass resultJson = JsonConvert.DeserializeObject<ReturnJsonClass>(result);
            return JsonConvert.DeserializeObject<ReturnJsonClass>(result);
        }

        /// <summary>
        /// 计算MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public String getMD5Hash(String str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            StringBuilder strReturn = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                strReturn.Append(Convert.ToString(result[i], 16).PadLeft(2, '0'));
            }
            return strReturn.ToString().PadLeft(32, '0');
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

    }

}
