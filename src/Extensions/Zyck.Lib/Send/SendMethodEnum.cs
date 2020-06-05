using System.ComponentModel;

namespace Zyck.Frame.Extensions
{
    /// <summary>
    /// 消息发送方式
    /// </summary>
    public enum SendMethodEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = -1,
        /// <summary>
        /// 验证码
        /// </summary>
        [Description("手机短信")]
        PhoneSMS
    }
}
