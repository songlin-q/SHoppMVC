using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHoppMVC.Areas.Admin.Controllers.IncomeManage
{
    public class incomeController : Controller
    {
        ShopEntities db = new ShopEntities();
        public static List<income> listattr;


        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<income>(incomeController.listattr);
        }



        [HttpPost]
        public ActionResult Index(int? pagenum, DateTime? Startdate, string orderSn,string incomeSn)
        {
            //查询已经支付过的订单
            var result = from x in db.income
                         select x;
            result = result.Where(x => x.orderSn.Contains(orderSn) && x.incomeCode.Contains(incomeSn));
            if (Startdate != null)//有时间才进行时间的搜索
            {
                DateTime endTime = Convert.ToDateTime(Startdate).AddDays(1);
                result = result.Where(x => x.createTime > Startdate && x.createTime < endTime);
            }
            Session["SearchIncome"] = result.ToList();//记忆赛选出条件的值进行分页
            incomeController.listattr = result.ToList();
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
        }


        // GET: Admin/income

        /// <summary>
        /// 收入加载
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
            var reslut = from x in db.income
                         select x;
            if (Session["SearchIncome"] != null)
            {
                var dd = (List<OrderInfo>)Session["SearchIncome"];
                OrderController.listattr = dd.ToList();
                return View(dd.ToPagedList(pagenum ?? 1, 5));

            }
            incomeController .listattr = reslut.ToList();
            return View(reslut.ToList().ToPagedList(pagenum ?? 1, 5));
        }
    }
}