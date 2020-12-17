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
    public class QtShopListController : Controller
    {
        ShopEntities db = new ShopEntities();
        // GET: Shoplist

        //    /// <summary>
        //    /// 窗口关闭恢复默认值
        //    /// </summary>
        //    /// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}

        public ActionResult Index(string ID,int ? pageNum)
        {
            //显示登陆名称
            if (LoginController.loginname == null)
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


            //绑定一二级菜单下拉列表


            var type = from a in db.goodsType
                       select a;
            var product = from b in db.Goods
                          select b;
            var img = from c in db.goodsImg
                      select c;
            ViewBag.frist = type.ToList();
            ViewBag.two = product.ToList();
            ViewBag.there = img.ToList();

            //判断搜索条件,全部查询还是单独查询
            if (ID == null)
            {
                var results = from a in db.Goods
                              join b in db.goodsType on a.goodType equals b.ID
                              join c in db.goodsImg on a.ID equals c.goodsCode
                              select new
                              {
                                  ID = a.ID,
               
                                  typeNarme = b.typeNarme,
                                  goodsName = a.goodsName,
                                  promotePrice = a.promotePrice,
                                  marketPrice = a.marketPrice,
                                  imgDesc = c.imgDesc,
                                  imgUrl = c.imgUrl
                              };
                List<QtGoodsList> dnc = new List<QtGoodsList>();
                foreach (var item in results.ToList())
                {
                    QtGoodsList d = new QtGoodsList();
                    d.goodsName = item.goodsName;
                    d.ID = item.ID;
                    d.imgUrl = item.imgUrl;
                    d.TypeName = item.typeNarme;
                    d.promoteprice = item.promotePrice;
                    d.marketprice = item.marketPrice;
                    d.imgDesc = item.imgDesc;
                    dnc.Add(d);
                }
                return View(dnc.ToPagedList(pageNum ?? 1, 7));
            }
            ViewBag.IDd = ID;
            if (ID != null)
            {
                int idd = Convert.ToInt32(ID);
                var results = from a in db.Goods
                              join b in db.goodsType on a.goodType equals b.ID
                              join c in db.goodsImg on a.ID equals c.goodsCode
                              where a.ID == idd
                              select new
                              {
                                  ID = a.ID,
                                  typeNarme = b.typeNarme,
                                  goodsName = a.goodsName,
                                  promotePrice = a.promotePrice,
                                  marketPrice = a.marketPrice,
                                  imgDesc = c.imgDesc,
                                  imgUrl = c.imgUrl
                              };
                List<QtGoodsList> dnc = new List<QtGoodsList>();
                foreach (var item in results.ToList())
                {
                    QtGoodsList d = new QtGoodsList();
                    d.ID = item.ID;
                    d.goodsName = item.goodsName;
                    d.imgUrl = item.imgUrl;
                    d.TypeName = item.typeNarme;
                    d.promoteprice = item.promotePrice;
                    d.marketprice = item.marketPrice;
                    d.imgDesc = item.imgDesc;
                    dnc.Add(d);
                }
                return View(dnc.ToPagedList(pageNum ?? 1, 7));
            }

            //条件搜索排序
            List<SelectListItem> paixu = new List<SelectListItem>();
            paixu.Add(new SelectListItem() { Text = "按人气(销量)排序", Value = "2" });
            paixu.Add(new SelectListItem() { Text = "排序新奇(商品更新时间)", Value = "3" });
            paixu.Add(new SelectListItem() { Text = "按价格排序:从低到高", Value = "4" });
            paixu.Add(new SelectListItem() { Text = "按价格排序:从高到低", Value = "5" });
            ViewData["PaiXu"] = paixu;
            int pn = Convert.ToInt32(Request.Form["PaiXu"]);
            Session["pn"] = pn;

            return View();
        }
    }
}