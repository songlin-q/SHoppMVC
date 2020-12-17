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

namespace SHoppMVC.Areas.Admin.Controllers.Report
{
    public class GoodsController : Controller
    {
        ShopEntities db = new ShopEntities();

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
        /// 价格
        /// </summary>
        /// <returns></returns>
        public ActionResult R_Goods_Price()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var goods = from a in db.Goods
                        where a.isOnSale == 1
                        select a;

            System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart();
            chart.Width = 1250;
            chart.Height = 380;
            chart.RenderType = RenderType.ImageTag;
            chart.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("货品市场价格", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black);
            chart.Titles.Add(t);

            chart.ChartAreas.Add("goods");
            chart.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            chart.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
            chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;   //设置是否交错显示,比如数据多的时间分成两行来显示
            chart.ChartAreas["goods"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置X轴网格线的颜色
            chart.ChartAreas["goods"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置Y轴网格线的颜色

            chart.Series.Add("goods");

            chart.Series["goods"].Label = "#VAL";
            chart.Series["goods"].LegendText = "价格/元";
            chart.Series["goods"].Color = System.Drawing.Color.FromArgb(132, 209, 157);//设置颜色

            //模拟数据
            List<string> actives = new List<string>();
            foreach (var item in goods.ToList())
            {
                actives.Add(item.goodsName);

            }

            foreach (var item in goods.ToList())
            {
                chart.Series["goods"].Points.AddXY(item.goodsName, item.marketPrice);

            }

            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            chart.BorderlineDashStyle = System.Web.UI.DataVisualization.Charting.ChartDashStyle.Solid;
            chart.BorderWidth = 2;

            chart.Legends.Add("goods");

            MemoryStream stream = new MemoryStream();
            chart.SaveImage(stream, ChartImageFormat.Jpeg);

            return new ImageResultController(Image.FromStream(stream), ImageFormat.Jpeg);
        }
        /// <summary>
        /// 库存
        /// </summary>
        /// <returns></returns>
        public ActionResult R_Goods()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var goods = from a in db.Goods
                        where a.isOnSale == 1
                        select a;

            System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart();
            chart.Width = 1250;
            chart.Height = 380;
            chart.RenderType = RenderType.ImageTag;
            chart.Palette = ChartColorPalette.BrightPastel;
            Title t = new Title("货品数量", Docking.Top, new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black);
            chart.Titles.Add(t);

            chart.ChartAreas.Add("goods");
            chart.ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            chart.ChartAreas[0].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
            chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;   //设置是否交错显示,比如数据多的时间分成两行来显示
            chart.ChartAreas["goods"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置X轴网格线的颜色
            chart.ChartAreas["goods"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);//设置Y轴网格线的颜色

            chart.Series.Add("goods");

            chart.Series["goods"].Label = "#VAL";
            chart.Series["goods"].LegendText = "数量";
            chart.Series["goods"].Color = System.Drawing.Color.FromArgb(132, 209, 157);//设置颜色

            //模拟数据
            List<string> actives = new List<string>();
            foreach (var item in goods.ToList())
            {
                actives.Add(item.goodsName);

            }

            foreach (var item in goods.ToList())
            {
                chart.Series["goods"].Points.AddXY(item.goodsName, item.goodsNumber);

            }

            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            chart.BorderlineDashStyle = System.Web.UI.DataVisualization.Charting.ChartDashStyle.Solid;
            chart.BorderWidth = 2;

            chart.Legends.Add("goods");

            MemoryStream stream = new MemoryStream();
            chart.SaveImage(stream, ChartImageFormat.Jpeg);

            return new ImageResultController(Image.FromStream(stream), ImageFormat.Jpeg);
        }
    }
}