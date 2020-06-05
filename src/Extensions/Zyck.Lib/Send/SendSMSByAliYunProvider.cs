using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zyck.Frame.Extensions
{
    public class SendSMSByAliYunProvider : ISendSMSProvider
    {
        string SignName = "自由创客";
        string accessKeyId = "LTAIQfyV2w9EIZSl";
        string accessSecret = "YQZUxGKt08kuRpw9oKmGgg08zpy2pm";

        /// <summary>
        /// 发送验证码
        /// </summary>
        public SendSMSlogEntity SendSMSVerify(string tenantId, string telephone, string verificationCode)
        {
            var model = new SendSMSlogEntity();
            try
            {
                string TemplateCode = "SMS_159135074";
                IDictionary<string, string> TemplateParam = new Dictionary<string, string>();
                TemplateParam.Add("code", verificationCode);

                model.Id = Guid.NewGuid().ToString();
                model.TenantId = tenantId;
                model.Phone = telephone;
                model.VerificationCode = verificationCode;
                model.CreateTime = DateTime.Now;

                var res = AliYunSendSms(telephone, TemplateCode, TemplateParam, model.Id).Result;
                model.State = res.success;
                model.Response = res.response;
                model.StateCode = "200";
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
        public SendSMSlogEntity SendSMSVerify(string tenantId, string telephone, string verificationCode, string business)
        {
            var model = new SendSMSlogEntity();
            try
            {
                string TemplateCode = "SMS_148860775";

                //您当前正在办理${name}，操作码是${code}，该操作码5分钟内有效，请勿泄漏于他人！
                IDictionary<string, string> TemplateParam = new Dictionary<string, string>();
                TemplateParam.Add("name", business);
                TemplateParam.Add("code", verificationCode);

                model.Id = Guid.NewGuid().ToString();
                model.TenantId = tenantId;
                model.Phone = telephone;
                model.VerificationCode = verificationCode;
                model.CreateTime = DateTime.Now;

                var res = AliYunSendSms(telephone, TemplateCode, TemplateParam, model.Id).Result;
                model.State = res.success;
                model.Response = res.response;
                model.StateCode = "200";
            }
            catch (Exception ex)
            {
                model.State = false;
                model.Remarks = ex.Message;
            }
            return model;
        }

        /// <summary>
        /// 发送业务提醒,含手机短信信息、手机APP端信息
        /// </summary>
        public List<SendSMSlogEntity> SendSMSInfo(string tenantId, string telephone, string business, string msg)
        {
            var modelList = new List<SendSMSlogEntity>();

            string TemplateCode = "SMS_164680880";

            //您的${mtname}申请已于${submittime}审批${type}，特此通知。
            IDictionary<string, string> TemplateParam = new Dictionary<string, string>();
            TemplateParam.Add("mtname", business);
            TemplateParam.Add("submittime", DateTime.Now.ToString());
            TemplateParam.Add("type", msg);

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
                    model.Id = Guid.NewGuid().ToString();
                    model.TenantId = tenantId;
                    model.Phone = phone;
                    model.Remarks = Msg;
                    model.CreateTime = DateTime.Now;

                    var res = AliYunSendSms(phone, TemplateCode, TemplateParam, model.Id).Result;
                    model.State = res.success;
                    model.Response = res.response;

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

        /// <summary>
        /// 阿里云接口发送SMS信息
        /// </summary>
        /// <param name="PhoneNumbers">待发送手机号</param>
        /// <param name="TemplateCode">短信模板</param>
        /// <param name="data">短信模板</param>
        /// <param name="OutId">短信回执消息Id</param>
        private async Task<(bool success, string response)> AliYunSendSms(string PhoneNumbers, string TemplateCode, IDictionary<string, string> data, string OutId)
        {
            var sms = new SmsObject
            {
                Mobile = PhoneNumbers,
                Signature = SignName,
                TempletKey = TemplateCode,
                Data = data,
                OutId = OutId
            };

            var res = await new SendSMSByAliyunHandler(accessKeyId, accessSecret).Send(sms);
            return (res.success, res.response);

            //Debug.WriteLine($"发送结果：{res.success}");
            //Debug.WriteLine($"Response：{res.response}");


            //String product = "Dysmsapi";                                //短信API产品名称（短信产品名固定，无需修改）
            //String domain = "dysmsapi.aliyuncs.com";                    //短信API产品域名（接口地址固定，无需修改）
            //String version = "V1.0";                                    //版本

            //DefaultProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessSecret);
            //IAcsClient client = new DefaultAcsClient(profile);
            //CommonRequest request = new CommonRequest(product, "V1.0", "SendSms");

            //request.Protocol = ProtocolType.HTTPS;
            //request.Method = MethodType.POST;
            //request.QueryParameters.Add("RegionId", "cn-hangzhou");
            //request.QueryParameters.Add("PhoneNumbers", PhoneNumbers);
            //request.QueryParameters.Add("SignName", SignName);
            //request.QueryParameters.Add("TemplateCode", TemplateCode);
            //request.QueryParameters.Add("TemplateParam", TemplateParam);
            //request.QueryParameters.Add("OutId", OutId);
            //try
            //{
            //    CommonResponse response = client.GetAcsResponse(request);
            //    string responseData = response.Data;

            //    return true;
            //}
        }

    }
}


