using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class ContactController : Controller
    {
        ShopEntities db = new ShopEntities();
        // GET: Contact

        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}


        [HttpPost]
        public ActionResult Index(string Name, string Email, string subject, string information)
        {

                //验证
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(information))
                {
                    Response.Write("<script>alert('数据不能为空')</script>");
                }
                else
                {

                    //保存
                    var measure = new Contacts();
                    measure.Name = Name;
                    measure.subject = subject;
                    measure.Email = Email;
                    measure.information = information;

                    db.Entry<Contacts>(measure).State = System.Data.Entity.EntityState.Added;
                    if (db.SaveChanges() > 0)
                    {
                        Response.Write("<script>alert('发送成功')</script>");

                    }
                    else
                    {
                        Response.Write("<script>alert('发送失败1')</script>");
                    }

                }
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            //显示登陆名称
            if (LoginController.loginname == "")
            {
                ViewBag.login = "未登录";
                ViewBag.cartnum = 0;
            }
            else
            {
                //显示购物车条数
                ViewBag.login = LoginController.loginname;
                var cartcount = db.cart.Where(a => a.enable == 0 && a.userId == LoginController.loginId && a.state == "未购买").ToList().Count;
                ViewBag.cartnum = cartcount;
            }
            return View();
        }
    }
}