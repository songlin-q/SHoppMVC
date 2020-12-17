//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SHoppMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderInfo
    {
        public int ID { get; set; }
        public string orderSn { get; set; }
        public int userId { get; set; }
        public string goodsSn { get; set; }
        public int orderStatus { get; set; }
        public Nullable<int> shippingStatus { get; set; }
        public string consignee { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string tel { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> bestTime { get; set; }
        public Nullable<int> payState { get; set; }
        public string shippingName { get; set; }
        public int payId { get; set; }
        public Nullable<decimal> goodsAmount { get; set; }
        public Nullable<decimal> shippingFee { get; set; }
        public Nullable<decimal> payFee { get; set; }
        public Nullable<int> buyNumber { get; set; }
        public Nullable<decimal> integralMoney { get; set; }
        public Nullable<decimal> bonus { get; set; }
        public decimal orderAmount { get; set; }
        public string deliverySn { get; set; }
        public System.DateTime addTime { get; set; }
        public Nullable<System.DateTime> confirmTime { get; set; }
        public Nullable<System.DateTime> payTime { get; set; }
        public Nullable<System.DateTime> shippingTime { get; set; }
        public string toBuyer { get; set; }
        public string payNote { get; set; }
        public Nullable<int> is_pickup { get; set; }
        public Nullable<int> shippingEimeEnd { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
    }
}