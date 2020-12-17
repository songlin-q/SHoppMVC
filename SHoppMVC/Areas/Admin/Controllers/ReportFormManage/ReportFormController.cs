using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class ReportFormController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static List<article> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<article>(ReportFormController.listattr);
        }

        //生成编号
        public string newSn()
        {
            var results = from a in db.article
                          select a;
            article sr = results.ToList().LastOrDefault();
            int num = Convert.ToInt32(sr.articleCode);
            num += 1;
            if (num < 10)
            {
                return "00000" + num;
            }
            else if (num >= 10 && num < 100)
            {
                return "0000" + num;
            }
            else if (num >= 100 && num < 1000)
            {
                return "000" + num;
            }
            else if (num >= 1000 && num < 10000)
            {
                return "00" + num;
            }
            else if (num >= 10000 && num < 100000)
            {
                return "0" + num;
            }
            else
            {
                return num.ToString();
            }
        }
        [HttpPost]
        public ActionResult Add(string Name, string subject, string content, string Data)
        {
            
              string articleCode = newSn();
                ////是否存在相同文章标题
                var exists = from a in db.article
                             where a.subject == subject
                             select a;
                if (exists.ToList().Count > 0)
                {
                   Response.Write("<script>alert('存在相同文章标题');</script>");           
                }
            //判断非空
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(content))
            {
                Response.Write("<script>alert('发表人、文章标题、文章内容均不能为空');</script>");
            }
            else
                {
                    var Al = new article();
                    Al.articleCode = articleCode;
                    Al.Author = Name;
                    Al.content = content;
                    Al.subject = subject;
                    Al.publishDate = DateTime.Now;
                    Al.commentNo = 0;
                    Al.picture = "/Areas/Admin/content/img/avatar-2.jpg";
                    Al.isDelete = Convert.ToInt32(Request.Form["sex"]);
                    db.Entry<article>(Al).State = System.Data.Entity.EntityState.Added;
                    if (db.SaveChanges() > 0)
                    {
                       Response.Write("<script>alert('增加成功');</script>");

                    }
                    else
                    {
                       Response.Write("<script>alert('增加失败');</script>");
                     }
                }
           
            return RedirectToAction("../ReportForm/Index");
        }

        //增加
        public ActionResult Add()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //绑定下拉列表（状态）
            List<SelectListItem> Sex = new List<SelectListItem>();
            Sex.Add(new SelectListItem() { Text = "显示", Value = "0" });
            Sex.Add(new SelectListItem() { Text = "隐藏", Value = "1" });
            ViewData["sex"] = Sex;
            return View( );
        }

        //修改
        [HttpPost]
        public ActionResult Pudate(article me,string head)
        {
            HttpPostedFileBase files = Request.Files[0];
            var fileName = files.FileName;
            var filePath = Server.MapPath("~/Areas/Admin/content/img");
            if (fileName != null)
            {
                var al = new article();
                al.articleId = me.articleId;
                al.articleCode = me.articleCode;
                al.Author = me.Author;
                al.content = me.content;
                al.subject = me.subject;
                al.isDelete = Convert.ToInt32(Request.Form["sex"]);
                al.publishDate = DateTime.Now;
                al.commentNo = me.commentNo;
                if (fileName != "")
                {
                    files.SaveAs(Path.Combine(filePath, fileName));

                    al.picture = "/Areas/Admin/content/img/" + fileName;
                }
                else
                {
                    al.picture=head;
                }


                db.Entry<article>(al).State = System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Response.Write("<script>alert('修改成功');</script>");
                }
                else
                {
                    Response.Write("<script>alert('修改失败');</script>");
                }

            }          
            return RedirectToAction("../ReportForm/Index");
        }

        //修改
        public ActionResult Pudate(string id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //绑定下拉列表（状态）
            List<SelectListItem> Sex = new List<SelectListItem>();
            Sex.Add(new SelectListItem() { Text = "显示", Value = "0" });
            Sex.Add(new SelectListItem() { Text = "隐藏", Value = "1" });
            ViewData["sex"] = Sex;
            var product = from a in db.article
                          where a.articleCode == id
                          select a;
            article sd = product.ToList().LastOrDefault();         
            return View(sd);
        }


        //删除
        public ActionResult Delete(int id)
        {
            var artu =db.article.Find(id);
            artu.isDelete = 1;
            db.Entry<article>(artu).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                Response.Write("<script>alert('删除成功');</script>");
            }
            else
            {
                Response.Write("<script>alert('删除失败');</script>");
            }
            return RedirectToAction("../ReportForm/Index");
         
        }
       //角色查询
        public ActionResult Index(string Name, int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var result = from a in db.article
                         where a.isDelete==0
                         select a;

            if (Name != null)
            {
                result = result.Where(a => a.Author.Contains(Name) || a.subject.Contains(Name));
                ReportFormController.listattr = result.ToList();
                return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
            }
            else
            {
                ReportFormController.listattr = result.ToList();
                return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
            }
          
              
        }

    }

}

