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
    
    public partial class cart
    {
        public int ID { get; set; }
        public int userId { get; set; }
        public string goodsSn { get; set; }
        public string goodsName { get; set; }
        public decimal marketPrice { get; set; }
        public Nullable<decimal> postage { get; set; }
        public int buyNumber { get; set; }
        public decimal totalPrice { get; set; }
        public Nullable<int> goodsType { get; set; }
        public Nullable<int> isGift { get; set; }
        public Nullable<int> isShipping { get; set; }
        public Nullable<int> shippingNum { get; set; }
        public Nullable<int> attrId { get; set; }
        public System.DateTime addTime { get; set; }
        public Nullable<decimal> promotePrice { get; set; }
        public string thumbUrl { get; set; }
        public int enable { get; set; }
        public string state { get; set; }
    }
}