using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Controllers
{
    public class AddressController : Controller
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
        /// 获取id值，返回一个对象
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int ID)
        {
            var rr = from x in db.userAddress
                     where x.ID == ID
                     select x;
            db.Configuration.ProxyCreationEnabled = false;//防止循环调用
            return Json(rr);

        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int deleteId)
        {
            var address = from x in db.userAddress
                          where x.ID == deleteId
                          select x;
            var addressInfo = address.ToList().LastOrDefault();
            addressInfo.IsDelete = 1;//标记删除
            db.Entry<userAddress>(addressInfo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 绑定下拉菜单 地区
        /// </summary>
        public void BindSelect()
        {
            List<SelectListItem> sli = new List<SelectListItem>();
            sli.Add(new SelectListItem() { Value = "直辖市", Text = "直辖市" });
            sli.Add(new SelectListItem() { Value = "广东省", Text = "广东省" });
            sli.Add(new SelectListItem() { Value = "江苏省", Text = "江苏省" });
            sli.Add(new SelectListItem() { Value = "福建省", Text = "福建省" });
            ViewData["provinceInfo"] = sli;

            List<SelectListItem> cityInfo = new List<SelectListItem>();
            
            ViewData["cityInfo"] = cityInfo;

            List<SelectListItem> districtInfo = new List<SelectListItem>();

            ViewData["districtInfo"] = districtInfo;

        }

        /// <summary>
        /// 更新/编辑
        /// </summary>
        /// <param name="ad"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(userAddress ad)
        {
            try
            {
                BindSelect();
                if (ad != null)
                {
                    if (ad.ID != 0)//编辑
                    {
                        string addreddtext = ad.province + ad.city + ad.district;
                   string oo=   Request.Form["city"].ToString();
                        string pp = Request.Form["district"].ToString();
                        
                        if (ad.address.Contains(addreddtext))
                        {
                            ad.district = pp;
                            db.Entry<userAddress>(ad).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        else //验证地址是否有误
                        {
                            return Content("<script>alert('地址错误！');window.close();location.href = '../Checkout/Index';</script>");//关闭当前页面
                        }

                    }
                    else//新增
                    {
                        ad.userId = LoginController.loginId;
                        ad.IsDelete = 0;
                        db.Entry<userAddress>(ad).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }

                }
                return Content("<script>alert('新增成功！');window.close();location.href = '../Checkout/Index';</script>");//关闭当前页面

            }
            catch
            {

                return Content("<script>alert('新增失败！');window.close();location.href = '../Checkout/Index';</script>");
            }



        }

        // GET: Address
        public ActionResult Index()
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
                var cartcount = db.cart.Where(aa => aa.userId == LoginController.loginId && aa.state == "未购买").ToList().Count;
                ViewBag.cartnum = cartcount;
            }
            BindSelect();
            #region 是否从结算页面跳转过来
            var url = Request.Url.ToString();
            var a = url.Split('?');
            if (a.Length != 1)//判断是否数组是否只有一个(只有1说明没有传值)
            {
                //已经传值
                var b = Convert.ToInt32(a[1]);
                var rr = from x in db.userAddress
                         where x.ID == b
                         select x;
                var rest = rr.ToList().LastOrDefault();
                return View(rest);
            }
            #endregion
            //查询当前用户的地址信息
            var adddress = from x in db.userAddress
                           where x.userId == LoginController.loginId && x.IsDelete == 0
                           select x;
            ViewBag.addressInfo = adddress.ToList();
            return View();
        }


    }
}