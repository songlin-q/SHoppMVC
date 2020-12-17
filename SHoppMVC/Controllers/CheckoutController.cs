using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class CheckoutController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static int orderNumber = 0;//累计订单数量

        ///// <summary>
        ///// 窗口关闭恢复默认值
        ///// </summary>
        ///// <param name="username"></param>
        //public void change(string username)
        //{
        //    LoginController.loginname = username;

        //}

        /// <summary>
        /// 支付页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult pay(string payId)
        {

            #region 查询出支付类型表中的支付类型名
            var payID = Convert.ToInt32(payId);
            var payInfos = db.payment.Find(payID);

            #endregion

            var userCart = (Session["CartsInfo"] as List<cart>);//当前用户购物车中的商品(包含用户id、商品编码)
            foreach (var item in userCart)//循环遍历，对商品表、订单表进行修改
            {

                #region 更改订单中的状态
                #region 查询出订单表中对应的购买信息
                var orderInfo = (from c in db.OrderInfo
                                 where c.userId == item.userId && c.goodsSn == item.goodsSn
                                 select c).ToList();
                #endregion
                foreach (var orderItem in orderInfo)
                {
                    if (orderItem.goodsSn == item.goodsSn)
                    {
                        orderItem.shippingFee = item.postage;//邮费
                        orderItem.payState = 1;//支付状态：0未支付，1已支付
                        orderItem.payTime = DateTime.Now;//支付时间
                        orderItem.payId = payID;//支付的类型id
                        orderItem.orderStatus = 1;//状态：改为已购买
                        db.Entry<OrderInfo>(orderItem).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        //新增数据到收入表
                        var income = new income();
                        income.incomeCode = NewaddressNum();
                        income.orderSn = orderItem.orderSn;
                        if (orderItem.payId==1)
                        {
                            income.payType = "微信";
                        }
                        if (orderItem.payId == 2)
                        {
                            income.payType = "支付宝";
                        }
                        if (orderItem.payId == 3)
                        {
                            income.payType = "网银";
                        }
                        if (orderItem.payId == 4)
                        {
                            income.payType = "银行卡";
                        }
                        income.IncomeFee = Convert.ToDecimal(item.marketPrice * orderItem.buyNumber);

                        income.payName = LoginController.loginname;
                        income.createTime = DateTime.Now;
                        db.income.Add(income);

                        db.SaveChanges();



                    }

                }
                #endregion


                #region 更改商品表中的数量
                #region  查询出商品表中对应的商品信息

                var goodsInfo = (from c in db.Goods
                                 where c.goodsSn == item.goodsSn
                                 select c).ToList();
                #endregion
                foreach (var goodItem in goodsInfo)
                {
                    if (goodItem.goodsSn == item.goodsSn)
                    {
                        goodItem.goodsNumber -= item.buyNumber;//数量：减去购买的数量
                        db.Entry<Goods>(goodItem).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                #endregion


            }
            foreach (var item in userCart)//更改购物表
            {
                item.enable = 1;//状态：无效
                item.state = "已购买";
                db.Entry<cart>(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect("../Main/Main");
        }

        /// <summary>
        /// 支付页面
        /// </summary>
        /// <returns></returns>
        public ActionResult pay()
        {
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


            if (Session["totalPrice"] != null)
            {
                //获取到总价(包括运费)
                ViewBag.totalprice = Convert.ToString(Session["totalPrice"]);
                ViewBag.orderNumber = orderNumber;
            }
            return View();
        }

        /// <summary>
        /// 生成收入的编号
        /// </summary>
        /// <returns></returns>
        public string NewaddressNum()
        {
            var nums = from a in db.income
                       orderby a.ID descending
                       select a;

            if (db.income.Count()!= 0)
            {
                income mm = nums.First();

                var hounum = mm.incomeCode.Substring(4, 2);
                var qiannum = mm.incomeCode.Substring(1, 4);
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
        /// 产生一个订单编码
        /// </summary>
        /// <returns></returns>
        public string CreateNum()
        {
            OrderInfo info = db.OrderInfo.ToList().LastOrDefault();
            if (db.OrderInfo.Count() != 0)
            {
                string str = info.orderSn.ToString();
                //就搜索出有效的最后一条记录.选取"_"后的数字，进行叠加
                if (str.IndexOf('_') != -1)
                {
                    #region indexOf()方法，查找某字符串在一个字符串内的位置，没有则返回 - 1

                    //                    string aa = "abcdef";
                    //int a = aa.indexOf("bc");//a会等于1
                    //int b = aa.indexOf("a");//b会等于0
                    //int c = aa.indexOf("g"); c会等于 - 1
                    #endregion

                    int a = str.IndexOf('_') + 1;

                    int num = Convert.ToInt32(str.Substring(a));
                    num += 1;
                    string header = str.Substring(0, a).ToString();
                    if (num < 10)
                    {
                        return header + "000000" + num;
                    }
                    else if (num >= 10 && num < 100)
                    {
                        return header + "00000" + num;
                    }
                    else if (num >= 100 && num < 1000)
                    {
                        return header + "0000" + num;
                    }
                    else if (num >= 1000 && num < 10000)
                    {
                        return header + "000" + num;
                    }
                    else if (num >= 10000 && num < 100000)
                    {
                        return header + "00" + num;
                    }
                    else if (num >= 10000 && num < 1000000)
                    {
                        return header + "0" + num;
                    }
                    else
                    {
                        return header + num;
                    }


                }
                else
                {
                    return "d";
                }

            }
            else
            {
                return "Osn_0000001";
            }

        }

        /// <summary>
        /// 单击了提交订单按钮
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string addressId, string payState, string shippingName, string totalPrice,string payNote)
        {

            Session["totalPrice"] = totalPrice;
            BindShipp();
            int aid = Convert.ToInt32(addressId);

            #region 通过地址表中的ID查询出选中的地址//通过支付方式的id查询出支付名称
            var addressInfos = db.userAddress.Find(aid);
            int dd = Convert.ToInt32(Request.Form["shippingName"]);//运输方式
            string shipName = "";
            if (dd == 1) { shipName = "中通"; }
            else if (dd == 2) { shipName = "韵达"; }
            else if (dd == 3) { shipName = "圆通"; }
            else { shipName = "顺丰"; }

            //var payInfos = db.payment.Find(payId);
            #endregion
            if (Session["CartsInfo"] != null)
            {
                #region 循环遍历商品
                foreach (var item in (Session["CartsInfo"] as List<cart>))
                {
                    orderNumber += 1;

                    #region 给订单表增加数据
                    OrderInfo source = new OrderInfo();
                    source.orderSn = CreateNum();
                    source.userId = LoginController.loginId;
                    source.goodsSn = item.goodsSn;
                    source.orderStatus = 0;//订单的状态(0预定，1已购买)
                    source.shippingStatus = 0;//商品的配送状态(0无信息、1配送中、2、配送结束)
                    source.consignee = addressInfos.consignee;
                    source.province = addressInfos.province;
                    source.city = addressInfos.city;
                    source.district = addressInfos.district;
                    source.address = addressInfos.address;
                    source.zipcode = addressInfos.zipcode;
                    source.tel = addressInfos.tel;
                    source.mobile = addressInfos.tel;
                    source.email = addressInfos.email;
                    //支付编码
                    source.payId = 1;
                    source.goodsAmount = item.totalPrice;
                    source.shippingFee = 0;
                    source.orderAmount = item.totalPrice;
                    source.shippingFee = 0;
                    source.buyNumber = item.buyNumber;//购买数量(舒珊珊添加)
                    source.payNote = payNote;
                    source.payState = 0;//支付状态(0未支付，1已支付)
                    source.shippingName = shipName;
                    source.addTime = DateTime.Now;

                    db.Entry<OrderInfo>(source).State = System.Data.Entity.EntityState.Added;

                    db.SaveChanges();

                    #endregion
                }
                #endregion

            }

            return RedirectToAction("pay");
        }

        /// <summary>
        /// 绑定配送方式
        /// </summary>
        public void BindShipp()
        {
            List<SelectListItem> sli = new List<SelectListItem>();
            sli.Add(new SelectListItem() { Value = "1", Text = "中通" });
            sli.Add(new SelectListItem() { Value = "2", Text = "韵达" });
            sli.Add(new SelectListItem() { Value = "1", Text = "圆通" });
            sli.Add(new SelectListItem() { Value = "2", Text = "顺丰" });
            ViewData["shippInfo"] = sli;

        }
        // GET: Checkout
        public ActionResult Index()
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
                var cartcount = db.cart.Where(s => s.enable == 0 && s.userId == LoginController.loginId).ToList().Count;
                ViewBag.cartnum = cartcount;
            }


            #region 根据当前用户名获取购物信息 
            var loginName = LoginController.loginname;
            int loginId = LoginController.loginId;

            //var cartcount = (from n in db.cart
            //                 where n.userId == loginId && n.enable == 0
            //                 select n).ToList().Count();

            //ViewBag.cartnum = cartcount;
            #endregion


            BindShipp();
            #region 获取从购物界面传入的参数(即购物的id值)
            var url = Request.Url.ToString();
            var a = url.Split('?');
            if (a.Length != 1)//判断是否数组是否只有一个(只有1说明没有传值)
            {
                var b = a[1];
                string[] cartNums = b.Split('&');
                #region 根据数组内的值进行查询
                List<cart> dncs = new List<cart>();
                for (int i = 0; i < cartNums.Length - 1; i++)
                {
                    var ii = Convert.ToInt32(cartNums[i].ToString());
                    var cartInfos = db.cart.Find(ii);
                    dncs.Add(cartInfos);
                }
                ViewBag.List = dncs.ToList();
                Session["CartsInfo"] = dncs.ToList();//当前用户购物车中的商品
                #endregion
            }
            #endregion
            #region 查询用户添加的地址  (userId给了固定值 后期要更改)
            var result = from x in db.userAddress
                         where x.userId == LoginController.loginId && x.IsDelete == 0
                         select x;
            ViewBag.address = result.ToList();
            #endregion

            return View();
        }
    }
}