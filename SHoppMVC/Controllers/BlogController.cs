using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class BlogController : Controller
    {
        ShopEntities db = new ShopEntities();
        ShopEntities db1 = new ShopEntities();
        public static List<article> articles = new List<article>();
        public static string articleInfo1;

        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}

        /// <summary>
        /// 获得一个新编码
        /// </summary>
        /// <returns></returns>
        public string newNum()
        {
            var results = from a in db.massage
                          select a;
            massage sr = results.ToList().LastOrDefault();
            int num = Convert.ToInt32(sr.massageCode);
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
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <returns></returns>
        public ActionResult Index(string articleInfo)
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
          
            #region

            articleInfo1 = articleInfo;
            //默认显示文章
            //显示文章
            #region
            if (string.IsNullOrEmpty(articleInfo))
            {
                var resultA = from a in db.article
                              where a.isDelete == 0 && a.articleCode == "000001" //获取到这篇文章的文章编码，添加到评论表里
                              select a;
                foreach (var item in resultA.ToList())
                {
                    ViewBag.articleCode = item.articleCode;
                    ViewBag.Author = item.Author;
                    ViewBag.subject = item.subject;
                    ViewBag.publishDate = item.publishDate;
                    ViewBag.content = item.content;
                    ViewBag.picture = item.picture;
                    ViewBag.commentNo = item.commentNo;
                }
            }
            else
            {
                //显示文章
                var resultAA = from a in db.article
                               where a.isDelete == 0 && a.articleCode == articleInfo //获取到这篇文章的文章编码，添加到评论表里
                               select a;
                foreach (var item in resultAA.ToList())
                {
                    ViewBag.articleCode = item.articleCode;
                    ViewBag.Author = item.Author;
                    ViewBag.subject = item.subject;
                    ViewBag.publishDate = item.publishDate;
                    ViewBag.content = item.content;
                    ViewBag.picture = item.picture;
                    ViewBag.isDelete = item.isDelete;
                    ViewBag.commentNo = item.commentNo;
                }
            }

            string code = ViewBag.articleCode;
            string Author = ViewBag.Author;
            string subject = ViewBag.subject;
            DateTime publishDate = Convert.ToDateTime(ViewBag.publishDate);
            string content = ViewBag.content;
            string picture = ViewBag.picture;
            int isDelete = Convert.ToInt32(ViewBag.isDelete);
            #endregion
            var results = from a in db.massage
                          where a.isDelete == 0 && a.articleId == code //获取到这篇文章的文章编码，添加到评论表里
                          orderby a.messageTime descending
                          select a;
            var counts = results.ToList().Count;
            ViewBag.commentNo = counts;

            List<dynamic> dncs = new List<dynamic>();
            foreach (var item in results.ToList())
            {
                dynamic d = new ExpandoObject();
                d.userName = item.userName;
                d.messageTime = item.messageTime;
                d.messageContent = item.messageContent;
                dncs.Add(d);
            }
            ViewBag.Data = dncs;
            #endregion
            return View();

        }
        // GET: Blog
        /// <summary>
        /// 保存评论
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <param name="Name"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string Name, string information, article art)
        {
            try
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

                //显示
                //默认显示文章
                //显示文章
                #region
                if (string.IsNullOrEmpty(articleInfo1))
                {
                    var resultA = from a in db.article
                                  where a.isDelete == 0 && a.articleCode == "000001" //获取到这篇文章的文章编码，添加到评论表里
                                  select a;
                    foreach (var item in resultA.ToList())
                    {
                        ViewBag.articleCode = item.articleCode;
                        ViewBag.Author = item.Author;
                        ViewBag.subject = item.subject;
                        ViewBag.publishDate = item.publishDate;
                        ViewBag.content = item.content;
                        ViewBag.picture = item.picture;
                        ViewBag.commentNo = item.commentNo;
                    }
                }
                else
                {
                    //显示文章
                    var resultAA = from a in db.article
                                   where a.isDelete == 0 && a.articleCode == articleInfo1 //获取到这篇文章的文章编码，添加到评论表里
                                   select a;
                    foreach (var item in resultAA.ToList())
                    {
                        ViewBag.articleCode = item.articleCode;
                        ViewBag.Author = item.Author;
                        ViewBag.subject = item.subject;
                        ViewBag.publishDate = item.publishDate;
                        ViewBag.content = item.content;
                        ViewBag.picture = item.picture;
                        ViewBag.isDelete = item.isDelete;
                        ViewBag.commentNo = item.commentNo;
                    }
                }

                string code = ViewBag.articleCode;
                string Author = ViewBag.Author;
                string subject = ViewBag.subject;
                DateTime publishDate = Convert.ToDateTime(ViewBag.publishDate);
                string content = ViewBag.content;
                string picture = ViewBag.picture;
                int isDelete = Convert.ToInt32(ViewBag.isDelete);
                #endregion

                //显示评论
                #region

                //分组查询和排序出这个文章一共有几条书据

                var results = from a in db.massage
                              where a.isDelete == 0 && a.articleId == articleInfo1 //获取到这篇文章的文章编码，添加到评论表里
                              orderby a.messageTime descending
                              select a;
                var counts = results.ToList().Count;
                ViewBag.commentNo = counts;

                List<dynamic> dncs = new List<dynamic>();
                foreach (var item in results.ToList())
                {
                    dynamic d = new ExpandoObject();
                    d.userName = item.userName;
                    d.messageTime = item.messageTime;
                    d.messageContent = item.messageContent;
                    d.articleId = item.articleId;
                    dncs.Add(d);
                }
                ViewBag.Data = dncs;
                #endregion
                //保存
                #region
                string Sno = newNum();
                //验证
                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(information))
                {
                    return Content("<script>alert('数据不能为空');window.location.href=('/Blog/Index')</script>");
                }
                else
                {
                    //保存评论数据
                    var measure = new massage();
                    measure.massageCode = Sno;
                    measure.userName = Name;
                    measure.messageContent = information;
                    measure.articleId = code;//获取到这篇文章的文章编码，添加到评论表里
                    measure.messageTime = DateTime.Now;
                    measure.isDelete = 0;
                    db.Entry<massage>(measure).State = System.Data.Entity.EntityState.Added;


                    //修改文章评论的条数
                    var ainfo = new article();
                    ainfo.articleCode = code; //获取到这篇文章的文章编码，添加到评论表里
                    ainfo.Author = Author;
                    ainfo.publishDate = Convert.ToDateTime(publishDate);
                    ainfo.content = content;
                    ainfo.subject = subject;
                    ainfo.picture = picture;
                    ainfo.isDelete = isDelete;
                    ainfo.commentNo = (counts + 1);
                    db1.Entry<article>(ainfo).State = System.Data.Entity.EntityState.Modified;

                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Blog/Index");
                    }
                    else
                    {
                        return Content("<script>alert('评论失败');window.location.href('Blog/Index')</script>");
                    }
                }
                #endregion
            }
            catch
            {
                return Content("<script>alert('评论失败,请查看数据是否正确');window.location.href('Blog/Index')</script>");
            }

            //return View(results.ToList());
        }
    }
}