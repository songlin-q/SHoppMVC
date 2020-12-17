using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class PersonalCenterController : Controller
    {
        ShopEntities db = new ShopEntities();
        List<PersonalOrder> dncs = new List<PersonalOrder>();

        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}


        [HttpPost]
        public ActionResult Indexcs(int? inTypes, int? pagenum)
        {
            int count = 0;
            var result = from x in db.OrderInfo
                         join b in db.Goods on x.goodsSn equals b.goodsSn
                         join c in db.goodsImg on b.ID equals c.goodsCode
                         where x.userId == LoginController.loginId && x.orderStatus!=2
                         select new
                         {
                             x.ID,
                             x.userId,
                             x.goodsSn,
                             x.payState,
                             x.shippingStatus,
                             x.orderAmount,
                             x.buyNumber,
                             b.goodsName,
                             b.marketPrice,
                             c.imgUrl,
                         };
            count = result.Count();
            int count1 = count;
            if (inTypes == 1)
            {

                //待付款
                result = from x in db.OrderInfo
                         join b in db.Goods on x.goodsSn equals b.goodsSn
                         join c in db.goodsImg on b.ID equals c.goodsCode
                         where x.payState == 0 && x.shippingStatus == 0 && x.userId == LoginController.loginId
                         select new
                         {
                             x.ID,
                             x.userId,
                             x.goodsSn,
                             x.payState,
                             x.shippingStatus,
                             x.orderAmount,
                             x.buyNumber,
                             b.goodsName,
                             b.marketPrice,
                             c.imgUrl,
                         };
                count = result.Count();
                ViewBag.Count1 = count;

            }
            if (inTypes == 2)
            {

                //待发货
                result = from x in db.OrderInfo
                         join b in db.Goods on x.goodsSn equals b.goodsSn
                         join c in db.goodsImg on b.ID equals c.goodsCode
                         where x.payState == 1 && x.shippingStatus == 0 && x.userId == LoginController.loginId
                         select new
                         {
                             x.ID,
                             x.userId,
                             x.goodsSn,
                             x.payState,
                             x.shippingStatus,
                             x.orderAmount,
                             x.buyNumber,
                             b.goodsName,
                             b.marketPrice,
                             c.imgUrl,
                         };
                count = result.Count();
                ViewBag.Count2 = count;

            }
            if (inTypes == 3)
            {

                //待收货
                result = from x in db.OrderInfo
                         join b in db.Goods on x.goodsSn equals b.goodsSn
                         join c in db.goodsImg on b.ID equals c.goodsCode
                         where x.shippingStatus == 1 && x.payState==1 && x.userId==LoginController.loginId
                         select new
                         {
                             x.ID,
                             x.userId,
                             x.goodsSn,
                             x.payState,
                             x.shippingStatus,
                             x.orderAmount,
                             x.buyNumber,
                             b.goodsName,
                             b.marketPrice,
                             c.imgUrl,
                         };
                count = result.Count();
                ViewBag.Count3 = count;

            }
            if (inTypes == 4)
            {
                result = from x in db.OrderInfo
                         join b in db.Goods on x.goodsSn equals b.goodsSn
                         join c in db.goodsImg on b.ID equals c.goodsCode
                         where x.shippingStatus == 2 && x.payState == 1 && x.userId == LoginController.loginId
                         select new
                         {
                             x.ID,
                             x.userId,
                             x.goodsSn,
                             x.payState,
                             x.shippingStatus,
                             x.orderAmount,
                             x.buyNumber,
                             b.goodsName,
                             b.marketPrice,
                             c.imgUrl,
                         };
                count = result.Count();
                ViewBag.Count4 = count;

            }
            foreach (var item in result.ToList())
            {
                PersonalOrder d = new PersonalOrder();
                d.ID = item.ID;
                d.userId = item.userId;
                d.goodsSn = item.goodsSn;
                d.shippingStatus = item.shippingStatus;
                d.goodsName = item.goodsName;
                d.marketPrice = item.marketPrice;
                d.buyNumber = item.buyNumber;
                d.orderAmount = item.orderAmount;
                d.imgUrl = item.imgUrl;
                d.count = count;
                dncs.Add(d);
            }
            ViewBag.Count5 = count1;

            return View(dncs.ToList().ToPagedList(pagenum ?? 1, 5));
        }
        public ActionResult Indexcs(int ? pagenum)
        {
            try
            {
                //显示登陆名称
                if (LoginController.loginname == null)
                {
                    ViewBag.login = "未登录";
                    ViewBag.cartnum = 0;
                    return RedirectToAction("../Login/Index");
                }
                else
                {
                    //显示购物车条数
                    ViewBag.login = LoginController.loginname;
                    var cartcount = db.cart.Where(a => a.userId == LoginController.loginId && a.state == "未购买").ToList().Count;
                    ViewBag.cartnum = cartcount;
                }

                //获取购物车的数据    (用户给了固定值，后期要更改)(enable：1标识有效；0标识无效)
                var result = from x in db.OrderInfo
                             join b in db.Goods on x.goodsSn equals b.goodsSn
                             join c in db.goodsImg on b.ID equals c.goodsCode
                             where x.userId == LoginController.loginId && x.orderStatus!=2
                             select new
                             {
                                 x.ID,
                                 x.userId,
                                 x.goodsSn,
                                 x.payState,
                                 x.shippingStatus,
                                 x.orderAmount,
                                 x.buyNumber,
                                 b.goodsName,
                                 b.marketPrice,
                                 c.imgUrl,
                             };
                int count = result.Count();

                foreach (var item in result.ToList())
                {
                    PersonalOrder d = new PersonalOrder();
                    d.ID = item.ID;
                    d.userId = item.userId;
                    d.goodsSn = item.goodsSn;
                    d.goodsName = item.goodsName;
                    d.shippingStatus = item.shippingStatus;
                    d.marketPrice = item.marketPrice;
                    d.buyNumber = item.buyNumber;
                    d.orderAmount = item.orderAmount;
                    d.imgUrl = item.imgUrl;
                    d.count = count;

                    dncs.Add(d);
                }
                ViewBag.Count5 = count;

                return View(dncs.ToList().ToPagedList(pagenum ?? 1, 5));

            }
            catch
            {
                return Content("<script>alert('数据有误！');history.go(-1)</script>");
            }

        }

        //修改，确认收货
        public ActionResult delete(int id)
        {
            //修改
            var sale = from a in db.OrderInfo
                       where a.ID == id
                       select a;
            OrderInfo result = sale.ToList().LastOrDefault();
            result.shippingStatus = 2;
            result.confirmTime = DateTime.Now;
            db.Entry<OrderInfo>(result).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Indexcs");
        }
    }
}