using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class MainController : Controller
    {
        ShopEntities db = new ShopEntities();
        // GET: Main

        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}
        public ActionResult Main()
        {
            if (LoginController.loginname == "")
            {
                ViewBag.login = "未登录";
                ViewBag.cartnum = 0;
            }
            else
            {
                ViewBag.login = LoginController.loginname;
                var cartcount = db.cart.Where(a => a.enable == 0 && a.userId== LoginController.loginId && a.state == "未购买").ToList().Count;
                ViewBag.cartnum = cartcount;
            }
          


            //得到商品类型表
            var goodstype = from a in db.goodsType
                            select a;
            ViewBag.goodstype = goodstype.ToList();
            //得到蔬菜类型的数据
            var VegeTable = db.Goods.Where(a => a.goodType == 2).ToList();
            ViewBag.VegeTable = VegeTable;
            //得到水果类型的数据
            var Fruits = db.Goods.Where(a => a.goodType == 3).ToList();
            ViewBag.Fruits = Fruits;
            //得到鲜肉类型的数据
            var Meat = db.Goods.Where(a => a.goodType == 5).ToList();
            ViewBag.Meat = Meat;
            //得到海鲜类型的数据
            var Seafood = db.Goods.Where(a => a.goodType == 4).ToList();
            ViewBag.Seafood = Seafood;
            //得到封面图片路径的数据
            var goodImg = from a in db.goodsImg
                          select a;
            ViewBag.goodImg = goodImg.ToList();
            //得到悬浮上去后的图片路径
            var XFgoodimg = from a in db.XFgoodimg
                            select a;
            ViewBag.XFgoodimg = XFgoodimg.ToList();
            return View();
        }
    }
}