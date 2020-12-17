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
    public class OrderAuditController : Controller
    {
        ShopEntities db = new ShopEntities();
        ShopEntities db1 = new ShopEntities();

        public static List<OrderInfo> listattr;
        public static List<OrderInfo> listship;
        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<OrderInfo>(OrderAuditController.listattr);
        }

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult exceloutshipcenter()
        {
            return new ExcelResult.ExcelResults<OrderInfo>(OrderAuditController.listattr);
        }
        /// <summary>
        /// 物流中心(搜索)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShipCenter(int? pagenum, string orderSn, string deliverySn, int? IsEnd, DateTime? Startdate, DateTime? Enddate)
        {
            //查询已经支付过的订单
            var result = from x in db.OrderInfo
                         where x.payState == 1
                         select x;
            if (IsEnd != null)
            {
                if (IsEnd != 0)//查询未审核订单
                {
                    result = result.Where(x => x.shippingStatus == IsEnd);
                }

            }
            result = result.Where(x => x.orderSn.Contains(orderSn) && x.deliverySn.Contains(deliverySn));
            if (Startdate != null && Enddate == null)//有开始时间，没有结束时间
            {
                Enddate = DateTime.Now;
                result = result.Where(x => x.addTime > Startdate && x.addTime < Enddate);//返回开始时间到现在的时间

            }
            if (Startdate != null && Enddate != null)//有时间才进行时间的搜索
            {
                if (Startdate > Enddate)
                {
                    return Content("<script>alert('输入的日期区间有误！');window.location.href='../OrderAudit/ShipCenter'</script>");

                }
                else
                {

                    result = result.Where(x => x.addTime > Startdate && x.addTime < Enddate);
                }
              
            }

            Session["SearchShip"] = result.ToList();//记忆赛选出条件的值进行分页

            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));

        }

        /// <summary>
        /// 物流中心
        /// </summary>
        /// <returns></returns>
        public ActionResult ShipCenter(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //查询已经发货的订单
            var result = from x in db.OrderInfo
                         where x.deliverySn != null && x.shippingStatus == 1
                         select x;
            if (Session["SearchShip"] != null)
            {
                var dd = (List<OrderInfo>)Session["SearchShip"];
                return View(dd.ToPagedList(pagenum ?? 1, 5));

            }
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));

        }



        /// <summary>
        /// 审核
        /// </summary>
        /// <returns></returns>

        public ActionResult Audit(int ? ID, string orderSn)
        {
            try
            {
                var result = db.OrderInfo.Find(ID);
                var now = DateTime.Now;
                var month = now.Month.ToString();
                var day = now.Day.ToString();
                if (month.Length == 1)
                {
                    month = "0" + month;
                }
                if (day.Length == 1)
                {
                    day = "0" + day;
                }
                result.shippingTime = now;//订单邮递时间
                result.shippingStatus = 1;//更改为已发货

                result.deliverySn = "dsn" + now.Year.ToString() + month + day + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                db.Entry<OrderInfo>(result).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //添加审核日志
                orderLog ol = new orderLog();
                ol.orderSn = result.orderSn;
                ol.CreateUser = "Sys0001";//【固定值，后期更改】
                ol.CreateTime = DateTime.Now;
                ol.Remark = "审核通过";
                db1.Entry<orderLog>(ol).State = System.Data.Entity.EntityState.Added;
                db1.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 单击搜索
        /// </summary>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int? pagenum, int? IsAudit, string orderSn, DateTime? Startdate,DateTime? Enddate)
        {
            //查询已经支付过的订单
            var result = from x in db.OrderInfo
                         select x;
            if (IsAudit != null)
            {
                if (IsAudit != 2)//查询未审核订单
                {
                    result = result.Where(x => x.shippingStatus == IsAudit);
                }

            }

            result = result.Where(x => x.orderSn.Contains(orderSn));
            if (Startdate != null && Enddate==null)//有开始时间，没有结束时间
            {
                Enddate = DateTime.Now;
                result = result.Where(x => x.addTime > Startdate && x.addTime < Enddate);//返回开始时间到现在的时间

            }
            if (Startdate!=null && Enddate!=null)//有时间才进行时间的搜索
            {
                if (Startdate > Enddate)
                {
                    return Content("<script>alert('输入的日期区间有误！');window.location.href='../OrderAudit/Index'</script>");

                }
                else
                {
          
                    result = result.Where(x => x.addTime > Startdate && x.addTime < Enddate);
                }
                //DateTime endTime = Convert.ToDateTime(Startdate).AddDays(1);
                //result = result.Where(x => x.addTime > Startdate && x.addTime < endTime);
            }
         
            Session["Search"] = result.ToList();//记忆赛选出条件的值进行分页
            OrderAuditController.listattr = result.ToList();
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
        }
    
        // GET: Admin/OrderAudit
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        public ActionResult Index(int? pagenum)
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            //查询已经支付过的订单
            var result = from x in db.OrderInfo
                         where x.payState == 1
                         select x;
            if (Session["Search"] != null)
            {
                var dd = (List<OrderInfo>)Session["Search"];
                OrderAuditController.listattr = dd.ToList();
                return View(dd.ToList().ToPagedList(pagenum ?? 1, 5));

            }
            OrderAuditController.listattr = result.ToList();
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
        }
    }
}