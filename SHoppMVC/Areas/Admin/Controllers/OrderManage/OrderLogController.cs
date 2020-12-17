using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers.OrderManage
{
    public class OrderLogController : Controller
    {
        ShopEntities db = new ShopEntities();

        public static List<orderLog> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<orderLog>(OrderLogController.listattr);
        }

        [HttpPost]
        public ActionResult Index(int? pagenum, DateTime? Startdate,string orderSn)
        {
            //查询已经支付过的订单
            var result = from x in db.orderLog
                         select x;
            result = result.Where(x => x.orderSn.Contains(orderSn));
            if (Startdate != null)//有时间才进行时间的搜索
            {
                DateTime endTime = Convert.ToDateTime(Startdate).AddDays(1);
                result = result.Where(x => x.CreateTime > Startdate && x.CreateTime < endTime);
            }
            Session["SearchOrderlog"] = result.ToList();//记忆赛选出条件的值进行分页
            OrderLogController.listattr = result.ToList();
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
        }


        // GET: Admin/OrderLog
        public ActionResult Index(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //查询已经支付过的订单
            var result = from x in db.orderLog
                         select x;
            if (Session["SearchOrderlog"] != null)
            {
                var dd = (List<orderLog>)Session["SearchOrderlog"];
                OrderLogController.listattr = dd.ToList();
                return View(dd.ToPagedList(pagenum ?? 1, 5));

            }
            OrderLogController.listattr = result.ToList();
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
        }
    }
}