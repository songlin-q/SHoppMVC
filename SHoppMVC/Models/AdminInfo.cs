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
    
    public partial class AdminInfo
    {
        public int ID { get; set; }
        public string userCode { get; set; }
        public string userName { get; set; }
        public string realName { get; set; }
        public string password { get; set; }
        public string telephone { get; set; }
        public string Email { get; set; }
        public System.DateTime createTime { get; set; }
        public int state { get; set; }
        public int roleCode { get; set; }
        public string Remark { get; set; }
        public string img { get; set; }
    
        public virtual adminState adminState { get; set; }
        public virtual role role { get; set; }
    }
}
