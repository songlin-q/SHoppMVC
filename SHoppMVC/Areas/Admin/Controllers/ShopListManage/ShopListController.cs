using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class ShopListController : Controller
    {
        
        ShopEntities db = new ShopEntities();
        public static List<Goods> litgood;
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<Goods>(ShopListController.litgood);
        }


        public ActionResult Innnn()
        {
            return View();

        }
        [HttpPost]
        public ActionResult ShopManage(int? pagenum,string salenums)
        {
            if (salenums == null)
            {
                salenums = "";
            }
            //是否促销
            List<SelectListItem> isshoping = new List<SelectListItem>();
            isshoping.Add(new SelectListItem { Text = "促销", Value = "2" });
            isshoping.Add(new SelectListItem { Text = "不促销", Value = "1" });
            ViewData["isshoping"] = isshoping;


            //是否包邮
            List<SelectListItem> baoyou = new List<SelectListItem>();
            baoyou.Add(new SelectListItem { Text = "包邮", Value = "2" });
            baoyou.Add(new SelectListItem { Text = "不包邮", Value = "1" });
            ViewData["Isbaoyou"] = baoyou;
            //商品类型
            List<SelectListItem> unit = new List<SelectListItem>();

            foreach (var item in db.goodsType.Where(a => a.enabled == 1))
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.typeNarme;
                selectss.Value = item.ID.ToString();
                unit.Add(selectss);
            }

            ViewData["GoodsType"] = unit;


            //供应商下拉列表
            List<SelectListItem> supplier = new List<SelectListItem>();

            foreach (var item in db.supplier)
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.supplierName;
                selectss.Value = item.ID.ToString();
                supplier.Add(selectss);
            }

            ViewData["supper"] = supplier;


            var Goods = from a in db.Goods
                        where a.isDelete == 0
                        select a;
            int Isbaoyou = Convert.ToInt32(Request.Form["Isbaoyou"]);

            if (Isbaoyou > 0)
            {
                Goods = Goods.Where(a => a.isPromote == Isbaoyou);
            }

            if (salenums != "")
            {
                Goods = Goods.Where(a => a.salesVolume == Convert.ToInt32(salenums));
            }

            int isshopings = Convert.ToInt32(Request.Form["isshoping"]);
            if (isshopings > 0)
            {
                Goods = Goods.Where(a => a.isShipping == isshopings);
            }
            int GoodsType = Convert.ToInt32(Request.Form["GoodsType"]);
            if (GoodsType > 0)
            {
                Goods = Goods.Where(a => a.goodType == GoodsType);
            }
            int supper = Convert.ToInt32(Request.Form["supper"]);
            if (supper > 0)
            {
                Goods = Goods.Where(a => a.supplier == supper);
            }
            Session["search"] = Goods.ToList();
            ShopListController.litgood = Goods.ToList();
            return View(Goods.ToList().ToPagedList(pagenum ?? 1, 5));
        }

        /// <summary>
        /// 商品管理
        /// </summary>
        /// <returns></returns>

        public ActionResult ShopManage(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //是否促销
            List<SelectListItem> isshoping = new List<SelectListItem>();
            isshoping.Add(new SelectListItem { Text = "促销", Value = "2" });
            isshoping.Add(new SelectListItem { Text = "不促销", Value = "1" });
            ViewData["isshoping"] = isshoping;


            //是否包邮
            List<SelectListItem> baoyou = new List<SelectListItem>();
            baoyou.Add(new SelectListItem { Text="包邮",Value="2"});
            baoyou.Add(new SelectListItem { Text = "不包邮", Value = "1" });
            ViewData["Isbaoyou"] = baoyou;
            //商品类型
            List<SelectListItem> unit = new List<SelectListItem>();

            foreach (var item in db.goodsType.Where(a=>a.enabled==1))
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.typeNarme;
                selectss.Value = item.ID.ToString();
                unit.Add(selectss);
            }

            ViewData["GoodsType"] = unit;


            //供应商下拉列表
            List<SelectListItem> supplier = new List<SelectListItem>();

            foreach (var item in db.supplier)
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.supplierName;
                selectss.Value = item.ID.ToString();
                supplier.Add(selectss);
            }

            ViewData["supper"] = supplier;


            if (Session["search"] !=null)
            {
                var good = (List<Goods>)Session["search"];
                ShopListController.litgood = good.ToList();
                return View(good.ToList().ToPagedList(pagenum ?? 1, 5));
            
            }

            var Goods = from a in db.Goods
                        where a.isDelete==0
                        select a;
         
            ShopListController.litgood = Goods.ToList();
            return View(Goods.ToList().ToPagedList(pagenum ?? 1,5));
        }


        /// <summary>
        /// 修改数据的按钮
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopManageupdate(Goods gs)
        {

            var goods = db.Goods.Find(gs.ID);
            goods.goodType= Convert.ToInt32(Request.Form["goodType"]);
            goods.goodsName = gs.goodsName;
            goods.clickCount = gs.clickCount;
            goods.supplier = Convert.ToInt32(Request.Form["supplier"]);
            goods.goodsNumber = gs.goodsNumber;
            goods.goodsWeight = gs.goodsWeight;
            goods.marketPrice = gs.marketPrice;
            goods.promotePrice = gs.promotePrice;
            goods.promoteStartDate = gs.promoteStartDate;
            goods.keywords = gs.keywords;
            goods.goodsDesc = gs.goodsDesc;
            goods.isOnSale = gs.isOnSale;
            goods.isShipping = gs.isShipping;
            goods.addTime =gs.addTime;
            goods.isDelete = gs.isDelete;
            goods.isPromote = gs.isPromote;
            goods.lastUpdate = gs.lastUpdate;
            goods.postage = gs.postage;
            goods.salesVolume = gs.salesVolume;
            goods.attrId = gs.attrId;
            db.SaveChanges();
            return RedirectToAction("ShopManage");
        }


        /// <summary>
        /// 修改查询数据的按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopManageupdate(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //商品类型
            List<SelectListItem> unit = new List<SelectListItem>();

            foreach (var item in db.goodsType.Where(a => a.enabled == 1))
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.typeNarme;
                selectss.Value = item.ID.ToString();
                unit.Add(selectss);
            }

            ViewData["GoodsType"] = unit;


            //供应商下拉列表
            List<SelectListItem> supplier = new List<SelectListItem>();

            foreach (var item in db.supplier)
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.supplierName;
                selectss.Value = item.ID.ToString();
                supplier.Add(selectss);
            }

            ViewData["supper"] = supplier;

            //是否促销下拉列表
            List<SelectListItem> isShipping = new List<SelectListItem>();
            isShipping.Add(new SelectListItem { Value = "1", Text = "是" });
            isShipping.Add(new SelectListItem { Value = "2", Text = "否" });
            ViewData["isShipping"] = isShipping;

            //是否上架下拉列表

            List<SelectListItem> isOnSale = new List<SelectListItem>();
            isOnSale.Add(new SelectListItem { Value = "1", Text = "是" });
            isOnSale.Add(new SelectListItem { Value = "0", Text = "否" });

            ViewData["isOnSale"] = isOnSale;

            //是否包邮下拉列表
            List<SelectListItem> isPromote = new List<SelectListItem>();
            isPromote.Add(new SelectListItem { Value = "1", Text = "是" });
            isPromote.Add(new SelectListItem { Value = "2", Text = "否" });

            ViewData["isPromote"] = isPromote;

            //是否删除下拉列表
            List<SelectListItem> IsDelete = new List<SelectListItem>();
            IsDelete.Add(new SelectListItem { Value = "1", Text = "是" });
            IsDelete.Add(new SelectListItem { Value = "0", Text = "否" });

            ViewData["isDelete"] = IsDelete;

            var GoodsType = from a in db.Goods
                            where a.ID == id
                            select a;
            var first = GoodsType.First();
            return View(first);
        }

        /// <summary>
        /// 删除数据信息的按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShopManagedelete(int id)
        {
            Goods good = db.Goods.Find(id);
            good.isDelete = 1;
            db.SaveChanges();
            return RedirectToAction("ShopManage");
        }

        /// <summary>
        /// 自动生成商品编号
        /// </summary>
        public string GoodsNum()
        {
            if (Convert.ToInt32(Request.Form["GoodsType"]) == 2)
            {
                var goodsnum = from a in db.Goods
                               where a.goodType == 2
                               orderby a.ID descending
                               select a;
                Goods gs = goodsnum.First();

                var qiannum = gs.goodsSn.Substring(0, 4);
                var hounum = gs.goodsSn.Substring(4, 2);
                if (qiannum == "s_00")
                {

                    if (hounum.Substring(0, 1) == "0")
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                        hounum = "0" + hounum;

                    }
                    else
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                    }
                    return qiannum + hounum;
                }
                else
                {
                    return "";
                }
            }
            else if (Convert.ToInt32(Request.Form["GoodsType"]) == 3)
            {
                var sgnum = from a in db.Goods
                               where a.goodType == 3
                               orderby a.ID descending
                               select a;
                Goods sg = sgnum.First();

                var qiannum = sg.goodsSn.Substring(0, 4);
                var hounum = sg.goodsSn.Substring(4, 2);
                if (qiannum == "g_00")
                {

                    if (hounum.Substring(0, 1) == "0")
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                        hounum = "0" + hounum;

                    }
                    else
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                    }
                    return qiannum + hounum;
                }
                else
                {
                    return "";
                }

            }
            else if (Convert.ToInt32(Request.Form["GoodsType"]) == 4)
            {
                var hxnum = from a in db.Goods
                               where a.goodType == 4
                               orderby a.ID descending
                               select a;
                Goods hx = hxnum.First();

                var qiannum = hx.goodsSn.Substring(0, 4);
                var hounum = hx.goodsSn.Substring(4, 2);
                if (qiannum == "y_00")
                {

                    if (hounum.Substring(0, 1) == "0")
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                        hounum = "0" + hounum;

                    }
                    else
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                    }
                    return qiannum + hounum;
                }
                else
                {
                    return "";
                }

            }
            else
            {
                var xrsnum = from a in db.Goods
                               where a.goodType == 5
                               orderby a.ID descending
                               select a;
                Goods xr = xrsnum.First();

                var qiannum = xr.goodsSn.Substring(0, 4);
                var hounum = xr.goodsSn.Substring(4, 2);
                if (qiannum == "r_00")
                {

                    if (hounum.Substring(0, 1) == "0")
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                        hounum = "0" + hounum;

                    }
                    else
                    {
                        hounum = (Convert.ToInt32(hounum) + 1).ToString();
                    }
                    return qiannum + hounum;
                }
                else
                {
                    return "";
                }

            }
        }


        /// <summary>
        /// 新增数据的按钮
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShopManageadd(string IsDelete, string Goodsname,string GoodsNUm,decimal GoodsWeight,decimal makeprice,int isShipping,decimal promoteprice,DateTime promoteStartdate,int isOnSale,int isPromote,int salesNum,string clickcount,string KeyWord,DateTime AddDate,DateTime LastUpdateDate,decimal Postage)
        {

            var AddGodds = new Goods();
            AddGodds.goodType = Convert.ToInt32(Request.Form["GoodsType"]);
            AddGodds.goodsName = Goodsname;
            AddGodds.goodsSn = GoodsNum();
            AddGodds.supplier= Convert.ToInt32(Request.Form["supper"]);
            AddGodds.goodsNumber = Convert.ToInt32(GoodsNUm);
            AddGodds.goodsWeight = GoodsWeight;
            AddGodds.marketPrice = makeprice;
            AddGodds.isShipping = isShipping;
            AddGodds.promotePrice = promoteprice;
            AddGodds.promoteStartDate = promoteStartdate;
            AddGodds.isDelete = Convert.ToInt32(IsDelete);
            AddGodds.isOnSale = isOnSale;
            AddGodds.isPromote = isPromote;
            AddGodds.salesVolume = salesNum;
            AddGodds.clickCount = Convert.ToInt32(clickcount);
            AddGodds.keywords = KeyWord;
            AddGodds.attrId = Convert.ToInt32(Request.Form["attr"]);
            AddGodds.addTime = AddDate;
            AddGodds.lastUpdate =LastUpdateDate;
            AddGodds.postage = Postage;
            db.Goods.Add(AddGodds);
            db.SaveChanges();

            return RedirectToAction("ShopManage");
        }


        /// <summary>
        /// 新增数据的按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopManageadd()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //商品类型
            List<SelectListItem> unit = new List<SelectListItem>();

            foreach (var item in db.goodsType.Where(a=>a.enabled==1))
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.typeNarme;
                selectss.Value = item.ID.ToString();
                unit.Add(selectss);
            }

            ViewData["GoodsType"] = unit;


            //供应商下拉列表
            List<SelectListItem> supplier = new List<SelectListItem>();

            foreach (var item in db.supplier)
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.supplierName;
                selectss.Value = item.ID.ToString();
                supplier.Add(selectss);
            }

            ViewData["supper"] = supplier;


            //商品属性下拉列表
            List<SelectListItem> attr = new List<SelectListItem>();

            foreach (var item in db.attribute.Where(a=>a.isDelete==0))
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.attrName;
                selectss.Value = item.attrId.ToString();
                attr.Add(selectss);
            }

            ViewData["attr"] = attr;

            //是否促销下拉列表
            List<SelectListItem> isShipping = new List<SelectListItem>();
            isShipping.Add(new SelectListItem { Value="1",Text="是"});
            isShipping.Add(new SelectListItem { Value = "2", Text = "否" });
            ViewData["isShipping"] = isShipping;

            //是否上架下拉列表

            List<SelectListItem> isOnSale = new List<SelectListItem>();
            isOnSale.Add(new SelectListItem { Value = "1", Text = "是" });
            isOnSale.Add(new SelectListItem { Value = "0", Text = "否" });

            ViewData["isOnSale"] = isOnSale;

            //是否包邮下拉列表
            List<SelectListItem> isPromote = new List<SelectListItem>();
            isPromote.Add(new SelectListItem { Value = "1", Text = "是" });
            isPromote.Add(new SelectListItem { Value = "2", Text = "否" });

            ViewData["isPromote"] = isPromote;

            //是否删除下拉列表
            List<SelectListItem> IsDelete = new List<SelectListItem>();
            IsDelete.Add(new SelectListItem { Value = "1", Text = "是" });
            IsDelete.Add(new SelectListItem { Value = "0", Text = "否" });

            ViewData["IsDelete"] = IsDelete;

            return View();
        }


        /// <summary>
        /// 查看数据信息的按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopManageSee(int ? id)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var goodsInfo = from a in db.Goods
                            where a.ID == id
                            select a;
            var firstgood = goodsInfo.First();
            return View(firstgood);
        }
    }
}