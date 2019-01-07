using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flower.Models;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;
using System.Data.SqlClient;
using System.Collections;

namespace Flower.Controllers
{

    public class MemberOrdersController : Controller
    {


        // GET: /MemberOrders/

        public ActionResult Index(string id, int pageIndex = 1)
        {

            int ordersId = Convert.ToInt32(id);
            string name = Session["LoginUser"] as string;
            if (!int.TryParse(id, out ordersId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/MemberOrders/OrdersIndex'</script>");
            }
            using (FlowersSIMEntities os = new FlowersSIMEntities())
            {
                Members ber = os.Members.FirstOrDefault(b => b.MembersName == name);
                Orders entity1 = os.Orders.FirstOrDefault(o => o.OrdersId == ordersId);
                ViewBag.Orders = entity1;
                string[] productIdString = entity1.ProductIdList.Split(',');
                int[] productIdArray = Array.ConvertAll(productIdString, int.Parse);
                if (pageIndex < productIdArray.Length || productIdArray.Length == 1)
                {
                    GetOrdersList(id);
                }
                ////使用EF查询用户列表信息
                List<OrderGoods> list = os.OrderGoods.ToList().ToPagedList(pageIndex, 2);
                //返回视图，顺带传递用户集合到视图中进行显示
                return View(list);
            }
        }
        public ActionResult OrderIndex(int pageIndex = 1)
        {
            string name = Session["LoginUser"] as string;

            using (FlowersSIMEntities os = new FlowersSIMEntities())
            {
                Members ber = os.Members.FirstOrDefault(b => b.MembersName == name);
                var list = os.Orders.Where(p => p.MembersId == ber.MembersId).ToList().ToPagedList(pageIndex, 4);
                //var list = os.Orders.Where(o => o.MembersId == ber.MembersId).ToList().ToPagedList(pageIndex, 3);
                return View(list);
            }
        }

        public void GetOrdersList(string id)
        {
            int ordersId = Convert.ToInt32(id);
            using (FlowersSIMEntities os = new FlowersSIMEntities())
            {
                //清除存放订单内商品信息的临时数据表OrderGoods内的数据
                List<OrderGoods> list = os.OrderGoods.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    int ogId = list[i].OGId;
                    SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@ogId",ogId),
                    };
                    os.Database.ExecuteSqlCommand("delete from OrderGoods where OGId=@ogId", paras);
                }

                Orders entity1 = os.Orders.FirstOrDefault(o => o.OrdersId == ordersId);
                ViewBag.Orders = entity1;
                string[] productIdString = entity1.ProductIdList.Split(',');
                int[] productIdArray = Array.ConvertAll(productIdString, int.Parse);
                string[] productNumString = entity1.OrderNumbers.Split(',');
                int[] productNumArray = Array.ConvertAll(productNumString, int.Parse);
                decimal Price = 0;
                OrderGoods entity = new OrderGoods();
                for (int i = 0; i < productIdArray.Length; i++)
                {
                    int productId = productIdArray[i];
                    Product IdToName = os.Product.FirstOrDefault(p => p.ProductId == productId);
                    entity.Picture1 = IdToName.Picture1;
                    entity.ProductName = IdToName.ProductName;
                    entity.Num = productNumArray[i];
                    entity.NowPrice = IdToName.NowPrice;
                    string num1 = Convert.ToString(entity.Num);
                    decimal num = decimal.Parse(num1);
                    decimal nowPrice = Convert.ToDecimal(entity.NowPrice);
                    Price += num * nowPrice;
                    os.OrderGoods.Add(entity);
                    os.SaveChanges();
                }
            }
            ViewBag.a = "a";
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Orders entity)
        {
            string name = Session["LoginUser"] as string;
            string productidlist = Request.Form["ProductIdList"];
            string[] sproductid = productidlist.Split(',');
            int carorproduct = Convert.ToInt32(Request.Form["carOrproduct"]); //获取来源
            int price = 0;
            string num = "";
            #region 将carid转为productid
            string array = "";
            #endregion
            #region 计算数量加入购物车
            using (FlowersSIMEntities dc = new FlowersSIMEntities())
            {
                Orders order = new Orders();
                if (carorproduct == 0)
                {
                    if (productidlist.Length == 1)
                    {
                        int id = Convert.ToInt32(productidlist);
                        ShoppingCar shop = dc.ShoppingCar.FirstOrDefault(s => s.CarId == id);
                        array = Convert.ToString(shop.ProductId);
                    }
                    else
                    {
                        string[] idlist = productidlist.Split(',');
                        for (int i = 0; i < idlist.Length; i++)
                        {
                            int id = Convert.ToInt32(idlist[i]);
                            ShoppingCar shop = dc.ShoppingCar.FirstOrDefault(s => s.CarId == id);
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
                    }
                    for (int i = 0; i < sproductid.Count(); i++)
                    {
                        int id = Convert.ToInt32(sproductid[i]);
                        ShoppingCar shop = dc.ShoppingCar.FirstOrDefault(s => s.CarId == id);
                        price += Convert.ToInt32(shop.NowPrice * shop.Num);
                        if (i == (sproductid.Count() - 1))
                        {
                            //num += "" + shop.Num + "";
                            num = "" + num + "," + shop.Num + "";
                        }
                        if (sproductid.Count() == 1)
                        {
                            //num = "" + shop.Num + "";
                            num = "" + shop.Num + "";
                        }
                        if (i == 0)
                        {
                            num = "" + shop.Num + "";
                        }
                        if (i != (sproductid.Count() - 1) && i != 0)
                        {
                            //num += "" + shop + ",";
                            num = "" + num + "," + shop.Num + "";
                        }
                    }
                    order.OrderNumbers = num;
                    order.Price = price;
                    order.ProductIdList = array;
                }
                else
                {
                    int productid = (Convert.ToInt32(productidlist));
                    Product pro = dc.Product.FirstOrDefault(p => p.ProductId == productid);
                    string Number = Request.Form["Number"];
                    order.OrderNumbers = Number;
                    order.Price = pro.NowPrice * Convert.ToInt32(Number);
                    order.ProductIdList = productidlist;
                }
                Members ber = dc.Members.FirstOrDefault(b => b.MembersName == name);
                order.MembersId = ber.MembersId;
                order.OrdersName = entity.OrdersName;
                order.OrdersPhone = entity.OrdersPhone;
                order.ConsigneeAddress = entity.ConsigneeAddress;
                order.ConsigneeName = entity.ConsigneeName;
                order.ConsigneePhone = entity.ConsigneePhone;

                order.Status = 0;
                order.DeliveryTime = DateTime.Now.Date;
                order.CreateTime = DateTime.Now.Date;
                //添加实体
                dc.Orders.Add(order);
                //保存更新
                dc.SaveChanges();
            }
            #endregion
            return Content("<script>alert('添加订单成功!');window.location.href='/MemberOrders/OrderIndex'</script>");
        }
        #region 订单操作
        public ActionResult Delete(string id)
        {
            //1、验证参数的合法性   字符串参数 => 整型值
            int ordersId;

            if (!int.TryParse(id, out ordersId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/MemberOrders/OrderIndex'</script>");
            }
            //2、订单操作
            using (FlowersSIMEntities dc = new FlowersSIMEntities())
            {
                //2.1 先查询
                Orders entity = dc.Orders.FirstOrDefault(u => u.OrdersId == ordersId);
                //2.2 在数据上下文中修改状态值
                if (entity != null)
                {
                    if (entity.Status == 4)
                    {
                        entity.Status += 1;
                    }
                    if (entity.Status == 1)
                    {
                        entity.Status = 4;
                    }
                    if (entity.Status == 0)
                    {
                        entity.Status += 1;
                    }

                    if (entity.Status == 2)
                    {
                        entity.Status += 1;
                    }

                }
                //2.3 保存回数据库中
                if (dc.SaveChanges() > 0)
                {
                    return Content("<script>alert('操作成功!');window.location.href = '/MemberOrders/Index'</script>");
                }
                else
                {
                    return Content("<script>alert('操作失败!');window.location.href = '/MemberOrders/Index'</script>");
                }
            }
        }
        #endregion
    }
}
