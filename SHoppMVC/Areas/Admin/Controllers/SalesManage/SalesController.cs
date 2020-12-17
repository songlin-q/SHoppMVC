using JxcSystem.Views;
using PagedList;
using SHoppMVC.Areas.Admin.Models;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class SalesController : Controller
    {
        ShopEntities db = new ShopEntities();

        public static List<SalesManage> listcux;
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<SalesManage>(SalesController.listcux);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        // GET: Admin/Sales
        [HttpPost]
        public ActionResult Index(int? pageNum,string goodsname, string goodstype)
        {

            var sale = from a in db.Goods
                       join b in db.supplier on a.supplier equals b.ID
                       join c in db.goodsType on a.goodType equals c.ID
                       where a.isShipping == 1 && a.isDelete == 0
                       select new
                       {
                           ID = a.ID,
                           typeNarme = c.typeNarme,
                           goodsName = a.goodsName,
                           supplierName = b.supplierName,
                           marketPrice = a.marketPrice,
                           promotePrice = a.promotePrice,
                           promoteStartDate = a.promoteStartDate,
                           postage = a.postage,
                           goodsNumber = a.goodsNumber,
                           salesVolume = a.salesVolume,
                           isShipping = a.isShipping
                       };
            if (!string.IsNullOrEmpty(goodsname) && !string.IsNullOrEmpty(goodstype))
            {
                sale = sale.Where(a => a.goodsName.Contains(goodsname) && a.typeNarme.Contains(goodstype));
            }
            if (!string.IsNullOrEmpty(goodsname) && string.IsNullOrEmpty(goodstype))
            {
                sale = sale.Where(a => a.goodsName.Contains(goodsname));
            }
            if (string.IsNullOrEmpty(goodsname) && !string.IsNullOrEmpty(goodstype))
            {
                sale = sale.Where(a => a.typeNarme.Contains(goodstype));
            }
            List<SalesManage> dns = new List<SalesManage>();
            foreach (var item in sale.ToList())
            {
                SalesManage d = new SalesManage();
                d.ID = item.ID;
                d.TypeName = item.typeNarme;
                d.goodsName = item.goodsName;
                d.sipplierName = item.supplierName;
                d.marketprice = item.marketPrice;
                d.promoteprice = item.promotePrice;
                d.promoteStartDate = item.promoteStartDate;
                d.postage = item.postage;
                d.goodsNumber = item.goodsNumber;
                d.salesVolume = item.salesVolume;
                d.isShipping = item.isShipping;
                dns.Add(d);
            }

            SalesController.listcux = dns;
            Session["Salesgood"] = dns.ToList();
            return View(dns.ToPagedList(pageNum ?? 1, 4));
        }

        public ActionResult Index(int ? pageNum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            if (Session["Salesgood"]!=null)
            {
                List<SalesManage> listsale = (List<SalesManage>)Session["Salesgood"];
                SalesController.listcux = listsale;
                return View(listsale.ToPagedList(pageNum ?? 1, 4));
            }

                var sale = from a in db.Goods
                           join b in db.supplier on a.supplier equals b.ID
                           join c in db.goodsType on a.goodType equals c.ID
                           where a.isShipping == 1 && a.isDelete == 0
                           select new
                           {
                               ID = a.ID,
                               typeNarme = c.typeNarme,
                               goodsName = a.goodsName,
                               supplierName = b.supplierName,
                               marketPrice = a.marketPrice,
                               promotePrice = a.promotePrice,
                               promoteStartDate = a.promoteStartDate,
                               postage = a.postage,
                               goodsNumber = a.goodsNumber,
                               salesVolume = a.salesVolume,
                               isShipping = a.isShipping
                           };
                List<SalesManage> dns = new List<SalesManage>();
                foreach (var item in sale.ToList())
                {
                SalesManage d = new SalesManage();
                    d.ID = item.ID;
                    d.TypeName = item.typeNarme;
                    d.goodsName = item.goodsName;
                    d.sipplierName = item.supplierName;
                    d.marketprice = item.marketPrice;
                    d.promoteprice = item.promotePrice;
                    d.promoteStartDate = item.promoteStartDate;
                    d.postage = item.postage;
                    d.goodsNumber = item.goodsNumber;
                    d.salesVolume = item.salesVolume;
                    d.isShipping = item.isShipping;
                    dns.Add(d);
                }
                SalesController.listcux = dns;
            return View(dns.ToPagedList(pageNum ?? 1, 4));

        }

        public ActionResult update(int ? ID)
        {

            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var sale = from a in db.Goods
                       where a.isDelete == 0 && a.ID==ID
                       select a;
            db.Configuration.ProxyCreationEnabled = false;//防止循环调用
            return Json(sale);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public ActionResult Add(Goods g)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //修改
            var sale = from a in db.Goods
                       where a.isDelete == 0 && a.ID == g.ID
                       select a;
            Goods result = sale.ToList().LastOrDefault();
            if (g.promotePrice == null)
            {
                g.promotePrice = 0;
            }
            result.promotePrice = g.promotePrice;
            db.Entry<Goods>(result).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Content("更改成功!");
        }

        //删除
        public ActionResult delete(int id)
        {
            //修改
            var sale = from a in db.Goods
                       where a.isDelete == 0 && a.ID == id
                       select a;
            Goods result = sale.ToList().LastOrDefault();
            result.isShipping = 0;
            db.Entry<Goods>(result).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        ////分页
        //public ActionResult PageIndex(int? pageNum)
        //{
        //    var result = from a in db.Goods
        //                 where a.isDelete == 0
        //                 select a;
        //    return View(result.ToList().ToPagedList(pageNum ?? 1,6));
        //}

    }
}