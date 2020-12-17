using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class CartController : Controller
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



        /// <summary>
        /// 从购物车里面删除商品
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? deleteId)
        {

            if (deleteId == null)
            {
                return RedirectToAction("Index");
            }
            var cinfo = db.cart.Find(deleteId);

            db.Entry<cart>(cinfo).State = System.Data.Entity.EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                return Content("<script>alert('删除成功！');window.location.href='Index';</script>");
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='Index';</script>");

            }

        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="buyNumber"></param>
        /// <param name="chpproducts"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int[] buyNumber)
        {
            try
            {

                string dd = Request.Form["chpproducts"];//获取选中的checkbox的value值
                string[] indexAndSIds = dd.Split(',');
              
                #region 循环遍历选中的数据进行修改
                //把选中的产品信息
                for (int i = 0; i < indexAndSIds.Length; i++)
                {
                    string[] fe = indexAndSIds[i].Split('S');
                    int index = Convert.ToInt32(fe[0])-1;//获取选中的复选框的索引
                    int sid = Convert.ToInt32(fe[1]);//获取选中索引对应的那条数据的ID值
                    
               //     int id = Convert.ToInt32(chpproducts[i].ToString());//获取选中的主键值
                    int num = Convert.ToInt32(buyNumber[index].ToString());//购买的数量
                  
                    //根据主键进行修改
                    var cinfo = db.cart.Find(sid);
                    cinfo.buyNumber = num;
                    cinfo.totalPrice = (num * cinfo.marketPrice) + Convert.ToDecimal(cinfo.postage);
                    db.Entry<cart>(cinfo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                #endregion

                return RedirectToAction("Index");
            }
            catch
            {

                return Content("<script>alert('请先选中修改的数据！');history.go(-1)</script>");
            }



        }

        // GET: Cart   初次展示购物车的列表
        public ActionResult Index()
        {

            #region 判断是否登录
            if (Session["LoginInfo"] ==null)
            {
                return RedirectToAction("../Login/Index");
            }
            #endregion


            var loginName = LoginController.loginname;
            int loginId = LoginController.loginId;

            //判断是否是空值
            if (db.cart.Count() != 0)
            {
                var cart = db.cart.First();
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                var gs = db.Goods.Find(ID);
                if (ID != 0)
                {
                    //判断是否有重复的商品
                    if (cart.goodsSn != gs.goodsSn)
                    {
                        //通过商品对购物车的信息进行新增
                        var carts = new cart();
                        carts.userId = LoginController.loginId;
                        carts.goodsSn = gs.goodsSn;
                        carts.goodsName = gs.goodsName;
                        carts.marketPrice = Convert.ToDecimal(gs.marketPrice);
                        carts.postage = gs.postage;
                        carts.buyNumber = 1;
                        carts.totalPrice = Convert.ToDecimal(gs.marketPrice * carts.buyNumber + gs.postage);
                        carts.goodsType = gs.goodType;
                        carts.isGift = 0;
                        carts.isShipping = gs.isShipping;
                        carts.shippingNum = 0;
                        carts.attrId = gs.attrId;
                        carts.addTime = gs.addTime;
                        carts.promotePrice = gs.promotePrice;
                        //得到产品的图片
                        var gimg = db.goodsImg.Where(a => a.goodsCode == ID).First();
                        carts.thumbUrl = gimg.imgUrl;
                        carts.enable = gs.isDelete;
                        carts.state = "未购买";
                        db.cart.Add(carts);
                        db.SaveChanges();
                    }
                    else
                    {
                        //如果重复购买数量增加1
                        var cartnum = db.cart.Find(ID);
                        cartnum.buyNumber = cartnum.buyNumber + 1;
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                var gs = db.Goods.Find(ID);
                //通过商品对购物车的信息进行新增
                var carts = new cart();
                carts.userId = LoginController.loginId;
                carts.goodsSn = gs.goodsSn;
                carts.goodsName = gs.goodsName;
                carts.marketPrice = Convert.ToDecimal(gs.marketPrice);
                carts.postage = gs.postage;
                carts.buyNumber = 1;
                carts.totalPrice = Convert.ToDecimal(gs.marketPrice * carts.buyNumber + gs.postage);
                carts.goodsType = gs.goodType;
                carts.isGift = 0;
                carts.isShipping = gs.isShipping;
                carts.shippingNum = 0;
                carts.attrId = gs.attrId;
                carts.addTime = gs.addTime;
                carts.promotePrice = gs.promotePrice;
                //得到产品的图片
                var gimg = db.goodsImg.Where(a => a.goodsCode == ID).First();
                carts.thumbUrl = gimg.imgUrl;
                carts.enable = gs.isDelete;
                carts.state = "未购买";
                db.cart.Add(carts);
                db.SaveChanges();
            }
      
       
           


            //获取购物车的数据    (用户给了固定值，后期要更改)(enable：0标识有效；1标识无效)
            var result = from x in db.cart
                         join b in db.attribute on x.attrId equals b.attrId
                         where x.userId == loginId && x.enable == 0 && x.state == "未购买"
                         select new
                         {
                             x.ID,
                             x.userId,
                             x.goodsSn,
                             x.goodsName,
                             x.marketPrice,
                             x.postage,
                             x.buyNumber,
                             x.totalPrice,
                             x.goodsType,
                             x.isGift,
                             x.isShipping,
                             x.shippingNum,
                             x.thumbUrl,
                             x.attrId,
                             b.attrName,
                             b.attrValues

                         };

            List<dynamic> dncs = new List<dynamic>();
            foreach (var item in result)
            {
                dynamic d = new ExpandoObject();
                d.ID = item.ID;
                d.userId = item.userId;
                d.goodsSn = item.goodsSn;

                d.goodsName = item.goodsName;
                d.marketPrice = item.marketPrice;
                d.postage = item.postage;
                d.buyNumber = item.buyNumber;
                d.totalPrice = item.totalPrice;
                d.thumbUrl = item.thumbUrl;
                d.goodsType = item.goodsType;
                d.isGift = item.isGift;
                d.isShipping = item.isShipping;
                d.attrId = item.attrId;
                d.attrName = item.attrName;
                d.attrValues = item.attrValues;

                dncs.Add(d);
            }

            ViewBag.List = dncs.ToList();
            ViewData["cartNum"] = result.Count();


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

            return View();
        }
    }
}