using NetModular;
using NetModular.Lib.Utils.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

using System.Threading.Tasks;

namespace Zyck.Frame.Extensions.WeiXinAuth
{
    /// <summary>
    /// 微信小程序相关鉴权方法
    /// </summary>
    [Singleton(true)]
    public class WechatMiniAuthHandler
    {
        /// <summary>
        /// 微信小程序的鉴权地址
        /// </summary>
        const string Code2SessionUrl = "https://api.weixin.qq.com/sns/jscode2session";

        public WechatMiniAuthHandler()
        {

        }

        /// <summary>
        /// 获取小程序关联的OpenId
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IResultModel<string>> GetOpenId(WechatMiniAuthParam param)
        {
            ResultModel<string> resultModel = new ResultModel<string>();

            var url = $"{Code2SessionUrl}?appid={param.AppId}&secret={param.AppSecret}&js_code={param.Code}&grant_type=authorization_code";
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(url);

            if (string.IsNullOrWhiteSpace(content))
                return resultModel.Failed("参数有误");

            var result = JsonConvert.DeserializeObject<Code2SessionGetResult>(content);
            if (result.ErrCode != 0)
                return resultModel.Failed(result.ErrMessage);

            return resultModel.Success(result.OpenId);
        }


        /// <summary>
        /// 获取微信小程序用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IResultModel<WechatDetails<Watermark>>> GetUserInfo(WechatMiniAuthParam param)
        {
            ResultModel<WechatDetails<Watermark>> resultModel = new ResultModel<WechatDetails<Watermark>>();

            var url = $"{Code2SessionUrl}?appid={param.AppId}&secret={param.AppSecret}&js_code={param.Code}&grant_type=authorization_code";
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync(url);

            if (string.IsNullOrWhiteSpace(content))
                return resultModel.Failed("参数有误");

            var result = JsonConvert.DeserializeObject<Code2SessionGetResult>(content);
            if (result.ErrCode != 0)
                return resultModel.Failed(result.ErrMessage);

            WechatDetails<Watermark> wechardetails = DeserializeWechatMiniInfo(result.SessionKey, param.Iv, param.EncryptedData);
            wechardetails.watermark.openid = result.OpenId;

            return resultModel.Success(wechardetails);
        }


        /// <summary>
        /// 解析微信小程序encryptedData里的用户信息
        /// </summary>
        /// <param name="aesIv">向量</param>
        /// <param name="encryptedData">encryptedData</param>
        /// <param name="code">加密数据</param>
        /// <returns></returns>
        private WechatDetails<Watermark> DeserializeWechatMiniInfo(string session_key, string aesIv, string encryptedData)
        {
            WechatDetails<Watermark> wecharInfo = null;

            try
            {
                //判断是否是16位 如果不够补0
                //text = tests(text);
                //16进制数据转换成byte
                byte[] encryptedDatas = Convert.FromBase64String(encryptedData);        // strToToHexByte(text);
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = Convert.FromBase64String(session_key);             // Encoding.UTF8.GetBytes(AesKey);
                rijndaelCipher.IV = Convert.FromBase64String(aesIv);                    // Encoding.UTF8.GetBytes(AesIV);
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedDatas, 0, encryptedDatas.Length);
                string results = Encoding.Default.GetString(plainText);

                Console.WriteLine("微信小程序：DeserializeWechatMiniInfo：" + results);

                //序列化获取手机号码
                wecharInfo = JsonConvert.DeserializeObject<WechatDetails<Watermark>>(results);
            }
            catch (Exception ex)
            {
                wecharInfo.Message = ex.Message;
            }
            return wecharInfo;
        }


    }


}
