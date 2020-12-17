using JxcSystem.Views;
using PagedList;
using SHoppMVC.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SHoppMVC.Areas.Admin.Controllers.InStorageManage

{
    public class InStorageController : Controller
    {
        ShopEntities db = new ShopEntities();
        static List<dynamic> dny=null;
        public static List<InStorage> listattr;

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult excelout()
        {
            return new ExcelResult.ExcelResults<InStorage>(InStorageController.listattr);
        }

        /// <summary>
        /// Ajax  返回
        /// </summary>
        /// <returns></returns>
        public ActionResult Show(string selectvalue)
        {
            var rr = from x in db.supplier
                     where x.supplierName == selectvalue
                     select x;

            db.Configuration.ProxyCreationEnabled = false;//防止循环调用
            return Json(rr);
        }

        /// <summary>
        /// 绑定下拉菜单
        /// </summary>
        public void BindSelet()
        {
            //供应商下拉列表
            List<SelectListItem> supplier = new List<SelectListItem>();
            foreach (var item in db.supplier)
            {
                SelectListItem selectss = new SelectListItem();
                selectss.Text = item.supplierName;
                selectss.Value = item.ID.ToString();
                supplier.Add(selectss);
            }
            ViewData["supper"] = supplier;

        }


        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="pagenum"></param>
        /// <param name="goodsSn"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GoodSelect(int? pagenum, string goodsSn, int[] InSorageNum, int[] InSoragePrice)
        {

            try
            {
                var result = from a in db.Goods
                             where a.isDelete == 0 && a.goodsSn.Contains(goodsSn)
                             select a;

                string dd = Request.Form["cproducts"];//获取选中的checkbox的value值
                string[] indexAndSIds = dd.Split(',');
                InStorageController.dny = new List<dynamic>() { };
                #region 循环遍历选中的数据进行修改
                //把选中的产品信息
                for (int i = 0; i < indexAndSIds.Length; i++)
                {
                    string[] fe = indexAndSIds[i].Split('p');
                    int index = Convert.ToInt32(fe[0]) - 1;//获取选中的复选框的索引
                    string sid = fe[1].ToString();//获取的商品编码
                    int num = Convert.ToInt32(InSorageNum[index].ToString());//购买的数量
                    int Price = Convert.ToInt32(InSoragePrice[index]);//单价
                    var rre = from x in db.Goods where x.goodsSn == sid select x;
                    Goods item = rre.ToList().LastOrDefault();
                    //存入list临时集合中
                    dynamic d = new ExpandoObject();
                    d.goodsSn = item.goodsSn;
                    d.goodsName = item.goodsName;
                    d.Price = Price;
                    d.num = num;
                    d.total = Price * num;
                    dny.Add(d);
                }
                #endregion


                Session["good"] = result.ToList();

                return View(result);




                //return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
            }
            catch
            {

                return Content("<script>alert('请先选中修改的数据！');history.go(-1)</script>");
            }




        }


        /// <summary>
        /// 商品管理
        /// </summary>
        /// <returns></returns>

        public ActionResult GoodSelect()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            var result = from a in db.Goods
                         where a.isDelete == 0
                         select a;
            if (Session["good"] != null)
            {
                var re = (List<Goods>)Session["good"];
                return View(re);
            }
            return View(result.ToList());
        }
      
        /// <summary>
        /// 产生入库单编码
        /// </summary>
        /// <returns></returns>
        public string CreateInStorageNum()
        {
            InStorage info = db.InStorage.ToList().LastOrDefault();
            if (info == null)
            {
                return "000001";
            }
            int num = Convert.ToInt32(info.InStorageCode);
            num += 1;
            if (num < 10)
            {
                return "00000" + num;
            }
            else if (num >= 10 && num < 100)
            {
                return "0000" + num;
            }
            else if (num >= 100 && num < 1000)
            {
                return "000" + num;
            }
            else if (num >= 1000 && num < 10000)
            {
                return "00" + num;
            }
            else if (num >= 10000 && num < 100000)
            {
                return "0" + num;
            }
            else
            {
                return num.ToString();
            }
        }
    
        /// <summary>
        /// 入库操作
        /// </summary>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(InStorage inn,int SupplierID)
        {
            BindSelet();
            if (InStorageController.dny != null)
            {
       
                ViewBag.data = InStorageController.dny;
                if (ViewBag.data!=null)
                {
                    foreach (var item in ViewBag.data)
                    {
                        inn.InStorageCode = CreateInStorageNum();
                        inn.SupplierID = SupplierID;
                        inn.supplier = db.supplier.Find(SupplierID);
                        inn.supplier.ID = SupplierID;
                        inn.supplier.Remark = inn.supplier.Remark;
                        inn.supplier.companyName = inn.supplier.companyName;
                        inn.supplier.email = inn.supplier.email;
                        inn.supplier.guimo = inn.supplier.guimo;
                        inn.supplier.supplierName = inn.supplier.supplierName;
                        inn.CreateUser = "yonghu";
                        inn.InType = "采购入库";//
                        inn.CreateTime = DateTime.Now;
                        inn.Num = item.num;
                        inn.Status = 2;//1审核，2未审核
                        inn.CreateType = 1;
                        inn.goodsSn = item.goodsSn;
                        inn.Price = item.Price;
                        inn.Amount = item.total;
                        inn.Remark = "备注";
                        db.Entry<InStorage>(inn).State = System.Data.Entity.EntityState.Added;
                        InStorageController.dny = null;
                        db.SaveChanges();
                        
                    }
                }
               
            }
            return View();
        }

        /// <summary>
        /// 入库操作
        /// </summary>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        public ActionResult Add()
        {
            //判断是否登录
            if (Session["LoginUser"] == null)
            {
                return Content("<script>alert('请登录');window.location.href='../AdminLogin/AdminLogins'</script>");
            }
            BindSelet();
            if (InStorageController.dny != null)
            {
                ViewBag.goods = InStorageController.dny;
            }

            return View();
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="pagenum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(int? pagenum, int? IsAudit,string InstorageSn, DateTime? Startdate, DateTime? Enddate)
        {
            //查询已经支付过的订单
            var result = from x in db.InStorage
                         select x;
            if (IsAudit!=null)
            {
                if (IsAudit != 0)
                {
                    result = result.Where(x => x.Status == IsAudit);
                }
            }
          
            result = result.Where(x => x.InStorageCode.Contains(InstorageSn) || x.goodsSn.Contains(InstorageSn)|| x.supplier.supplierCode.Contains(InstorageSn));
            if (Startdate != null && Enddate == null)//有开始时间，没有结束时间
            {
                Enddate = DateTime.Now;
                result = result.Where(x => x.CreateTime > Startdate && x.CreateTime < Enddate);//返回开始时间到现在的时间

            }
            if (Startdate != null && Enddate != null)//有时间才进行时间的搜索
            {
                if (Startdate > Enddate)
                {
                    return Content("<script>alert('输入的日期区间有误！');window.location.href='../InStorage/Index'</script>");

                }
                else
                {

                    result = result.Where(x => x.CreateTime > Startdate && x.CreateTime < Enddate);
                }
             
            }


         
            string dd = Request.Form["InChk"];//获取选中的checkbox的value值
            if (dd!=null)
            {
                #region 循环遍历选中的数据进行修改
                string[] indexAndSIds = dd.Split(',');
                //把选中的产品信息
                for (int i = 0; i < indexAndSIds.Length; i++)
                {
                    string[] fe = indexAndSIds[i].Split('p');
                    int index = Convert.ToInt32(fe[0]) - 1;//获取选中的复选框的索引
                    string sid = fe[1].ToString();//获取的商品编码
                    var rre = from x in db.InStorage where x.InStorageCode == sid select x;
                    InStorage item = rre.ToList().LastOrDefault();
                    item.Status = 1;
                    db.Entry<InStorage>(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                #endregion
            }


            Session["InStSearch"] = result.ToList();//记忆赛选出条件的值进行分页
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
        }

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
            var result = from x in db.InStorage
                         select x;
            if (Session["InStSearch"] != null)
            {
                var dd = (List<InStorage>)Session["InStSearch"];
                InStorageController.listattr = result.ToList();
                return View(dd.ToPagedList(pagenum ?? 1, 5));

            }
            InStorageController.listattr = result.ToList();
            return View(result.ToList().ToPagedList(pagenum ?? 1, 5));
        }
    }
}