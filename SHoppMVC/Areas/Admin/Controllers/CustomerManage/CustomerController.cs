using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        ShopEntities db = new ShopEntities();

        public static List<UserInfo> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<UserInfo>(CustomerController.listattr);
        }

        //修改
        [HttpPost]
        public ActionResult Pudate(UserInfo me)
        {
            var dinfo = new UserInfo();
            dinfo.Userid = me.Userid;
            dinfo.LoginName = me.LoginName;
            dinfo.UserName = me.UserName;
            dinfo.LoginPwd = me.LoginPwd;
            dinfo.Age = me.Age;
            dinfo.Tel = me.Tel;
            dinfo.Email = me.Email;
            dinfo.Address = me.Address;
            dinfo.State = Convert.ToInt32(Request.Form["Type"]);
            dinfo.Sex = Convert.ToInt32(Request.Form["sex"]);
            dinfo.RegditDate = DateTime.Now;
            dinfo.BirthDay = me.BirthDay;
            db.Entry<UserInfo>(dinfo).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                Response.Write("<script>alert('修改成功');</script>");
              
            }
            else
            {
                Response.Write("<script>alert('修改失败');</script>");
            }
            return RedirectToAction("../Customer/Index");
        }

        //修改
        public ActionResult Pudate(int? id)
        {

            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //绑定下拉列表（性别）
            List<SelectListItem> Sex = new List<SelectListItem>();
            Sex.Add(new SelectListItem() { Text = "请选择", Value = " " });
            Sex.Add(new SelectListItem() { Text = "男", Value = "0" });
            Sex.Add(new SelectListItem() { Text = "女", Value = "1" });
            ViewData["sex"] = Sex;


            //绑定下拉列表（状态类型）
            List<SelectListItem> Type = new List<SelectListItem>();
            Type.Add(new SelectListItem() { Text = "请选择", Value = " " });
            Type.Add(new SelectListItem() { Text = "正常", Value = "1" });
            Type.Add(new SelectListItem() { Text = "失效", Value = "0" });
            ViewData["Type"] = Type;

            var product = from a in db.UserInfo
                          where a.Userid == id
                          select a;
            UserInfo sd = product.ToList().LastOrDefault();
            return View(sd);
        }


        //删除
        public ActionResult Delete(int id)
        {
            var user = db.UserInfo.Find(id);
            db.Entry<UserInfo>(user).State = System.Data.Entity.EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                Response.Write("<script>alert('删除成功');</script>");
              
            }
            else
            {
                Response.Write("<script>alert('删除失败');</script>");
           
            }

            return RedirectToAction("../Customer/Index");

        }

        //角色查询
        public ActionResult Index(string Name, int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var result = from a in db.UserInfo
                         select a;
            if (Name != null)
            {
                result = result.Where(a => a.UserName.Contains(Name) || a.LoginName.Contains(Name));
                CustomerController.listattr = result.ToList();
                return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
            }
            else
            {
                CustomerController.listattr = result.ToList();
                return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
            }         

        }
    }
}