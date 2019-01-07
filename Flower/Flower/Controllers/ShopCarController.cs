using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flower.Models;
using System.Collections;

namespace Flower.Controllers
{
    public class ShopCarController : Controller
    {
        //
        // GET: /ShopCar/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GiveShopCar()
        {
            string name = Session["LoginUser"] as string;
            ArrayList array = new ArrayList();
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                Members member = fs.Members.FirstOrDefault(m => m.MembersName == name);
                var car = fs.ShoppingCar.Where(c => c.MembersId == member.MembersId);
                foreach (var item in car)
                {
                    var pro = fs.Product.Where(p => p.ProductId == item.ProductId).ToList(); ;
                    foreach (var item1 in pro)
                    {
                        array.Add(item.CarId);
                        array.Add(item1.ProductName);
                        array.Add(item1.Picture1);
                        array.Add(item1.FlowerLanguage);
                        array.Add(item.Price);
                        array.Add(item.Num);
                        array.Add(item.ProductId);
                    }
                }
            }
            return Json(array, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditShopCar(int afternum,int id)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                ShoppingCar car = fs.ShoppingCar.FirstOrDefault(c => c.CarId == id);
                Product pro = fs.Product.FirstOrDefault(p => p.ProductId == car.ProductId);
                car.Num = afternum;
                car.Price = afternum * pro.NowPrice;
                decimal price = afternum * pro.NowPrice;
                fs.SaveChanges();
                return Json(price, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteShopCar(int id)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                ShoppingCar car = fs.ShoppingCar.FirstOrDefault(c => c.CarId == id);
                fs.ShoppingCar.Remove(car);
                fs.SaveChanges();
                return Json("删除成功", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddCar(int id,int Num)
        {
            int membersId = Convert.ToInt32(Session["LoginId"]);
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                ShoppingCar shop = fs.ShoppingCar.FirstOrDefault(s => s.MembersId == membersId && s.ProductId == id);
                if (shop != null)
                {
                    //return Content("<script>alert('您已加入购物车');window.location.href='/Home/Detailed?id=" + id + "'</script>");
                    return Json(-1, JsonRequestBehavior.AllowGet);
                }
                else 
                { 
                 Product pro = fs.Product.FirstOrDefault(p => p.ProductId == id);
                ShoppingCar shopcar = new ShoppingCar();
                shopcar.MembersId = membersId;
                shopcar.NowPrice = pro.NowPrice;
                shopcar.Num = Num;
                shopcar.Price = pro.NowPrice*Num;
                shopcar.ProductId = id;
                fs.ShoppingCar.Add(shopcar);
                fs.SaveChanges();
                return Json("添加成功", JsonRequestBehavior.AllowGet);
                }
            }
           
        }
        public ActionResult ChangeId(string list)
        {
            string array = "";
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                if (list.Length == 1)
                {
                    int id = Convert.ToInt32(list);
                    ShoppingCar shop = fs.ShoppingCar.FirstOrDefault(s => s.CarId == id);
                    return Json(shop.ProductId, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string[] idlist = list.Split(',');
                    for (int i = 0; i < idlist.Length; i++)
                    {
                        int id = Convert.ToInt32(idlist[i]);
                        ShoppingCar shop = fs.ShoppingCar.FirstOrDefault(s => s.CarId == id);
                        if (i == (idlist.Count() - 1))
                        {
                            array = "" + array + "," + shop.ProductId + "";
                        }
                        if (idlist.Count() == 1)
                        {
                            array = "" + shop.ProductId + "";
                        }
                        if (i == 0)
                        {
                            array = "" + shop.ProductId + "";
                        }
                        if (i != (idlist.Count() - 1) && i != 0)
                        {
                            array = "" + array + "," + shop.ProductId + "";
                        }
                    }
                    return Json(array, JsonRequestBehavior.AllowGet);
                }

            }
            
        }
        public ActionResult Money(string array)
        {
            if (array == "")
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int money = 0;
                string[] arr = array.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    int carid = Convert.ToInt32(arr[i]);
                    using (FlowersSIMEntities fs = new FlowersSIMEntities())
                    {
                        ShoppingCar shop = fs.ShoppingCar.FirstOrDefault(s => s.CarId == carid);
                        money += Convert.ToInt32(shop.NowPrice * shop.Num);
                    }
                }
                return Json(money, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetInventory(int id)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                ShoppingCar shop = fs.ShoppingCar.FirstOrDefault(s => s.CarId == id);
                Product pro = fs.Product.FirstOrDefault(p => p.ProductId == shop.ProductId);
                return Json(pro.Inventory, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
