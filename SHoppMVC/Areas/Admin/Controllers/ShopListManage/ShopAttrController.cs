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
   
    public class ShopAttrController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static int ID;//保存属性的ID
        public static List<attribute> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<attribute>(ShopAttrController.listattr);
        }

        /// <summary>
        /// 商品的属性管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopAttr(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var Goodsattr = from a in db.attribute
                            where a.isDelete==0
                            select a;
            ShopAttrController.listattr = Goodsattr.ToList();
            return View(Goodsattr.ToList().ToPagedList(pagenum ?? 1, 5));
        }

        /// <summary>
        /// 属性表的查看
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopAttrSee(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var attr = from a in db.attribute
                       where a.attrId == id
                       select a;
            var firstattr = attr.First();
            return View(firstattr);
        }

        /// <summary>
        /// 属性的删除
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopAttrDelete(int id)
        {
            attribute ab = db.attribute.Find(id);
            ab.isDelete = 1;
            db.SaveChanges();
            return RedirectToAction("ShopAttr");
        }

        /// <summary>
        /// 属性的修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopAttrUpdate(attribute att)
        {

            var updateattr = db.attribute.Find(ShopAttrController.ID);
            updateattr.attrName = att.attrName;
            updateattr.attrValues = att.attrValues;
            updateattr.attrType = Convert.ToByte(Request.Form["attrType"]);
            updateattr.attrIndex = Convert.ToByte(Request.Form["attrIndex"]);
            updateattr.sortOrder = att.sortOrder;
            updateattr.is_linked = Convert.ToByte(Request.Form["is_linked"]);
            updateattr.isDelete = att.isDelete;
            db.SaveChanges();
           
            return RedirectToAction("ShopAttr");
        }

        /// <summary>
        /// 属性的修改的数据查看
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopAttrUpdate(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //是否多选
            List<SelectListItem> attrss= new List<SelectListItem>();
            attrss.Add(new SelectListItem { Text="可以多选",Value="1"});
            attrss.Add(new SelectListItem { Text = "不可多选", Value = "0" });
            ViewData["attrType"] = attrss;

            //是否检索
            List<SelectListItem> attrIndex = new List<SelectListItem>();
            attrIndex.Add(new SelectListItem { Text = "不需要检索", Value = "0" });
            attrIndex.Add(new SelectListItem { Text = "关键字检索", Value = "1" });
            attrIndex.Add(new SelectListItem { Text = "范围检索", Value = "2" });
            ViewData["attrIndex"] = attrIndex;

            //是否关联
            List<SelectListItem> is_linked = new List<SelectListItem>();
            is_linked.Add(new SelectListItem { Text = "不关联", Value = "0" });
            is_linked.Add(new SelectListItem { Text = "关联", Value = "1" });
            ViewData["is_linked"] = is_linked;

            //是否删除下拉列表
            List<SelectListItem> IsDelete = new List<SelectListItem>();
            IsDelete.Add(new SelectListItem { Value = "1", Text = "是" });
            IsDelete.Add(new SelectListItem { Value = "0", Text = "否" });

            ViewData["isDelete"] = IsDelete;

            var attr = from a in db.attribute
                       where a.attrId == id 
                       select a;
            var firstattr = attr.First();
            ShopAttrController.ID = firstattr.attrId;
            return View(firstattr);
        }


        /// <summary>
        /// 属性的新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopAttrAdd(string attrname,string attrvalues,Byte order,int isdelete)
        {
            var updateattr = new attribute();
            updateattr.attrName = attrname;
            updateattr.attrValues = attrvalues;
            updateattr.attrType = Convert.ToByte(Request.Form["attrType"]);
            updateattr.attrIndex = Convert.ToByte(Request.Form["attrIndex"]);
            updateattr.sortOrder = order;
            updateattr.is_linked = Convert.ToByte(Request.Form["is_linked"]);
            updateattr.isDelete = isdelete;
            db.attribute.Add(updateattr);
            db.SaveChanges();
            return RedirectToAction("ShopAttr");
        }


        /// <summary>
        /// 属性的新增
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopAttrAdd()
        {            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }

            //是否删除下拉列表
            List<SelectListItem> IsDelete = new List<SelectListItem>();
            IsDelete.Add(new SelectListItem { Value = "1", Text = "是" });
            IsDelete.Add(new SelectListItem { Value = "0", Text = "否" });

            ViewData["IsDelete"] = IsDelete;

            //是否多选
            List<SelectListItem> attrss = new List<SelectListItem>();
            attrss.Add(new SelectListItem { Text = "可以多选", Value = "1" });
            attrss.Add(new SelectListItem { Text = "不可多选", Value = "0" });
            ViewData["attrType"] = attrss;

            //是否检索
            List<SelectListItem> attrIndex = new List<SelectListItem>();
            attrIndex.Add(new SelectListItem { Text = "不需要检索", Value = "0" });
            attrIndex.Add(new SelectListItem { Text = "关键字检索", Value = "1" });
            attrIndex.Add(new SelectListItem { Text = "范围检索", Value = "2" });
            ViewData["attrIndex"] = attrIndex;

            //是否关联
            List<SelectListItem> is_linked = new List<SelectListItem>();
            is_linked.Add(new SelectListItem { Text = "不关联", Value = "0" });
            is_linked.Add(new SelectListItem { Text = "关联", Value = "1" });
            ViewData["is_linked"] = is_linked;
            return View();
        }
    }
}