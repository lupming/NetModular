using System.ComponentModel.DataAnnotations;

namespace Zyck.Frame.Extensions
{
    /// <summary>
    /// 支付通用模型
    /// </summary>
    public class PaymentViewModel
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double TotalAmount { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 支付完成后回调ApiURL
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 支付完成后返回的URL
        /// </summary>
        public string ReturnUrl { get; set; }

    }

    /// <summary>
    /// 支付回调模型
    /// </summary>
    public class PaymentNotifyViewModel
    {
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 支付订单编号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public string PaymentTime { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public double Money { get; set; }
    }

}
