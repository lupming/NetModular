using NetModular;
using NetModular.Lib.Utils.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;

namespace Zyck.Frame.Extensions.WeiXinAuth
{
    /// <summary>
    /// 微信公众号相关鉴权方法
    /// </summary>
    [Singleton(true)]
    public class WechatMpAuthHandler
    {
        /// <summary>
        /// 微信公众号的鉴权地址
        /// </summary>
        const string Oauth2Url = "https://api.weixin.qq.com/sns/oauth2/access_token";

        const string Oauth2UserInfoUrl = "https://api.weixin.qq.com/sns/userinfo";

        public WechatMpAuthHandler()
        {

        }

        /// <summary>
        /// 获取公众号关联的OpenId
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IResultModel<string>> GetOpenId(WechatMpAuthParam param)
        {
            ResultModel<string> resultModel = new ResultModel<string>();

            var url = Oauth2Url + "?appid=" + param.AppId + "&secret=" + param.AppSecret + "&code=" + param.Code + "&grant_type=authorization_code";
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(url);

            Dictionary<string, object> contentDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            //{"access_token":"ACCESS_TOKEN", "expires_in":7200, "refresh_token":"REFRESH_TOKEN","openid":"OPENID", "scope":"SCOPE","unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"}

            if (contentDict.Keys.Contains("errmsg"))
                return resultModel.Failed(contentDict["errmsg"].ToString());

            return resultModel.Success(contentDict["openid"].ToString());
        }

        /// <summary>
        /// 获取公众号关联的UserInfo
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IResultModel<Dictionary<string, object>>> GetUserInfo(WechatMpAuthParam param)
        {
            ResultModel<Dictionary<string, object>> resultModel = new ResultModel<Dictionary<string, object>>();

            var url = Oauth2Url + "?appid=" + param.AppId + "&secret=" + param.AppSecret + "&code=" + param.Code + "&grant_type=authorization_code";
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(url);
            Dictionary<string, object> contentDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            if (contentDict.Keys.Contains("errmsg"))
                return resultModel.Failed(contentDict["errmsg"].ToString());

            var access_token = contentDict["access_token"].ToString();
            var openid = contentDict["openid"].ToString();

            url = Oauth2UserInfoUrl + "?access_token=" + access_token + "&openid=" + openid;
            content = await httpClient.GetStringAsync(url);

            //{"openid":"OPENID","nickname":"NICKNAME","sex":1,"province":"PROVINCE","city":"CITY","country":"COUNTRY",
            //"headimgurl": "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0",
            //"privilege":["PRIVILEGE1","PRIVILEGE2"],"unionid": " o6_bmasdasdsad6_2sgVt7hMZOPfL"}
            contentDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

            if (contentDict.Keys.Contains("errmsg"))
                return resultModel.Failed(contentDict["errmsg"].ToString());

            return resultModel.Success(contentDict);
        }

    }


}
