using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class OurSelfController : Controller
    {
        ShopEntities db = new ShopEntities();

        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}

        // GET: OurSelf
        //修改
        //修改
        [HttpPost]
        public ActionResult Index(UserInfo me)
        {
            var dinfo =db.UserInfo.Find(LoginController.loginId);
            dinfo.Userid = LoginController.loginId;
            dinfo.LoginName = me.LoginName;
            dinfo.UserName = me.UserName;

            var loginuser = db.UserInfo.Where(a => a.Userid == LoginController.loginId).First();
                            
            dinfo.LoginPwd = loginuser.LoginPwd;
            dinfo.Age = me.Age;
            dinfo.Tel = me.Tel;
            dinfo.Email = me.Email;
            dinfo.Address = me.Address;
            dinfo.State =1;
            dinfo.Sex = Convert.ToInt32(Request.Form["sex"]);
            dinfo.RegditDate = DateTime.Now;
            dinfo.BirthDay = me.BirthDay;
            db.Entry<UserInfo>(dinfo).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
               return Content("<script>alert('修改成功');window.location.href='../PersonalCenter/Indexcs'</script>");
            }
            else
            {
                return Content("<script>alert('修改失败');window.location.href='../OurSelf/Index'</script>");
            }
        }

        //修改
        public ActionResult Index(  )
        {
            //显示登陆名称
            if (LoginController.loginname == "")
            {
                ViewBag.login = "未登录";
                ViewBag.cartnum = 0;
                return RedirectToAction("../Login/Index");
            }
            else
            {
                //显示购物车条数
                ViewBag.login = LoginController.loginname;
                var cartcount = db.cart.Where(a => a.userId == LoginController.loginId && a.state == "未购买").ToList().Count;
                ViewBag.cartnum = cartcount;
            }
            //绑定下拉列表（性别）
            List<SelectListItem> Sex = new List<SelectListItem>();
            Sex.Add(new SelectListItem() { Text = "男", Value = "0" });
            Sex.Add(new SelectListItem() { Text = "女", Value = "1" });
            ViewData["sex"] = Sex;


            var product = from a in db.UserInfo
                          where a.Userid == LoginController.loginId
                          select a;
            UserInfo sd = product.ToList().LastOrDefault();
            return View(sd);
        }
    }
}