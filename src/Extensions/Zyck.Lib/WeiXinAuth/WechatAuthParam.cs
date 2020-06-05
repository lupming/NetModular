using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zyck.Frame.Extensions.WeiXinAuth
{
    /// <summary>
    /// 微信小程序鉴权参数
    /// </summary>
    public class WechatMiniAuthParam
    {
        [Required]
        public string AppId { get; set; }

        [Required]
        public string AppSecret { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string EncryptedData { get; set; }

        [Required]
        public string Iv { get; set; }

    }


    /// <summary>
    /// 微信公众号鉴权参数
    /// </summary>
    public class WechatMpAuthParam
    {
        [Required]
        public string AppId { get; set; }

        [Required]
        public string AppSecret { get; set; }

        [Required]
        public string Code { get; set; }

    }

}
