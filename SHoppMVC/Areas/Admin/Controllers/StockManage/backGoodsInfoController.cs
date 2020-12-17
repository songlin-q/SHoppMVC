using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHoppMVC.Models;
using JxcSystem.Views;

namespace SHoppMVC.Areas.Admin.Controllers.StockManage
{
    public class backGoodsInfoController : Controller
    {
        private ShopEntities db = new ShopEntities();


        public static List<backGoods> litbackgood;
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<backGoods>(backGoodsInfoController.litbackgood);
        }


        // GET: Admin/backGoods
        public ActionResult Index()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            backGoodsInfoController.litbackgood = db.backGoods.ToList();
            return View(db.backGoods.ToList());
        }
        [HttpPost]
        public ActionResult Index(string inTypes,string ordercode)
        {
            //搜索
            var results = from a in db.backGoods
                          select a;
            if (!string.IsNullOrEmpty(inTypes) && inTypes != "2")
            {
                int inType = Convert.ToInt32(inTypes);
                results = results.Where(a => a.States.Equals(inType));
            }
            if (!string.IsNullOrEmpty(ordercode))
            {
                results = results.Where(a => a.goodsCode.Contains(ordercode));
            }
            backGoodsInfoController.litbackgood = results.ToList();
            return View(results.ToList());
        }
       
        public ActionResult update(int ID)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var backgoodss = from a in db.backGoods
                             where a.ID == ID
                             select a;
            return Json(backgoodss);
        }
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="g"></param>
        ///// <returns></returns>
        //public ActionResult Add(int ID)
        //{
        //    //修改
        //    var back = from a in db.backGoods
        //               where a.ID == ID
        //               select a;
        //    return View();
        //}

        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="g"></param>
        ///// <returns></returns>

        //[HttpPost]
        public ActionResult Add(backGoods g)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //修改
            var back = from a in db.backGoods
                       where a.ID == g.ID
                       select a;
            backGoods result = back.ToList().LastOrDefault();//获取到所有的数据添加，把不需要修改的就直接添加了
                                                             //需要修改的数据重新赋值
            result.statusBack = g.statusBack;
            result.statusRefund = g.statusRefund;
            result.States = g.States;
            result.CreateTime = DateTime.Now;
            db.Entry<backGoods>(result).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Content("更改成功！");
        }

        //删除
        public ActionResult delete(int id)
        {
            //删除退货表的这条数据
            //var sale = from a in db.backGoods
            //           where a.ID == id
            //           select a;
            //backGoods result = sale.ToList().LastOrDefault();
            ////int num = result.backgoodsNumber;
            ////string ordercode = result.goodsCode;
            //db.Entry<backGoods>(result).State = System.Data.Entity.EntityState.Deleted;

            var findLi = db .backGoods.Find(id);
            int num = findLi.backgoodsNumber;
            string ordercode = findLi.goodsCode;
            //删除退货表的这条数据
            db.Entry<backGoods>(findLi).State = System.Data.Entity.EntityState.Deleted;

            //根据result.goodsCode 删除订单表该条数据
            var findgoodsSn = from a in db.OrderInfo
                              where a.orderSn == ordercode
                              select a;
            OrderInfo results = findgoodsSn.ToList().LastOrDefault();
            string goodsn = results.goodsSn;
            //删除退货表的这条数据
            db.Entry<OrderInfo>(results).State = System.Data.Entity.EntityState.Deleted;

            //修改goods表的goodsNumber加上result.backgoodsNumber
            var goods = from a in db.Goods
                       where a.goodsSn == goodsn
                        select a;
            Goods goodss = goods .ToList().LastOrDefault();
            goodss.goodsNumber = goodss.goodsNumber + num;
            db.Entry<Goods>(goodss).State = System.Data.Entity.EntityState.Modified;         
          
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
