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
    
    public partial class UserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInfo()
        {
            this.OrderInfo = new HashSet<OrderInfo>();
        }
    
        public int Userid { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string LoginPwd { get; set; }
        public Nullable<int> Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public System.DateTime RegditDate { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
        public string Address { get; set; }
        public int State { get; set; }
    
        public virtual adminState adminState { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInfo> OrderInfo { get; set; }
    }
}