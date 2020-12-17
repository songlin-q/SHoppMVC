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
    public class ShopTyeManageController : Controller
    {
        ShopEntities db = new ShopEntities();


        public static List<goodsType> listtype;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<goodsType>(ShopTyeManageController.listtype);
        }

        /// <summary>
        /// 商品类别管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopTyeManage(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var Goods = from a in db.goodsType
                        where a.enabled==1
                        select a;

            ShopTyeManageController.listtype = Goods.ToList();
            return View(Goods.ToList().ToPagedList(pagenum ?? 1, 5));
        }

        /// <summary>
        /// 商品类别的新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopTyeManageAdd(string goodtypename)
        {
            var goodtype = new goodsType();
            goodtype.typeNarme = goodtypename;
            goodtype.enabled = Convert.ToByte(Request.Form["enlable"]);
            db.goodsType.Add(goodtype);
            db.SaveChanges();
            return RedirectToAction("ShopTyeManage");
        }


        /// <summary>
        /// 商品类别的新增
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopTyeManageAdd()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            List<SelectListItem> enlable = new List<SelectListItem>();
            enlable.Add(new SelectListItem { Text = "可用", Value = "1" });
            enlable.Add(new SelectListItem { Text = "不可用", Value = "0" });
            ViewData["enlable"] = enlable;
            return View();

        }

        /// <summary>
        /// 商品的修改操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopTyeManageUpdate(goodsType gt)
        {
            var goodstype =db.goodsType.Find(gt.ID);
            goodstype.typeNarme = gt.typeNarme;
            goodstype.enabled = Convert.ToSByte(Request.Form["enabled"]);
            db.SaveChanges();
            return RedirectToAction("ShopTyeManage");

        }


        /// <summary>
        /// 商品的修改的数据显示
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopTyeManageUpdate(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            List<SelectListItem> TypeName = new List<SelectListItem>();
            TypeName.Add(new SelectListItem { Text = "可用", Value = "1" });
            TypeName.Add(new SelectListItem { Text = "不可用", Value = "0" });
            ViewData["TypeName"] = TypeName;


             var goodtyp = from a in db.goodsType
                          where a.ID == id
                          select a;
            var fisrtgood = goodtyp.First();
            return View(fisrtgood);

        }

        /// <summary>
        /// 商品类别的删除
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopTyeManageDelete(int id)
        {
            goodsType deletetype = db.goodsType.Find(id);
            deletetype.enabled = 0;
            db.SaveChanges();
            return RedirectToAction("ShopTyeManage");
        }

        /// <summary>
        /// 商品类别的查看
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopTyeManageSee(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var seetype = from a in db.goodsType
                          where a.ID == id
                          select a;
            var firstsee = seetype.First();
            return View(firstsee);
        }
    }
}