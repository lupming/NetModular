using System;
using System.Collections.Generic;
using System.Linq;

namespace Zyck.Frame.Extensions
{
    public class SendByYunTongXunProvider : ISendSMSProvider
    {
        string AccountSid = "8a216da8679d0e9d0167ac4fe4bc0aca";
        string AccountToken = "14b4b26b79dc4a27ac6e0ceccfec6352";
        string AppId = "8a216da8679d0e9d0167ac4fe5090ad0";

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="companyId">商户ID</param>
        /// <param name="telephone">手机号</param>
        /// <param name="verificationCode">验证码</param>
        /// <returns></returns>
        public SendSMSlogEntity SendSMSVerify(string tenantId, string telephone, string verificationCode)
        {
            //http://www.yuntongxun.com/
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();

            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount(AccountSid, AccountToken);
            api.setAppId(AppId);

            var model = new SendSMSlogEntity();

            if (!isInit)
            {
                model.State = false;
                return model;
            }

            Dictionary<string, object> retData = null;
            try
            {
                string[] phoneList = telephone.Split(new char[] { ',' });

                foreach (string phone in phoneList)
                {
                    if (phone == "" || phone.Length != 11) continue;

                    retData = api.SendTemplateSMS(telephone, "404496", new string[] { verificationCode, "5分钟" });
                    model.Response = getDictionaryData(retData);

                    model.Id = Guid.NewGuid().ToString();
                    model.TenantId = tenantId;
                    model.Phone = telephone;
                    model.VerificationCode = verificationCode;
                    model.CreateTime = DateTime.Now;
                    model.State = true;
                    model.StateCode = "200";
                }
            }
            catch (Exception ex)
            {
                model.State = false;
                model.Remarks = ex.Message;
            }
            return model;
        }

        /// <summary>
        /// 发送验证码-带业务名称
        /// </summary>
        /// <param name="companyId">商户ID</param>
        /// <param name="telephone">手机号</param>
        /// <param name="verificationCode">验证码</param>
        /// <param name="business">业务介绍</param>
        public SendSMSlogEntity SendSMSVerify(string tenantId, string telephone, string verificationCode, string business)
        {
            //http://www.yuntongxun.com/
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();

            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount(AccountSid, AccountToken);
            api.setAppId(AppId);

            var model = new SendSMSlogEntity();

            if (!isInit)
            {
                model.State = false;
                return model;
            }

            Dictionary<string, object> retData = null;
            try
            {
                string[] phoneList = telephone.Split(new char[] { ',' });

                foreach (string phone in phoneList)
                {
                    if (phone == "" || phone.Length != 11) continue;

                    retData = api.SendTemplateSMS(telephone, "318597", new string[] { business, verificationCode });
                    model.Response = getDictionaryData(retData);

                    model.Id = Guid.NewGuid().ToString();
                    model.TenantId = tenantId;
                    model.Phone = telephone;
                    model.VerificationCode = verificationCode;
                    model.CreateTime = DateTime.Now;
                    model.State = true;
                    model.StateCode = "200";
                    model.Remarks = "您" + business + "，验证码是{2}。提示：请勿泄露验证码给他人";
                }
            }
            catch (Exception ex)
            {
                model.State = false;
                model.Remarks = ex.Message;
            }
            return model;
        }

        public List<SendSMSlogEntity> SendSMSInfo(string tenantId, string telephone, string business, string msg)
        {
            var modelList = new List<SendSMSlogEntity>();

            //您的${mtname}申请已于${submittime}审批${type}，特此通知。
            string PhoneNumbers = telephone;
            //string SignName = "自由创客";
            //string TemplateCode = "SMS_148861947";
            string TemplateParam = "{\"mtname\":\"" + business + "\",\"submittime\":\"" + DateTime.Now.ToString() + "\",\"type\":\"" + msg + "\"}";
            string OutId = Guid.NewGuid().ToString();

            string Msg = "您的" + business + "申请已于" + DateTime.Now.ToString() + "审批" + msg + "，特此通知。";

            //发送手机APP端信息
            //SeedMsgHandler.ExecutePush(ref errors, companyId, telephone, Msg);

            string[] phoneList = telephone.Split(new char[] { ',' });
            foreach (string phone in phoneList)
            {
                if (phone == "" || phone.Length != 11) continue;

                SendSMSlogEntity model = new SendSMSlogEntity();
                try
                {
                    model.Id = OutId;
                    model.TenantId = tenantId;
                    model.Phone = phone;
                    model.Remarks = "您的" + business + "申请已于" + DateTime.Now.ToString() + "审批" + msg + "，特此通知。";
                    model.CreateTime = DateTime.Now;
                    //model.State = AliYunSendSms(phone, SignName, TemplateCode, TemplateParam, OutId);
                    model.StateCode = "200";
                }
                catch (Exception ex)
                {
                    model.State = false;
                    model.Remarks = ex.Message;
                }
                modelList.Add(model);
            }
            return modelList;
        }

        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }

    }
}