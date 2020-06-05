using System;
using System.Collections.Generic;
using System.Text;

namespace Zyck.Frame.Extensions
{
    public interface ISendSMSProvider
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="companyId">商户ID</param>
        /// <param name="telephone">手机号</param>
        /// <param name="verificationCode">验证码</param>
        /// <returns></returns>
        SendSMSlogEntity SendSMSVerify(string tenantId, string telephone, string verificationCode);

        /// <summary>
        /// 发送验证码-带业务名称
        /// </summary>
        /// <param name="companyId">商户ID</param>
        /// <param name="telephone">手机号</param>
        /// <param name="verificationCode">验证码</param>
        /// <param name="business">业务介绍</param>
        SendSMSlogEntity SendSMSVerify(string tenantId, string telephone, string verificationCode, string business);

        /// <summary>
        /// 发送业务提醒,含手机短信信息、手机APP端信息
        /// </summary>
        /// <param name="companyId">商户ID</param>
        /// <param name="telephone">手机号</param>
        /// <param name="business">业务介绍</param>
        /// <param name="msg">提示信息</param>
        List<SendSMSlogEntity> SendSMSInfo(string tenantId, string telephone, string business, string msg);

    }


}

