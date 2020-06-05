using System;

namespace Zyck.Frame.Extensions
{
    /// <summary>
    /// 短信发送记录
    /// </summary>
    public class SendSMSlogEntity
    {
        /// <summary>
        /// 唯一ID    
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// 类别-
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// 返回JSon
        /// </summary>
        public string Response { get; set; }

    }
}
