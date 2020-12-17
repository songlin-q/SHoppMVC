
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        ShopEntities db = new ShopEntities();
        ShopEntities db1 = new ShopEntities();
        ShopEntities db2 = new ShopEntities();
        ShopEntities db3 = new ShopEntities();
        // GET: Admin/Login
        /// <summary>
        /// 获得一个新的编码
        /// </summary>
        /// <returns></returns>
        public string NewNum()
        {
            var result = from a in db.AdminInfo
                         select a;
            AdminInfo sr = result.ToList().OrderBy(a => a.ID).LastOrDefault();
            int num = sr.ID;
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
        public ActionResult AdminLogins(string newnum, string action1, string newName, string newPwd, string catchPwd, string userTel)
        {
            newnum = NewNum();
            if (action1 == "注册")
            {
                var adminInfo = from a in db.AdminInfo
                                where a.userName == newName && a.password == newPwd && a.roleCode == 1
                                select a;
                //判断非空
                if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(catchPwd) || string.IsNullOrEmpty(userTel))
                {
                    Response.Write("<script>alert('登录名、密码、电话均不能为空');</script>");
                }
                else if (newPwd == catchPwd)
                {
                    //判断是否存在相同用户
                    if (adminInfo.ToList().Count > 0)
                    {
                        Response.Write("<script>alert('存在相同用户');</script>");
                    }
                    else
                    {
                        var adminInfo1 = new AdminInfo();
                        adminInfo1.userName = newName;
                        adminInfo1.password = newPwd;
                        adminInfo1.userCode = newnum;
                        adminInfo1.telephone = userTel;
                        adminInfo1.realName = "";
                        adminInfo1.createTime = DateTime.Now;
                        adminInfo1.roleCode = 1;
                        adminInfo1.state = 2;
                        db.Entry<AdminInfo>(adminInfo1).State = System.Data.Entity.EntityState.Added;
                        if (db.SaveChanges() > 0)
                        {
                            Response.Write("<script>alert('注册成功');</script>");
                            return RedirectToAction("AdminLogins");
                        }
                        else
                        {
                            Response.Write("<script>alert('注册失败');</script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('两次密码不一致，请检查数据');</script>");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogins(string newnum, string action1, string userName, string userPwd, string newName, string newPwd, string userTel)
        {
            newnum = NewNum();
            var admin = from a in db.AdminInfo
                        where a.userName == userName && a.password == userPwd
                        select a;

            if (action1 == "登录")
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userPwd))
                {
                    Response.Write("<script>alert('用户名、密码不能为空');</script>");
                }
                else
                {
                    if (admin.ToList().Count > 0)
                    {
                        var ad = from b in db.AdminInfo
                                 where b.userName == userName
                                 select b;

                        AdminInfo sr = admin.ToList().OrderBy(a => a.ID).LastOrDefault();
                        Session["LoginUser"] = sr;
                        Response.Write("<script>alert('登陆成功');</script>");
                        return RedirectToAction("Mian");

                    }
                    else
                    {
                        Response.Write("<script>alert('登陆失败');</script>");
                    }
                }
            }
            else if (action1 == "注册")
            {
                //判断非空
                if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(userTel))
                {
                    Response.Write("<script>alert('登录名、密码、电话均不能为空');</script>");
                }
                else
                {
                    //判断是否存在相同用户
                    if (admin.ToList().Count > 0)
                    {
                        Response.Write("<script>alert('存在相同用户');</script>");
                    }
                    else
                    {
                        var adminInfo1 = new AdminInfo();
                        adminInfo1.userName = newName;
                        adminInfo1.realName = newName;
                        adminInfo1.password = newPwd;
                        adminInfo1.userCode = newnum;
                        adminInfo1.telephone = userTel;
                        adminInfo1.realName = "";
                        adminInfo1.createTime = DateTime.Now;
                        adminInfo1.roleCode = 1;
                        adminInfo1.state = 2;
                        adminInfo1.img = "/Areas/Admin/content/img/avatar-2.jpg";
                        db.Entry<AdminInfo>(adminInfo1).State = System.Data.Entity.EntityState.Added;
                        if (db.SaveChanges() > 0)
                        {
                            return Content("<script>alert('注册成功');window.open('AdminLogins');</script>");
                        }
                        else
                        {
                            return Content("<script>alert('注册失败');window.open('AdminLogins');</script>");
                        }
                    }
                }
            }
            return View();
        }
        //主页
        public ActionResult Mian()
        {


            if (Session["LoginUser"] == null)
            {
                Response.Write("<script>alert('请从登录页面进入')</script>");
                return RedirectToAction("AdminLogin");
            }
            int ID = ((AdminInfo)Session["LoginUser"]).ID;
            string name = "";
            var pa = from p in db.Resource
                     where p.ParentNum == ""
                     select p;
            ViewBag.Date = pa.ToList();
            var pages = from p in db1.Resource
                        where p.ParentNum == ""
                        select p;
            //一级菜单
            List<dynamic> dnc = new List<dynamic>();
            foreach (var item in pages.ToList())
            {
                dynamic d = new ExpandoObject();
                d.ID = item.ID;
                d.ResNum = item.ResNum;
                d.ResName = item.ResName;
                d.ParentNum = item.ParentNum;
                d.url = item.url;
                d.img = item.img;
                dnc.Add(d);
                name = item.ResName;
            }
            ViewBag.Data = dnc;

            //二级菜单
            var aa = from p in db2.RoleRight
                     join d in db2.Resource on p.ResourceID equals d.ID
                     join m in db2.AdminInfo on p.RoleId equals m.ID
                     where p.RoleId == ID
                     select new
                     {
                         ID = d.ID,
                         img = d.img,
                         ResNum = d.ResNum,
                         ResName = d.ResName,
                         ParentNum = d.ParentNum,
                         url = d.url,
                     };

            List<dynamic> dsc = new List<dynamic>();
            foreach (var itt in aa.ToList())
            {
                dynamic d = new ExpandoObject();
                d.ID = itt.ID;
                d.ResNum = itt.ResNum;
                d.ResName = itt.ResName;
                d.ParentNum = itt.ParentNum;
                d.url = itt.url;
                d.img = itt.img;
                dsc.Add(d);
            }
            ViewBag.Dete = dsc;

            //绑定图片用户名
            var bb = from e in db3.AdminInfo
                     where e.ID == ID
                     select e;

            AdminInfo sr = bb.ToList().OrderBy(a => a.ID).LastOrDefault();
            ViewBag.Naa = sr.img;
            ViewBag.foot = sr.userName;

            ViewBag.time = DateTime.Now;
            return View();
        }

        //修改自己的账号
        public ActionResult Ourslef()
        {
            int ID = ((AdminInfo)Session["LoginUser"]).ID;
            var admin = from a in db.AdminInfo
                        where a.ID == ID
                        select a;
            return View(admin);
        }
        
        //个人信息修改
        public ActionResult OurslefPudate( )
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

            int id = Convert.ToInt32(Session["LoginUser"]);
            var product = from a in db.AdminInfo
                          where a.ID == id
                          select a;
            AdminInfo sd = product.ToList().LastOrDefault();
            return View(sd);
        }
        [HttpPost]
        public ActionResult OurslefPudate(AdminInfo me, string head)
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
            return RedirectToAction("../AdminLogin/Ourslef");
         
        }
        /// <summary>
        /// 初始化展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ZsMain()
        {
            return View();
        }
    }

   
}