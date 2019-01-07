using Flower.Models;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace Flower.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult Detailed(int id)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                 var flower = fs.Product.Where(p => p.ProductId == id).ToList();
                return View(flower);
            }
            
        }
        #region 给与首页传数据库
        public ActionResult GiveIndex()
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                ArrayList array = new ArrayList();
                List<Product> pro = fs.Product.OrderByDescending(p => p.ProductVolume).ToList(); ;
                foreach (var item in pro)
                {
                    array.Add(item.ProductId);
                    array.Add(item.Picture1);
                }
                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult giveFestival()
        {
            ArrayList array = new ArrayList();
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Festival> list = fs.Festival.ToList();
                foreach (var item in list)
                {
                    array.Add(item.FestivalId);
                    array.Add(item.FestivalName);
                }
                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult giveflowerkind()
        {
            ArrayList array = new ArrayList();
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<FlowerKind> list = fs.FlowerKind.ToList();
                foreach (var item in list)
                {
                    array.Add(item.FlowerKindId);
                    array.Add(item.FlowerKindName);
                }
                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult givecolor()
        {
            ArrayList array = new ArrayList();
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Color> list = fs.Color.ToList();
                foreach (var item in list)
                {
                    array.Add(item.ColorId);
                    array.Add(item.ColorName);
                }
                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        
        #region 列表
        public ActionResult getListCount(string table, int Id, int price, int volume)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                switch (table)
                {
                    case "Festival":
                        {
                            var pro = fs.Product.Where(p => p.FestivalId == Id).Count();
                            return Json(pro, JsonRequestBehavior.AllowGet);
                        }
                    case "Color":
                        {
                            var pro = fs.Product.Where(p => p.ColorId == Id).Count();
                            return Json(pro, JsonRequestBehavior.AllowGet);
                        }
                    case "FlowerKind":
                        {
                            var pro = fs.Product.Where(p => p.FlowerKindId == Id).Count();
                            return Json(pro, JsonRequestBehavior.AllowGet);
                        }
                    default:
                        {
                            return Json(table, JsonRequestBehavior.AllowGet);
                        }
                }
            }
        }
        public ActionResult getList(string table, int Id, int price, int volume)
        {
            ArrayList array = new ArrayList();
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                switch (table)
                {
                    case "Festival":
                        {
                            if (price == 0 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 0 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderByDescending(p=>p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 0 && volume == 2)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderBy(p => p.NowPrice).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderBy(p => p.NowPrice).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 2)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderBy(p => p.NowPrice).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 2 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderByDescending(p => p.NowPrice).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 2 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderByDescending(p => p.NowPrice).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var pro = fs.Product.Where(p => p.FestivalId == Id).OrderByDescending(p => p.NowPrice).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                        }
                    case "Color":
                        {
                            if (price == 0 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 0 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 0 && volume == 2)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderBy(p => p.NowPrice).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderBy(p => p.NowPrice).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 2)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderBy(p => p.NowPrice).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 2 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderByDescending(p => p.NowPrice).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 2 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderByDescending(p => p.NowPrice).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var pro = fs.Product.Where(p => p.ColorId == Id).OrderByDescending(p => p.NowPrice).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                        }
                    case "FlowerKind":
                        {
                            if (price == 0 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 0 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 0 && volume == 2)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderBy(p => p.NowPrice).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderBy(p => p.NowPrice).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 1 && volume == 2)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderBy(p => p.NowPrice).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 2 && volume == 0)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderByDescending(p => p.NowPrice).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            if (price == 2 && volume == 1)
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderByDescending(p => p.NowPrice).OrderBy(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                var pro = fs.Product.Where(p => p.FlowerKindId == Id).OrderByDescending(p => p.NowPrice).OrderByDescending(p => p.ProductVolume).ToList();
                                foreach (var item in pro)
                                {
                                    array.Add(item.ProductId);
                                    array.Add(item.ProductName);
                                    array.Add(item.Picture1);
                                    array.Add(item.NowPrice);
                                    array.Add(item.FlowerLanguage);
                                }
                                return Json(array, JsonRequestBehavior.AllowGet);
                            }
                        }
                    default:
                        {
                            return Json(table, JsonRequestBehavior.AllowGet);
                        }
                }
            }

        }
        #endregion
       
        #region 所有鲜花组合查询
        public ActionResult AllFlowers()
        {
            Bind();
            return View();
        }
        public ActionResult SelectList(int FestivalId, int ColorId, int FlowerKindId)
        {
            ArrayList array = new ArrayList();
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                //111
                if (FestivalId != 0 && ColorId != 0 && FlowerKindId != 0)
                {
                    var flower = fs.Product.Where(p => p.FestivalId == FestivalId && p.ColorId == ColorId && p.FlowerKindId == FlowerKindId);
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                //110
                if (FestivalId != 0 && ColorId != 0 && FlowerKindId == 0)
                {
                    var flower = fs.Product.Where(p => p.FestivalId == FestivalId && p.ColorId == ColorId);
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                //101
                if (FestivalId != 0 && ColorId == 0 && FlowerKindId != 0)
                {
                    var flower = fs.Product.Where(p => p.FestivalId == FestivalId && p.FlowerKindId == FlowerKindId);
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                //100
                if (FestivalId != 0 && ColorId == 0 && FlowerKindId == 0)
                {
                    var flower = fs.Product.Where(p => p.FestivalId == FestivalId);
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                //011
                if (FestivalId == 0 && ColorId != 0 && FlowerKindId != 0)
                {
                    var flower = fs.Product.Where(p => p.ColorId == ColorId && p.FlowerKindId == FlowerKindId);
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                //010
                if (FestivalId == 0 && ColorId != 0 && FlowerKindId == 0)
                {
                    var flower = fs.Product.Where(p => p.ColorId == ColorId);
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                //001
                if (FestivalId == 0 && ColorId == 0 && FlowerKindId != 0)
                {
                    var flower = fs.Product.Where(p => p.FlowerKindId == FlowerKindId);
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                //000
                if (FestivalId == 0 && ColorId == 0 && FlowerKindId == 0)
                {
                    //var flower = fs.Product.Where(p => p.FlowerKindId == FlowerKindId);
                    List<Product> flower = fs.Product.ToList();
                    foreach (var item in flower)
                    {
                        array.Add(item.ProductId);
                        array.Add(item.Picture1);
                        array.Add(item.ProductName);
                        array.Add(item.NowPrice);
                        array.Add(item.FlowerLanguage);
                    }
                }
                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SelectCount(int FestivalId, int ColorId, int FlowerKindId)
        {
            int Count;
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                if (FestivalId == 0 && ColorId == 0 && FlowerKindId == 0)
                {
                    Count = fs.Product.Count();
                    return Json(Count, JsonRequestBehavior.AllowGet);
                }
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
        #region 搜索框
        public ActionResult Search(string search, int pageIndex = 1)
        {
            List<Product> product = new List<Product>();
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                for (int i = 0; i < search.Length; i++)
                {
                    string s = search[i].ToString();
                    List<Product> pi = fs.Product.Where(f => f.ProductName.Contains(s)).ToList();
                    (product as List<Product>).AddRange(pi);
                }
                for (int k = 0; k < product.Count(); k++)
                {
                    for (int k1 = k + 1; k1 < product.Count(); k1++)
                    {
                        if (product[k].ProductId == product[k1].ProductId)
                        {
                            product.RemoveAt(k1);
                            k1--;
                        }
                    }
                }
                if (product.Count() == 0)
                {
                    List<Product> flower = fs.Product.ToList();
                    (product as List<Product>).AddRange(flower);
                }
                return View(product.ToPagedList(pageIndex, 12));
            }
        }
   
        #endregion
        private void Bind()
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Festival> f_list = fs.Festival.ToList();
                List<Color> c_list = fs.Color.ToList();
                List<FlowerKind> k_list = fs.FlowerKind.ToList();
                SelectList lis_fes = new SelectList(f_list, "FestivalId", "FestivalName");
                SelectList lis_color = new SelectList(c_list, "ColorId", "ColorName");
                SelectList lis_kind = new SelectList(k_list, "FlowerKindId", "FlowerKindName");
                ViewBag.Festival = lis_fes;
                ViewBag.Color = lis_color;
                ViewBag.FlowerKind = lis_kind;
            }
        }
        
        public ActionResult GetJson()
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Product> pro = fs.Product.Where(p => p.FestivalId == 4).ToList();
                return Json(pro, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}
