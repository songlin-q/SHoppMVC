using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using SHoppMVC.Controllers;
using SHoppMVC.Models;
using JxcSystem.Views;

namespace SHoppMVC.Areas.Admin.Controllers.OrderIn
{
    public class OrderInfoController : Controller
    {
        // GET: Admin/OrderInfo
        ShopEntities db = new ShopEntities();
        public static List<income> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<income>(OrderInfoController.listattr);
        }
        public ActionResult MVCChart()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            return View();
        }

        public ActionResult ChartView()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            return PartialView();
        }
        public ActionResult pie_Price()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var Income = from a in db.income
                          select a;
            OrderInfoController.listattr = Income.ToList();
            List<string> xValues = new List<string>();
            List<string> yValues = new List<string>();
            List<string> actives = new List<string>();

            //数据
            foreach (var item in Income.ToList())
            {
                actives.Add(item.IncomeFee.ToString() + "元");
            }

            foreach (var item in Income.ToList())
            {
                xValues.Add(item.createTime.ToString().Substring(5,2)+"月");
                yValues.Add(item.IncomeFee.ToString());
            }

            System.Web.UI.DataVisualization.Charting.Chart pieChar = new System.Web.UI.DataVisualization.Charting.Chart();
            pieChar.Series.Add("OrderIn");
            pieChar.Series["OrderIn"].Points.DataBindXY(xValues, yValues);
            pieChar.Series["OrderIn"].Label = "#AXISLABEL #VAL";
            pieChar.Series["OrderIn"].LegendText = "#AXISLABEL #PERCENT{P1}";
            Title pieTitle = new Title("金额比例/元", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black);
            pieChar.Titles.Add(pieTitle);

            pieChar.Width = 800;

            pieChar.Height = 360;

            pieChar.Series["OrderIn"].ChartType = SeriesChartType.Pie;

            pieChar.Series["OrderIn"]["PieLabelStyle"] = "Inside";

            pieChar.Series["OrderIn"]["DoughnutRadius"] = "70";


            pieChar.ChartAreas.Add("OrderIn");
            pieChar.ChartAreas["OrderIn"].Area3DStyle.Enable3D = false;

            pieChar.Legends.Add("OrderIn");
            pieChar.Legends[0].Enabled = true;

            MemoryStream stream = new MemoryStream();
            pieChar.SaveImage(stream, ChartImageFormat.Jpeg);

            return new ImageResultController(Image.FromStream(stream), ImageFormat.Jpeg);
        }
        /// <summary>
        /// 销售
        /// </summary>
        /// <returns></returns>
        public ActionResult R_salesVolume()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var OrderIn = from a in db.OrderInfo
                          where a.orderStatus==1
                        select a;

            System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart();
            chart.Width = 1600;
            chart.Height = 400;
            chart.RenderType = RenderType.ImageTag;
            chart.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("销售数量", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black);
            chart.Titles.Add(t);

            chart.ChartAreas.Add("OrderIn");
            chart.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            chart.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
            chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;   //设置是否交错显示,比如数据多的时间分成两行来显示
            chart.ChartAreas["OrderIn"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置X轴网格线的颜色
            chart.ChartAreas["OrderIn"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置Y轴网格线的颜色
            //chart.ChartAreas["OrderIn"].AxisX.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置X轴的线条颜色
            //chart.ChartAreas["OrderIn"].AxisY.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置Y轴的线条颜色
            chart.ChartAreas["OrderIn"].ShadowColor = System.Drawing.Color.Red;//设置图表的阴影颜色

            chart.Series.Add("OrderIn");

            chart.Series["OrderIn"].Label = "#VAL";
            chart.Series["OrderIn"].LegendText = "数量/件";
            chart.Series["OrderIn"].Color = System.Drawing.Color.FromArgb(132, 209, 157);//设置颜色

            //模拟数据
            List<string> actives = new List<string>();
            foreach (var item in OrderIn.ToList())
            {
                actives.Add(item.addTime.ToString());
                actives.Add(item.addTime.ToString().Substring(2,7));
               
            }

            Random rd = new Random();
            foreach (var item in OrderIn.ToList())
            {
                chart.Series["OrderIn"].Points.AddXY(item.addTime.ToString().Substring(2,7), item.orderAmount);

            }
        
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.BorderColor = System.Drawing.Color.FromArgb(146,75,127);
            chart.BorderlineDashStyle = System.Web.UI.DataVisualization.Charting.ChartDashStyle.Solid;
            chart.BorderWidth = 2;

            chart.Legends.Add("OrderIn");

            MemoryStream stream = new MemoryStream();
            chart.SaveImage(stream, ChartImageFormat.Jpeg);

            return new ImageResultController(Image.FromStream(stream), ImageFormat.Jpeg);
        }
    }
}