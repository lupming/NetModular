using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zyck.Frame.Extensions.WeiXinAuth
{
    public class Code2SessionGetResult
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        /// <summary>
        /// 会话密钥
        /// </summary>
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }
        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回，详见 UnionID 机制说明。
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrMessage { get; set; }
    }


    //获取用户手机号
    public class WechatDetails<T>
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// 区域手机号
        /// </summary>
        public string purePhoneNumber { get; set; }

        /// <summary>
        /// 区码
        /// </summary>
        public string countryCode { get; set; }

        public T watermark { get; set; }

        public string Message { get; set; }

    }

    public class Watermark
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 用户appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }


}
