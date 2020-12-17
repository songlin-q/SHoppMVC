using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        ShopEntities db = new ShopEntities();

        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}

        #region (舒珊珊添加)
        public static int loginId = 0;
        public static string loginname="";
        #endregion

        // GET: Login
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="action1"></param>
        /// <param name="newName"></param>
        /// <param name="newPwd"></param>
        /// <param name="Tel"></param>
        /// <param name="Address"></param>
        /// <returns></returns>
        public ActionResult Index(string action1, string newName, string newPwd, string Tel, string Address)
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
                var cartcount = db.cart.Where(a => a.enable == 0 && a.userId == LoginController.loginId && a.state== "未购买").ToList().Count;
                ViewBag.cartnum = cartcount;
            }
            if (action1 == "注册")
            {
                var admin = from a in db.UserInfo
                            where a.LoginName == newName && a.LoginPwd == newPwd
                            select a;
                //判断非空
                if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(Tel) || string.IsNullOrEmpty(Address))
                {
                    Response.Write("<script>alert('登录名、密码、电话号码、地址均不能为空');</script>");
                }
                //判断是否存在相同用户
                else if (admin.ToList().Count > 0)
                {
                    Response.Write("<script>alert('存在相同用户');</script>");
                }
                else
                {

                    var userInfo = new UserInfo();
                    userInfo.LoginName = newName;
                    userInfo.LoginPwd = newPwd;
                    userInfo.Tel = Tel;
                    userInfo.Address = Address;
                    userInfo.RegditDate = DateTime.Now;
                    userInfo.State = 1;
                    db.Entry<UserInfo>(userInfo).State = System.Data.Entity.EntityState.Added;
                    if (db.SaveChanges() > 0)
                    {
                        Response.Write("<script>alert('注册成功');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('注册失败');</script>");
                    }
                }
            }
            return View();
        }
        /// <summary>
        /// 前台登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string action1, string loginName, string loginPwd, string newName, string newPwd, string Tel, string Address)
        {
            var admin = from a in db.UserInfo
                        where a.LoginName == loginName
                        select a;

            if (action1 == "登录")
            {
                if (admin.ToList().Count > 0)
                {


                    #region 查询用户id(舒珊珊添加)
                    var loginInfo = from x in db.UserInfo
                                    where x.LoginName.Equals(loginName) && x.LoginPwd==loginPwd
                                    select x;

                    #endregion
                    if (loginInfo.ToList().Count() > 0)
                    {
                        var login = loginInfo.ToList().LastOrDefault();
                        LoginController.loginId = login.Userid;
                        LoginController.loginname = login.LoginName;


                        Session["LoginInfo"] = admin;
                        Response.Write("<script>alert('登陆成功');</script>");
                        return RedirectToAction("../Main/Main");
                    }
                    else
                    {
                        return Content("<script>alert('登陆失败');window.location.href='../Login/Index';</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('没有该用户，请注册！');</script>");
                }
            }
            else if (action1 == "注册")
            {
                //判断非空
                if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(Tel) || string.IsNullOrEmpty(Address))
                {
                    Response.Write("<script>alert('登录名、密码、电话号码、地址均不能为空');</script>");
                }
                //判断是否存在相同用户
                else if (admin.ToList().Count > 0)
                {
                    Response.Write("<script>alert('存在相同用户');</script>");
                }
                else
                {
                    var userInfo = new UserInfo();
                    userInfo.LoginName = newName;
                    userInfo.LoginPwd = newPwd;
                    userInfo.Tel = Tel;
                    userInfo.Address = Address;
                    userInfo.RegditDate = DateTime.Now;
                    userInfo.State = 1;
                    db.Entry<UserInfo>(userInfo).State = System.Data.Entity.EntityState.Added;
                    if (db.SaveChanges() > 0)
                    {
                        Response.Write("<script>alert('注册成功');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('注册失败');</script>");
                    }
                }
            }
            return View();
        }
    }
}