using System.ComponentModel;

namespace Zyck.Frame.Extensions
{
    /// <summary>
    /// 消息发送类型
    /// </summary>
    public enum SendCategoryEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 验证码
        /// </summary>
        [Description("验证码")]
        PhoneVerifyCode
    }
}
