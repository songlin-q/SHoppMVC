using JxcSystem.Views;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers.QunxianManage
{
    public class QuanxianController : Controller
    {
        ShopEntities db = new ShopEntities();
        ShopEntities db1 = new ShopEntities();

        public static List<AdminInfo> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<AdminInfo>(QuanxianController.listattr);
        }

        //生成编号
        public string newSn()
        {
            var results = from a in db.AdminInfo
                          select a;
            AdminInfo sr = results.ToList().LastOrDefault();
            int num = Convert.ToInt32(sr.ID);
            num += 1;
            if (num < 10)
            {
                return "sys000" + num;
            }
            else if (num >= 10 && num < 100)
            {
                return "00" + num;
            }
            else if (num >= 100 && num <= 1000)
            {
                return "0" + num;
            }
            else
            {
                return num.ToString();
            }
    }
        [HttpPost]
        public ActionResult Add(string LoginName, string UserName, string Tel, string Email, string Password,string Remark)
        {

            string userCode = newSn();
            ////是否存在相同用户名
            var exists = from a in db.AdminInfo
                         where a.userName == UserName
                         select a;
            //判断非空
            if (string.IsNullOrEmpty(LoginName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Tel))
            {
                Response.Write("<script>alert('登录名、密码、真实姓名、电话均不能为空');</script>");
            }
            if (exists.ToList().Count > 0)
            {
                Response.Write("<script>alert('存在相同登录名');</script>");
            }
            else
            {
                var userinfo = new AdminInfo();
                userinfo.userCode = userCode;
                userinfo.userName = LoginName;
                userinfo.realName = UserName;
                userinfo.password = Password;
                userinfo.telephone = Tel;
                userinfo.Email = Email;
                userinfo.Remark = Remark;
                userinfo.state = Convert.ToInt32(Request.Form["sex"]);
                userinfo.roleCode = Convert.ToInt32(Request.Form["Type"]);               
                userinfo.createTime = DateTime.Now;
                userinfo.img = "/Areas/Admin/content/img/avatar-2.jpg";
                db.Entry<AdminInfo>(userinfo).State = System.Data.Entity.EntityState.Added;
                if (db.SaveChanges() > 0)
                {
                    Response.Write("<script>alert('成功');</script>");
                }
                else
                {
                    Response.Write("<script>alert('失败');</script>");
                }
            }

            return RedirectToAction("../Quanxian/Index");
        }

        //增加
        public ActionResult Add()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            AdminInfo ad = (AdminInfo)Session["LoginUser"];
            if (ad.roleCode == 1)
            {
                //绑定下拉列表（状态类型）
                List<SelectListItem> Sex = new List<SelectListItem>();
            Sex.Add(new SelectListItem() { Text = "请选择", Value = " " });
            Sex.Add(new SelectListItem() { Text = "正常", Value = "2" });
            Sex.Add(new SelectListItem() { Text = "失效", Value = "1" });
            ViewData["sex"] = Sex;


            //绑定下拉列表（用户类型）
            List<SelectListItem> Type = new List<SelectListItem>();
            Type.Add(new SelectListItem() { Text = "管理员", Value = "1" });
            Type.Add(new SelectListItem() { Text = "普通用户", Value = "3" });
            ViewData["Type"] = Type;

            return View();
            }
            else
            {
                return Content("<script>alert('你不是管理员，不能对后台员工信息进行新增');window.location.href='../Quanxian/Index'</script>");
            }

        }

        //修改
        [HttpPost]
        public ActionResult Pudate(AdminInfo me,string head)
        {

                HttpPostedFileBase files = Request.Files[0];
                var fileName = files.FileName;
                var filePath = Server.MapPath("~/Areas/Admin/content/img");
                if (fileName != null)
                {

                    var dinfo = new AdminInfo();
                    dinfo.ID = me.ID;
                    dinfo.userCode = me.userCode;
                    dinfo.userName = me.userName;
                    dinfo.realName = me.realName;
                    dinfo.password = me.password;
                    dinfo.telephone = me.telephone;
                    dinfo.Email = me.Email;
                    dinfo.createTime = DateTime.Now;
                    dinfo.state = Convert.ToInt32(Request.Form["sex"]);
                    dinfo.roleCode = Convert.ToInt32(Request.Form["Type"]);
                    dinfo.Remark = me.Remark;
                    if (fileName != "")
                    {
                        files.SaveAs(Path.Combine(filePath, fileName));

                        dinfo.img = "/Areas/Admin/content/img/" + fileName;
                    }
                    else
                    {
                        dinfo.img = head;
                    }


                    db.Entry<AdminInfo>(dinfo).State = System.Data.Entity.EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        Response.Write("<script>alert('修改成功');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('修改失败');</script>");
                    }

                }
                return RedirectToAction("../Quanxian/Index");


        }

        //修改
        public ActionResult Pudate(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            AdminInfo ad = (AdminInfo)Session["LoginUser"];
            if (ad.roleCode == 1)
            {

                //绑定下拉列表（状态类型）
                List<SelectListItem> Sex = new List<SelectListItem>();
            Sex.Add(new SelectListItem() { Text = "正常", Value = "2" });
            Sex.Add(new SelectListItem() { Text = "失效", Value = "1" });
            ViewData["sex"] = Sex;


            //绑定下拉列表（用户类型）
            List<SelectListItem> Type = new List<SelectListItem>();
            Type.Add(new SelectListItem() { Text = "管理员", Value = "1" });
            Type.Add(new SelectListItem() { Text = "普通用户", Value = "3" });
            ViewData["Type"] = Type;

            var product = from a in db.AdminInfo
                          where a.ID == id
                          select a;
            AdminInfo sd = product.ToList().LastOrDefault();
            return View(sd);
            }
            else
            {
                return Content("<script>alert('你不是管理员，不能对后台员工信息进行修改');window.location.href='../Quanxian/Index'</script>");
            }

        }


        //删除

        public ActionResult Delete(int ? id)
        {
            AdminInfo ad = (AdminInfo)Session["LoginUser"];
            if (ad.roleCode == 1)
            {

                var user = db.AdminInfo.Find(id);
                if (user.ID == ad.ID)
                {
                    return Content("<script>alert('你不能删除自己！');history.go(-1);</script>");
                }
                else
                {
                    db.Entry<AdminInfo>(user).State = System.Data.Entity.EntityState.Deleted;
                    if (db.SaveChanges() > 0)
                    {
                        Response.Write("<script>alert('删除成功');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('删除失败');</script>");

                    }
                    return RedirectToAction("../Quanxian/Index");
                }
            }
            else
            {
                return RedirectToAction("../Quanxian/Index");
            }

        }


        //查询后台用户列表
        public ActionResult Index(string Name)
        {

            //判断是否登录
            if (Session["LoginUser"]==null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }

            var admin = from a in db.AdminInfo
                        select a;
            if (Name!=null)
            {
                admin = admin.Where(a => a.realName.Contains(Name) || a.userName.Contains(Name));
                QuanxianController.listattr = admin.ToList();
                  return View(admin);
            }
            else
            {
                QuanxianController.listattr = admin.ToList();
                return View(admin);
            }
            
           
        }
        //权限分配
        [HttpPost]
        public ActionResult QuanXian(int[] firstChks, int id)
        {
            int rid = Convert.ToInt32(Session["RoleId"]);
            var qx = from a in db.Resource
                     where a.ParentNum == ""
                     select a;
            var xq = from a in db.Resource
                     where a.ParentNum != ""
                     select a;

            ViewBag.First = qx.ToList();
            ViewBag.Two = xq.ToList();

            //提交时刷新权限为空，重新保存绑定新的权限
            var aa = from a in db1.RoleRight
                     where a.RoleId == rid
                     select a;
            
           foreach (var item in aa.ToList())
            {
                var ri = new RoleRight();
                ri.ID = item.ID;
                ri.RoleId = item.RoleId;
                ri.ResourceID = item.ResourceID;
                db.Entry<RoleRight>(ri).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }         

            //保存刷新该用户的权限
            for (int i = 0; i < firstChks.Length; i++)
            {
                var rr = new RoleRight();
                rr.RoleId = rid;
                rr.ResourceID = firstChks[i];
                db.Entry<RoleRight>(rr).State = System.Data.Entity.EntityState.Added;
                if (db.SaveChanges()>0)
                {
                    Response.Write("<script>alert('成功');</script>");
                }
                else
                {
                    Response.Write("<script>alert('失败');</script>");
                }
         
               
            }
            return RedirectToAction("../Quanxian/Index");
        }

        //权限分配
        public ActionResult QuanXian(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            AdminInfo ad = (AdminInfo)Session["LoginUser"];
            if (ad.roleCode == 1)
            {
                Session["RoleId"] = id;
            var qx = from a in db.Resource
                     where a.ParentNum==""
                     select a;
            var xq = from a in db.Resource
                     where a.ParentNum != ""
                     select a;

            ViewBag.First = qx.ToList();
            ViewBag.Two = xq.ToList();
            return View();
            }
            else
            {
                return Content("<script>alert('你不是管理员，不能对后台员工信息进行权限分配');window.location.href='../Quanxian/Index'</script>");
            }
        }

     }
}