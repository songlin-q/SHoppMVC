using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers.GoodsPlayManage
{
    public class GoodsPlayController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static List<supplier> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<supplier>(GoodsPlayController.listattr);
        }

        //生成编号
        public string newSn()
        {
            var results = from a in db.supplier
                          select a;
            supplier sr = results.ToList().LastOrDefault();
            int num = Convert.ToInt32(sr.ID);
            num += 1;
            if (num < 10)
            {
                return "s_00" + num;
            }
            else if (num >= 10 && num < 100)
            {
                return "s_0" + num;
            }
            else if (num >= 100 && num < 1000)
            {
                return "s_" + num;
            }
            else if (num >= 1000 && num < 10000)
            {
                return "r_" + num;
            }
            else if (num >= 10000 && num < 100000)
            {
                return "h_" + num;
            }
            else
            {
                return num.ToString();
            }
        }
        //删除
        public ActionResult Delete(int id)
        {
            var artu = db.supplier.Find(id);
            db.Entry<supplier>(artu).State = System.Data.Entity.EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                Response.Write("<script>alert('修改失败');</script>");
            }
            else
            {
                Response.Write("<script>alert('删除失败');</script>");
            }
            return RedirectToAction("../GoodsPlay/Index");

        }
        //修改
        [HttpPost]
        public ActionResult Pudate(supplier me)
        {

            var al = new supplier();
            al.ID = me.ID;
            al.supplierCode = me.supplierCode;
            al.supplierName = me.supplierName;
            al.companyName = me.companyName;
            al.address = me.address;
            al.tel =me.tel;
            al.email = me.email;
            al.guimo = me.guimo;
            al.Remark = me.Remark;

            db.Entry<supplier>(al).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                Response.Write("<script>alert('修改成功');</script>");

            }
            else
            {
                Response.Write("<script>alert('修改失败');</script>");
            }
            return RedirectToAction("../GoodsPlay/Index");
        }
        //修改
        public ActionResult Pudate(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var product = from a in db.supplier
                          where  a.ID==id
                          select a;
            supplier sd = product.ToList().LastOrDefault();
            return View(sd);
        }

        //供应商新增
        //增加
        [HttpPost]
        public ActionResult Add(string Name, string name, string content, string tel,string email,string remark,string adress)
        {

            string supplierCode = newSn();
            ////是否存在相同供应商
            var exists = from a in db.supplier
                         where a.supplierName == Name
                         select a;
            if (exists.ToList().Count > 0)
            {
                Response.Write("<script>alert('存在相同供应商');</script>");
            }
            //判断非空
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                Response.Write("<script>alert('供应商，公司，邮箱均不能为空');</script>");
            }
            else
            {
                var Al = new supplier();
                Al.supplierCode = supplierCode;
                Al.supplierName = Name;
                Al.companyName = name;
                Al.address =adress;
                Al.tel =tel;
                Al.email = email;
                Al.guimo =content;
                Al.Remark = remark;
                db.Entry<supplier>(Al).State = System.Data.Entity.EntityState.Added;
                if (db.SaveChanges() > 0)
                {
                    Response.Write("<script>alert('增加成功');</script>");

                }
                else
                {
                    Response.Write("<script>alert('增加失败');</script>");
                }
            }

            return RedirectToAction("../GoodsPlay/Index");
        }
        public ActionResult Add()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            return View();
        }

        //供应商查询
        public ActionResult Index(string Name, int? pagenum)
        {

            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }

            var result = from a in db.supplier
                         select a;
            if (Name != null)
            {
                result = result.Where(a => a.supplierName.Contains(Name) || a.companyName.Contains(Name));
                GoodsPlayController.listattr = result.ToList();
                return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
            }
            else
            {
                GoodsPlayController.listattr = result.ToList();
                return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
            }

          
        }
      }
}