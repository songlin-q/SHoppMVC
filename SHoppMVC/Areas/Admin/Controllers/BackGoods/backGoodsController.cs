using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using SHoppMVC.Controllers;
using SHoppMVC.Models;

namespace SHoppMVC.Areas.Admin.Controllers
{
    public class backGoodsController : Controller
    {
        // GET: Admin/backGoods
        ShopEntities db = new ShopEntities();
        // GET: Admin/Goods
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
        /// <summary>
        /// 退换数量
        /// </summary>
        /// <returns></returns>
        public ActionResult R_backNums()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var backNums = from a in db.backGoods
                          where a.statusBack == 1
                          select a;

            System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart();
            chart.Width = 1250;
            chart.Height = 380;
            chart.RenderType = RenderType.ImageTag;
            chart.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("退换数量", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black);
            chart.Titles.Add(t);

            chart.ChartAreas.Add("backNums");
            chart.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            chart.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
            chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;   //设置是否交错显示,比如数据多的时间分成两行来显示

            chart.Series.Add("backNums");

            chart.Series["backNums"].Label = "#VAL";
            chart.Series["backNums"].LegendText = "数量/件";
            chart.Series["backNums"].Color = System.Drawing.Color.FromArgb(132, 209, 157);//设置颜色 


            //模拟数据
            List<string> actives = new List<string>();
            foreach (var item in backNums.ToList())
            {
                actives.Add("编号"+item.backCode);

            }

            Random rd = new Random();
            foreach (var item in backNums.ToList())
            {
                chart.Series["backNums"].Points.AddXY(item.backCode.ToString(), item.backgoodsNumber);

            }

            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.ChartAreas["backNums"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置X轴网格线的颜色
            chart.ChartAreas["backNums"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置Y轴网格线的颜色
            chart.BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            chart.BorderlineDashStyle = System.Web.UI.DataVisualization.Charting.ChartDashStyle.Solid;
            chart.BorderWidth = 2;

            chart.Legends.Add("backNums");

            MemoryStream stream = new MemoryStream();
            chart.SaveImage(stream, ChartImageFormat.Jpeg);

            return new ImageResultController(Image.FromStream(stream), System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        /// <summary>
        /// 退换价格
        /// </summary>
        /// <returns></returns>
        public ActionResult R_backPrices()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var backNums = from a in db.backGoods
                           where a.statusBack == 1
                           select a;

            System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart();
            chart.Width = 1250;
            chart.Height = 380;
            chart.RenderType = RenderType.ImageTag;
            chart.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("退换价格", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black);
            chart.Titles.Add(t);

            chart.ChartAreas.Add("backNums");
            chart.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            chart.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
            chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;   //设置是否交错显示,比如数据多的时间分成两行来显示

            chart.Series.Add("backNums");

            chart.Series["backNums"].Label = "#VAL";
            chart.Series["backNums"].Color = System.Drawing.Color.FromArgb(132, 209, 157);//设置颜色 
            chart.Series["backNums"].LegendText = "总价/元";


            //模拟数据
            List<string> actives = new List<string>();
            foreach (var item in backNums.ToList())
            {
                actives.Add("编号" + item.backCode);

            }

            Random rd = new Random();
            foreach (var item in backNums.ToList())
            {
                chart.Series["backNums"].Points.AddXY(item.backCode.ToString(), item.backGoodsPrice);

            }

            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.ChartAreas["backNums"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置X轴网格线的颜色
            chart.ChartAreas["backNums"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置Y轴网格线的颜色
            chart.BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            chart.BorderlineDashStyle = System.Web.UI.DataVisualization.Charting.ChartDashStyle.Solid;
            chart.BorderWidth = 2;

            chart.Legends.Add("backNums");

            MemoryStream stream = new MemoryStream();
            chart.SaveImage(stream, ChartImageFormat.Jpeg);

            return new ImageResultController(Image.FromStream(stream), System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}