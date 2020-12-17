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
    public class HaveBuyController : Controller
    {
        ShopEntities db = new ShopEntities();


        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}

        public string GetFrontViewData(string frontViewData)
        {

            //handle frontViewData code

            return frontViewData;

        }


        /// <summary>
        /// 生成退货的编号
        /// </summary>
        /// <returns></returns>
        public string NewaddressNum()
        {
            var nums = from a in db.backGoods
                       orderby a.ID descending
                       select a;

            if (db.backGoods.Count() != 0)
            {
                backGoods mm = nums.First();

                var hounum = mm.backCode.Substring(4, 2);
                var qiannum = mm.backCode.Substring(1, 4);
                if (qiannum == "0000")
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
                return "000001";
            }
        }


        /// <summary>
        /// 退货事件
        /// </summary>
        /// <param name="deleteId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string frontViewData,string backgoodss)
        {
            var deleteId = Convert.ToInt32(frontViewData);


            if (frontViewData == null)
            {
                return RedirectToAction("Index");
            }
            var cinfo = db.OrderInfo.Find(deleteId);
            //加入退货表
            var backgoods = new backGoods();
            backgoods.backCode = NewaddressNum();
            //查询出对应的订单号
            var ordrinfo = db.cart.Where(a => a.goodsSn == cinfo.goodsSn).First();
            backgoods.goodsCode = cinfo.orderSn;
            backgoods.backType = Convert.ToInt32(backgoodss);
            backgoods.backGoodsPrice = ordrinfo.totalPrice;
            backgoods.backgoodsNumber = Convert.ToInt32(cinfo.buyNumber);
            backgoods.statusBack = 1;
            backgoods.statusRefund = 1;
            backgoods.States = 1;
            backgoods.CreateUser = LoginController.loginname;
            backgoods.CreateTime = DateTime.Now;

            cinfo.orderStatus = 2;
            db.backGoods.Add(backgoods);
            db.SaveChanges();

          
            return RedirectToAction("Index");
        }

        public ActionResult Index(int ? pagenum)
        {
            //显示登陆名称
            if (LoginController.loginname == "")
            {
                ViewBag.login = "未登录";
                ViewBag.cartnum = 0;
                return RedirectToAction("../Login/Index");
            }
            else
            {
                //显示购物车条数
                ViewBag.login = LoginController.loginname;
                var cartcount = db.cart.Where(a =>a.userId == LoginController.loginId && a.state == "未购买").ToList().Count;
                ViewBag.cartnum = cartcount;
            }

            //退货原因
            List<SelectListItem> backyy = new List<SelectListItem>();
            backyy.Add(new SelectListItem { Text = "拍错", Value = "0" });
            backyy.Add(new SelectListItem { Text = "不想要", Value = "1" });
            backyy.Add(new SelectListItem { Text = "未按时间发货", Value = "2" });
            ViewData["backgood"] = backyy;


            var loginName = LoginController.loginname;
            int loginId = LoginController.loginId;



            //获取购物车的数据    (用户给了固定值，后期要更改)(enable：0标识有效；1标识无效)


            var backgood = from bx in db.OrderInfo
                           join ct in db.cart on bx.goodsSn equals ct.goodsSn
                           where bx.payState == 1 && bx.orderStatus!=2 && bx.userId==LoginController.loginId
                           select new
                           {
                               bx.ID,
                                bx.goodsSn,
                                ct.marketPrice,
                                bx.buyNumber,
                                ct.userId,
                                ct.goodsName,
                                ct.thumbUrl
                           };


            List <Backgood> dncs = new List<Backgood>();
            foreach (var item in backgood)
            {
                Backgood d = new Backgood();
                d.ID = item.ID;
                d.userId = item.userId;
                d.goodsSn = item.goodsSn;

                d.goodsName = item.goodsName;
                d.marketPrice = item.marketPrice;
                d.buyNumber = item.buyNumber;

                d.thumbUrl = item.thumbUrl;
                d.totalprice = item.marketPrice * item.buyNumber;
                dncs.Add(d);
            }

            ViewBag.List = dncs.ToList();
        

            return View(dncs.ToList().ToPagedList(pagenum ?? 1, 5));
        }
    }
}