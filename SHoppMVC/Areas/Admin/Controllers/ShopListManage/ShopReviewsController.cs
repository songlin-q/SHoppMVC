using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers.ShopListManage
{
    public class ShopReviewsController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static List<massage> listmess;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<massage>(ShopReviewsController.listmess);
        }
        [HttpPost]
        public ActionResult ShopReviewsManage(int? pagenum, string Username)
        {

            if (Username == null)
            {
                Username = "";
            }
            var Goodsmess = from a in db.massage
                            where a.isDelete == 0
                            select a;
            if (Username != "")
            {
                Goodsmess = Goodsmess.Where(a => a.userName.Contains(Username));
            }
            ShopReviewsController.listmess = Goodsmess.ToList();
            Session["Massage"] = Goodsmess.ToList();
            return View(Goodsmess.ToList().ToPagedList(pagenum ?? 1, 5));
        }

        /// <summary>
        /// 评论表的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopReviewsManage(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var Goodsmess = from a in db.massage
                            where a.isDelete == 0
                            select a;


            if (Session["Massage"]!=null)
            {
                var mass=(List<massage>)Session["Massage"];
                ShopReviewsController.listmess = mass.ToList();
                return View(mass.ToList().ToPagedList(pagenum ?? 1, 5));
            }

            ShopReviewsController.listmess = Goodsmess.ToList();

            return View(Goodsmess.ToList().ToPagedList(pagenum ?? 1, 5));

        }



        /// <summary>
        /// 评论表修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopReviewsUpdate(massage mg)
        {
            massage mass = db.massage.Where(a=>a.ID==mg.ID).First();
            mass.massageCode = mg.massageCode;
            mass.userName = mg.userName;
            mass.messageContent = mg.messageContent;
            mass.messageTime = mg.messageTime;
            mass.articleId = mg.articleId;
            mass.isDelete = mg.isDelete;
            db.SaveChanges();
            return RedirectToAction("ShopReviewsManage");
        }


        /// <summary>
        /// 评论表修改
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopReviewsUpdate(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //是否删除下拉列表
            List<SelectListItem> IsDelete = new List<SelectListItem>();
            IsDelete.Add(new SelectListItem { Value = "1", Text = "是" });
            IsDelete.Add(new SelectListItem { Value = "0", Text = "否" });

            ViewData["isDelete"] = IsDelete;


            //文章ID下拉列表
            List<SelectListItem> massage = new List<SelectListItem>();
            foreach (var item in db.article.Where(a => a.isDelete == 0))
            {
                SelectListItem attselect = new SelectListItem();
                attselect.Text =item.subject;
                attselect.Value = item.articleId.ToString();
                massage.Add(attselect);
            } 

            ViewData["articleId"] = massage;

            var Goodsattr = from a in db.massage
                            where a.ID==id
                            select a;
            var first = Goodsattr.First();
            return View(first);
        }

        /// <summary>
        /// 评论表的删除
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopReviewsDelete(int id)
        {
            massage mass = db.massage.Where(a => a.ID == id).First();
            mass.isDelete = 1;
            db.SaveChanges();
            return RedirectToAction("ShopReviewsManage");
        }


        /// <summary>
        /// 评论表的查看
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopReviewsSee(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var Goodsattr = from a in db.massage
                            where a.isDelete == 0
                            select a;
            var first = Goodsattr.First();
            return View(first);
        }
    }
}