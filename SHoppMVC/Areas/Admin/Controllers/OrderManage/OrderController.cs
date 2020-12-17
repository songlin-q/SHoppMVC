using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static List<OrderInfo> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<OrderInfo>(OrderController.listattr);
        }

        /// <summary>
        /// 修改地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Look(int ? ID)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var result = db.OrderInfo.Find(ID);

            return View(result);
        }




        /// <summary>
        /// 修改地址
        /// </summary>
        /// <returns></returns>
        public ActionResult EditReturnAddress(OrderInfo orinfo)
        {
           
            var result = db.OrderInfo.Find(orinfo.ID);
            result.province = orinfo.province;
            result.city = orinfo.city;
            result.district = orinfo.district;
            result.address = orinfo.address;
            db.Entry<OrderInfo>(result).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Content("更改成功!");
        }

        /// <summary>
        /// Ajax返回对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditAddress(int ID)
        {
            
            //通过id查询次订单(条件：订单id，已经支付)
            var result = from x in db.OrderInfo
                         where x.payState == 1 && x.ID == ID && x.shippingStatus==0
                         select x;

            db.Configuration.ProxyCreationEnabled = false;//防止循环调用
            var res = result.ToList().LastOrDefault();
         
            return Json(res); 


        }


        /// <summary>
        /// 删除没有支付的订单
        /// </summary>
        /// <returns></returns>
        public ActionResult IsPaydelete(int id)
        {
            //通过id查询次订单，是否是支付了的(没有支付，就进行删除)
            var result = from x in db.OrderInfo
                         where x.payState == 0 && x.ID ==id
                         select x;
            var res = result.ToList().LastOrDefault();
            if (result.Count() > 0)//可以删除
            {

                db.Entry<OrderInfo>(res).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            else
            {
                return Content("<script>alert('删除失败!');</script>");

            }
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

        }

        /// <summary>
        /// 单击搜索按钮
        /// </summary>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int? pagenum,int? IsPay,string orderSn, DateTime? Startdate, DateTime? Enddate)
        {
            BindSelect();
            var reslut = from x in db.OrderInfo
                         select x;

            if (IsPay!=null)
            {
                if (IsPay != 2)
                {
                    reslut = reslut.Where(x => x.payState == IsPay);
                }

            }
            reslut = reslut.Where(x => x.orderSn.Contains(orderSn));
            if (Startdate != null && Enddate == null)//有开始时间，没有结束时间
            {
                Enddate = DateTime.Now;
                reslut = reslut.Where(x => x.addTime > Startdate && x.addTime < Enddate);//返回开始时间到现在的时间

            }
            if (Startdate != null && Enddate != null)//有时间才进行时间的搜索
            {
                if (Startdate > Enddate)
                {
                    return Content("<script>alert('输入的日期区间有误！');window.location.href='../Order/Index'</script>");

                }
                else
                {

                    reslut = reslut.Where(x => x.addTime > Startdate && x.addTime < Enddate);
                }
                //DateTime endTime = Convert.ToDateTime(Startdate).AddDays(1);
                //result = result.Where(x => x.addTime > Startdate && x.addTime < endTime);
            }


            Session["Searchorder"] = reslut.ToList();//记忆赛选出条件的值进行分页
            OrderController.listattr = reslut.ToList();
            return View(reslut.ToList().ToPagedList(pagenum ?? 1, 5));

        }


        // GET: Admin/Order
        public ActionResult Index(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            BindSelect();
            var reslut = from x in db.OrderInfo
                         select x;
            if (Session["Searchorder"] != null)
            {
                var dd = (List<OrderInfo>)Session["Searchorder"];
                OrderController.listattr = dd.ToList();
                return View(dd.ToPagedList(pagenum ?? 1, 5));

            }
            OrderController.listattr = reslut.ToList();
            return View(reslut.ToList().ToPagedList(pagenum ?? 1, 5));

        }
    }
}