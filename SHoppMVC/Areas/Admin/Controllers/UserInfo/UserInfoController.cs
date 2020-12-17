using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using PagedList;
using SHoppMVC.Models;
using JxcSystem.Views;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class UserInfoController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static List<UserInfo> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<UserInfo>(UserInfoController.listattr);
        }
        public ActionResult userIn(int? PageNum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var userin = from a in db.UserInfo
                         where a.State == 1
                         select a;
            UserInfoController.listattr = userin.ToList();
            return View(userin.ToList().ToPagedList(PageNum ?? 1, 5));
        }
    }
}