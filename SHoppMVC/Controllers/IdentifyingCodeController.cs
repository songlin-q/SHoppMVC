using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHoppMVC.Models;
using SHoppMVC.PublicClass;

namespace SHoppMVC.Controllers
{
    public class IdentifyingCodeController : Controller
    {
        ShopEntities db = new ShopEntities();
        ShopEntities db1 = new ShopEntities();


        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}


        public ActionResult IdentifyingCode()
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
                var cartcount = db.cart.Where(a => a.userId == LoginController.loginId && a.state == "未购买").ToList().Count;
                ViewBag.cartnum = cartcount;
            }
            return View();
        }
        // GET: IdentifyingCode
        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="Tel"></param>
        /// <param name="YZM"></param>
        /// <param name="NPwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IdentifyingCode(string Tel,string YZM,string NPwd)
        {
            var ui = from a in db.UserInfo
                             where a.Tel == Tel && a.State == 1
                             select a;

            //判断非空
            if (string.IsNullOrEmpty(Tel) || string.IsNullOrEmpty(YZM) || string.IsNullOrEmpty(NPwd))
            {
                Response.Write("<script>alert('电话、验证码、密码不能为空!');</script>");
            }
            else
            {
                string sessioncode = Session["verifyCode"].ToString();
                string thecodeX = YZM.ToLower();
                string sessioncodeX = sessioncode.ToLower();
                if (thecodeX == sessioncodeX)
                {
                    if (ui.ToList().Count > 0)
                    {
                        UserInfo sr = ui.ToList().OrderBy(a => a.Userid).LastOrDefault();
                        string loginName = sr.LoginName;
                        string UTel = sr.Tel;
                        string Address = sr.Address;
                        int ID = sr.Userid;

                        if (UTel==Tel)
                        {
                            UserInfo uii = new UserInfo();
                            uii.Userid = ID;
                            uii.LoginName = sr.LoginName;
                            uii.LoginPwd = NPwd;
                            uii.Tel = UTel;
                            uii.RegditDate = DateTime.Now;
                            uii.Address = Address;
                            uii.State = 1;
                            db1.Entry<UserInfo>(uii).State = System.Data.Entity.EntityState.Modified;
                            if (db1.SaveChanges() > 0)
                            {
                                Response.Write("<script>alert('提交成功')</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('提交失败')</script>");
                            }
                        }
                        
                    }
                    else
                    {
                        Response.Write("<script>alert('电话号码无效!');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('输入的验证码不正确!');</script>");
                }
            }

            return View();
        }
        public ActionResult CreateVerifyCode(string thecode)
        {
            VerifyCode vc = new PublicClass.VerifyCode();
            byte[] result = vc.GetVerifyCode();
            return File(result, "image/jpeg jpeg jpg jpe");
        }

        public string CheckVerifyCode(string thecode)
        {
            string result = "";
            string sessioncode = Session["verifyCode"].ToString();
            string thecodeX = thecode.ToLower();
            string sessioncodeX = sessioncode.ToLower();
            if (thecodeX == sessioncodeX)
            {
                result = "ok";
            }
            else
            {
                result = "no";
            }
            return result;
        }
    }
}