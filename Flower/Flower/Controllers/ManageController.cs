using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flower.Models;
using System.IO;
using Webdiyer.WebControls.Mvc;
using Flower.Controllers;
using System.Data.SqlClient;
using System.Collections;

namespace flower.Controllers
{
    public class ManageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region 用户管理

        //用户信息表单
        [HttpGet]
        public ActionResult MembersManage(int pageIndex = 1)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                //使用EF查询用户列表信息
                List<Members> list = fs.Members.ToList().ToPagedList(pageIndex, 10);
                //返回视图，顺带传递用户集合到视图中进行显示
                return View(list);
            }
        }
        [HttpPost]
        public ActionResult MembersManage(string MembersName, string gender, string phone, int id = 1)
        {
            return MembersSearch(MembersName, gender, phone, id);
        }
        private ActionResult MembersSearch(string MembersName, string gender, string phone, int id = 1)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Members> list = fs.Members.ToList();
                if (!string.IsNullOrWhiteSpace(MembersName) || !string.IsNullOrWhiteSpace(gender) || !string.IsNullOrWhiteSpace(phone))
                {
                    if (!string.IsNullOrWhiteSpace(MembersName))
                        list = list.Where(a => a.MembersName.Contains(MembersName)).ToPagedList(1, 100); ;

                    if (gender != null)
                    {
                        int gender1 = int.Parse(gender);
                        list = list.Where(a => a.Gender == gender1).ToPagedList(1, 100);
                    }

                    if (!string.IsNullOrWhiteSpace(phone))
                        list = list.Where(a => a.Phone.Contains(phone)).ToPagedList(1, 100); ;
                    return View(list.ToPagedList(1, 10));
                }
                else
                    return Content("<script>alert('请输入要查询的内容');window.location.href = '/Manage/MembersManage'</script>");
            }
        }

        //用户信息添加
        [HttpGet]
        public ActionResult MembersAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MembersAdd(Members entity)
        {
            ModelState.Remove("MembersId");
            if (ModelState.IsValid)
            {
                using (FlowersSIMEntities fs = new FlowersSIMEntities())
                {
                    bool result = fs.Members.Count(m => m.MembersName == entity.MembersName) == 0;
                    if (result)
                    {
                        entity.LoginStatus = 0;
                        fs.Members.Add(entity);
                        if (fs.SaveChanges() > 0)
                        {
                            return Content("<script language='javascript' type='text/javascript'>alert('增加成功!');window.location.href = '/Manage/MembersManage'</script>");
                        }
                        else
                        {
                            return Content("<script>alert('增加失败!');window.location.href = '/Manage/MembersManage'</script>");
                        }
                    }
                    else
                    {
                        return Content("<script>alert('该用户名已存在，请重新输入!');window.location.href = '/Manage/MembersAdd'</script>");
                    }
                }
            }
            return View(entity);
        }

        //检查会员用户名唯一性
        [HttpGet]
        public ActionResult CheckMembersName(string membersName)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                //在用户表中，查询用户名等于userName参数值的记录的条数，并且与0进行比较。
                //如果记录条数为1，此时result为false，如果记录条数为0，此时result为true
                bool result = fs.Members.Count(m => m.MembersName == membersName) == 0;
                //返回JSON格式的结果
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //用户信息删除
        public ActionResult MembersDelete(string id)
        {
            int membersId;
            if (!int.TryParse(id, out membersId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/Manage/MembersManage'</script>");
            }
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                Members entity = fs.Members.FirstOrDefault(m => m.MembersId == membersId);
                if (entity != null)
                {
                    fs.Members.Remove(entity);
                }
                if (fs.SaveChanges() > 0)
                {
                    return Content("<script>alert('删除成功!');window.location.href = '/Manage/MembersManage'</script>");
                }
                else
                {
                    return Content("<script>alert('删除失败!');window.location.href = '/Manage/MembersManage'</script>");
                }
            }
        }

        //用户信息修改
        [HttpGet]
        public ActionResult MembersEdit(string id)
        {
            int membersId;
            if (!int.TryParse(id, out membersId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/Manage/MembersManage'</script>");
            }
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                Members entity = fs.Members.FirstOrDefault(m => m.MembersId == membersId);
                if (entity != null)
                {
                    return View(entity);
                }
                else
                {
                    return Redirect("/Manage/MembersManage");
                }
            }
        }
        [HttpPost]
        public ActionResult MembersEdit(Members entity)
        {
            if (ModelState.IsValid)
            {
                using (FlowersSIMEntities fs = new FlowersSIMEntities())
                {
                    Members model = fs.Members.FirstOrDefault(m => m.MembersId == entity.MembersId);
                    if (model != null)
                    {
                        model.MembersName = entity.MembersName;
                        model.MembersPwd = entity.MembersPwd;
                        model.Phone = entity.Phone;
                        model.Gender = entity.Gender;
                        model.LoginStatus = 0;
                    }
                    fs.Configuration.ValidateOnSaveEnabled = false;

                    if (fs.SaveChanges() > 0)
                    {
                        fs.Configuration.ValidateOnSaveEnabled = true;
                        return Content("<script>alert('修改成功');window.location.href = '/Manage/MembersManage'</script>");
                    }
                    else
                    {
                        if (model != null)
                        {
                            return Content("<script>alert('未做任何修改，修改成功');window.location.href = '/Manage/MembersManage'</script>");
                        }
                        else
                        {
                            return Content("<script>alert('修改失败');window.location.href = '/Manage/MembersManage'</script>");
                        }
                    }
                }
            }
            return View(entity);
        }
        #endregion*

        #region 商品管理

        //商品信息表单
        [HttpGet]
        public ActionResult ProductManage(int pageIndex = 1)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                //使用EF查询用户列表信息
                Bind();
                List<Product> list = fs.Product.ToList().ToPagedList(pageIndex, 10);
                //返回视图，顺带传递用户集合到视图中进行显示
                return View(list);
            }
        }

        //商品信息表单查询
        [HttpPost]
        public ActionResult ProductManage(string productName, string material, string flowerLanguage)
        {
            return ProductSearch(productName, material, flowerLanguage);
        }
        private ActionResult ProductSearch(string productName, string material, string flowerLanguage)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Product> list = fs.Product.ToList();
                if (!string.IsNullOrWhiteSpace(productName) || !string.IsNullOrWhiteSpace(material) || !string.IsNullOrWhiteSpace(flowerLanguage))
                {
                    if (!string.IsNullOrWhiteSpace(productName))
                        list = list.Where(a => a.ProductName.Contains(productName)).ToPagedList(1, 10);

                    if (!string.IsNullOrWhiteSpace(material))
                        list = list.Where(a => a.Material.Contains(material)).ToPagedList(1, 10);

                    if (!string.IsNullOrWhiteSpace(flowerLanguage))
                        list = list.Where(a => a.FlowerLanguage.Contains(flowerLanguage)).ToPagedList(1, 10);
                    return View(list.ToPagedList(1, 10));
                }
                else
                    return Content("<script>alert('请输入要查询的内容');window.location.href = '/Manage/ProductManage'</script>");
            }
        }

        //商品信息添加
        [HttpGet]
        public ActionResult ProductAdd()
        {
            Bind();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Product entity)
        {
            Bind();
            ModelState.Remove("ProductId");

            //获取上传图片路径
            HttpPostedFileBase file = Request.Files["imgFile"];
            var ext = Path.GetExtension(file.FileName);
            string path = "/Image/" + Guid.NewGuid().ToString() + file.FileName;
            file.SaveAs(Request.MapPath(path));
            entity.Picture1 = path;

            if (ModelState.IsValid)
            {
                using (FlowersSIMEntities fs = new FlowersSIMEntities())
                {
                    bool result = fs.Product.Count(m => m.ProductName == entity.ProductName) == 0;
                    if (result)
                    {
                        fs.Product.Add(entity);
                        if (fs.SaveChanges() > 0)
                        {
                            return Content("<script>alert('增加成功!');window.location.href = '/Manage/ProductManage'</script>");
                        }
                        else
                        {
                            return Content("<script>alert('增加失败!');window.location.href = '/Manage/ProductManage'</script>");
                        }
                    }
                    else
                    {
                        return Content("<script>alert('该商品名称存在，请重新输入!');window.location.href = '/Manage/ProductAdd'</script>");
                    }
                }
            }
            return View(entity);
        }

        //商品信息删除
        public ActionResult ProductDelete(string id)
        {
            int productId;
            if (!int.TryParse(id, out productId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/Manage/ProductManage'</script>");
            }
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                Product entity = fs.Product.FirstOrDefault(p => p.ProductId == productId);
                if (entity != null)
                {
                    fs.Product.Remove(entity);
                }
                if (fs.SaveChanges() > 0)
                {
                    return Content("<script>alert('删除成功!');window.location.href = '/Manage/ProductManage'</script>");
                }
                else
                {
                    return Content("<script>alert('删除失败!');window.location.href = '/Manage/ProductManage'</script>");
                }
            }
        }

        //商品信息修改
        [HttpGet]
        public ActionResult ProductEdit(string id)
        {

            Bind();
            int productId;
            if (!int.TryParse(id, out productId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/Manage/ProductManage'</script>");
            }
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                Product entity = fs.Product.FirstOrDefault(u => u.ProductId == productId);
                if (entity != null)
                {
                    return View(entity);
                }
                else
                {
                    return Redirect("/Manage/ProductManage");
                }
            }
        }
        [HttpPost]
        public ActionResult ProductEdit(Product entity)
        {
            Bind();

            //获取上传图片路径
            HttpPostedFileBase file = Request.Files["imgFile"];
            var ext = Path.GetExtension(file.FileName);
            if (ext != "")
            {
                string path = "/Image/" + Guid.NewGuid().ToString() + file.FileName;
                file.SaveAs(Request.MapPath(path));
                entity.Picture1 = path;
            }


            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                Product model = fs.Product.FirstOrDefault(p => p.ProductId == entity.ProductId);
                if (model != null)
                {
                    model.ProductName = entity.ProductName;
                    model.Material = entity.Material;
                    model.Package = entity.Package;
                    model.FlowerLanguage = entity.FlowerLanguage;
                    if (entity.Picture1 != null)
                    {
                        model.Picture1 = entity.Picture1;
                    }
                    model.OriginalPrice = entity.OriginalPrice;
                    model.NowPrice = entity.NowPrice;
                    model.Inventory = entity.Inventory;
                    model.FestivalId = entity.FestivalId;
                    model.ColorId = entity.ColorId;
                    model.FlowerKindId = entity.FlowerKindId;
                }
                if (fs.SaveChanges() > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href = '/Manage/ProductManage'</script>");
                }
                else
                {
                    if (model != null)
                    {
                        return Content("<script>alert('未做任何修改，修改成功');window.location.href = '/Manage/ProductManage'</script>");
                    }
                    else
                    {
                        return Content("<script>alert('修改失败');window.location.href = '/Manage/ProductManage'</script>");
                    }

                }
            }
        }

        #region 绑定商品信息下拉列表框数据
        private void Bind()
        {
            BindFestival();
            Binfsolor();
            BindFlowerKind();
        }
        private void BindFestival()
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Festival> list = fs.Festival.ToList();
                SelectList jrlist = new SelectList(list, "FestivalId", "FestivalName");
                ViewBag.Festival = jrlist;
            }
        }
        private void Binfsolor()
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Color> list = fs.Color.ToList();
                SelectList yslist = new SelectList(list, "ColorId", "ColorName");
                ViewBag.Color = yslist;
            }
        }
        private void BindFlowerKind()
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<FlowerKind> list = fs.FlowerKind.ToList();
                SelectList hclist = new SelectList(list, "FlowerKindId", "FlowerKindName");
                ViewBag.FlowerKind = hclist;
            }
        }
        #endregion
        #endregion

        #region 订单管理
        [HttpGet]
        public ActionResult OrdersManage(int pageIndex = 1)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                //使用EF查询用户列表信息
                List<Orders> list = fs.Orders.ToList().ToPagedList(pageIndex, 9);
                //返回视图，顺带传递用户集合到视图中进行显示
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult OrdersManage(string ordersName, int id = 1)
        {
            return OrdersSearch(ordersName, id);
        }
        private ActionResult OrdersSearch(string ordersName, int id = 1)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Orders> list = fs.Orders.ToList();
                if (!string.IsNullOrWhiteSpace(ordersName))
                {
                    if (!string.IsNullOrWhiteSpace(ordersName))
                        list = list.Where(a => a.OrdersName.Contains(ordersName)).ToPagedList(1, 9);
                    return View(list.ToPagedList(1, 9));
                }
                else
                    return Content("<script>alert('请输入要查询的内容');window.location.href = '/Manage/OrdersManage'</script>");
            }
        }
        //订单管理的筛选
        public ActionResult OrdersFilter(string id)
        {
            int ordersFilterId = int.Parse(id);
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Orders> list = fs.Orders.ToList();
                list = list.Where(a => a.Status == ordersFilterId).ToPagedList(1, 9);
                return View("OrdersManage", list.ToPagedList(1, 9));
            }
        }

        [HttpGet]
        public ActionResult OrdersList(string ordersId, int pageIndex = 1)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                int id = Convert.ToInt32(ordersId);
                //获取当前查看的订单信息并用ViewBag.Orders存放信息
                Orders entity = fs.Orders.FirstOrDefault(o => o.OrdersId == id);
                ViewBag.Orders = entity;
                string[] productIdString = entity.ProductIdList.Split(',');
                int[] productIdArray = Array.ConvertAll(productIdString, int.Parse);
                //每次加载订单商品信息的第一页时重新进行商品信息的添加
                if (pageIndex < productIdArray.Length || productIdArray.Length == 1)
                {
                    GetOrdersList(id);
                }
                entity.Price = GetOrdersPrice();
                fs.SaveChanges();
                ViewBag.Price = entity.Price;

                List<OrderGoods> list = fs.OrderGoods.ToList().ToPagedList(pageIndex, 2);
                return View(list);
            }
        }
        //获取该订单内的商品信息
        public void GetOrdersList(int ordersId)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                //清除存放订单内商品信息的临时数据表OrderGoods内的数据
                List<OrderGoods> list = fs.OrderGoods.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    int ogId = list[i].OGId;
                    SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@ogId",ogId),
                    };
                    fs.Database.ExecuteSqlCommand("delete from OrderGoods where OGId=@ogId", paras);
                }

                Orders entity1 = fs.Orders.FirstOrDefault(o => o.OrdersId == ordersId);
                string[] productIdString = entity1.ProductIdList.Split(',');
                int[] productIdArray = Array.ConvertAll(productIdString, int.Parse);

                string[] productNumString = entity1.OrderNumbers.Split(',');
                int[] productNumArray = Array.ConvertAll(productNumString, int.Parse);

                decimal Price = 0;
                OrderGoods entity = new OrderGoods();
                for (int i = 0; i < productIdArray.Length; i++)
                {
                    int productId = productIdArray[i];
                    Product IdToName = fs.Product.FirstOrDefault(p => p.ProductId == productId);
                    entity.Picture1 = IdToName.Picture1;
                    entity.ProductName = IdToName.ProductName;
                    entity.Num = productNumArray[i];
                    entity.NowPrice = IdToName.NowPrice;
                    string num1 = Convert.ToString(entity.Num);
                    decimal num = decimal.Parse(num1);
                    decimal nowPrice = Convert.ToDecimal(entity.NowPrice);
                    entity.Price = num * nowPrice;
                    Price += num * nowPrice;
                    fs.OrderGoods.Add(entity);
                    fs.SaveChanges();
                }
                ViewBag.Price = Price;
            }
        }
        //获取并计算该订单的商品总额
        public decimal GetOrdersPrice()
        {
            decimal Price = 0;
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<OrderGoods> list = fs.OrderGoods.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    string num1 = Convert.ToString(list[i].Num);
                    decimal num = decimal.Parse(num1);
                    decimal nowPrice = Convert.ToDecimal(list[i].NowPrice);
                    Price += num * nowPrice;
                }
            }
            return Price;
        }
        public ActionResult OrderOperation(string id)
        {
            int ordersId;
            if (!int.TryParse(id, out ordersId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/Manage/OrdersManage'</script>");
            }
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                Orders entity = fs.Orders.FirstOrDefault(u => u.OrdersId == ordersId);
                if (entity != null)
                {
                    if (entity.Status == 1)
                    {
                        entity.Status = 2;
                        Orders entity1 = fs.Orders.FirstOrDefault(o => o.OrdersId == ordersId);
                        string[] productIdString = entity1.ProductIdList.Split(',');
                        int[] productIdArray = Array.ConvertAll(productIdString, int.Parse);

                        string[] productNumString = entity1.OrderNumbers.Split(',');
                        int[] productNumArray = Array.ConvertAll(productIdString, int.Parse);

                        for (int i = 0; i < productIdArray.Length; i++)
                        {
                            int productId = productIdArray[i];
                            Product model = fs.Product.FirstOrDefault(p => p.ProductId == productId);
                            model.Inventory -= productNumArray[i];
                            if (model.Inventory < 0)
                            {
                                return Content("<script>alert('库存不足无法发货，请及时增加库存!');window.location.href = '/Manage/OrdersList/?ordersId=" + id + "'</script>");
                            }
                        }
                    }
                    if (entity.Status == 4)
                    {
                        entity.Status = 5;
                        Orders entity1 = fs.Orders.FirstOrDefault(o => o.OrdersId == ordersId);
                        string[] productIdString = entity1.ProductIdList.Split(',');
                        int[] productIdArray = Array.ConvertAll(productIdString, int.Parse);
                        string[] productNumString = entity1.OrderNumbers.Split(',');
                        int[] productNumArray = Array.ConvertAll(productIdString, int.Parse);

                        for (int i = 0; i < productIdArray.Length; i++)
                        {
                            int productId = productIdArray[i];
                            Product model = fs.Product.FirstOrDefault(p => p.ProductId == productId);
                            model.Inventory += productNumArray[i];
                        }
                    }
                }

                if (fs.SaveChanges() > 0)
                {
                    return Content("<script>alert('订单操作成功!');window.location.href = '/Manage/OrdersList/?ordersId=" + id + "'</script>");
                }
                else
                {
                    return Content("<script>alert('订单操作失败!');window.location.href = '/Manage/OrdersManage/?ordersId=" + id + "'</script>");
                }
            }
        }
        #endregion

        #region 查询操作
        // 参数1为传入的查询内容 参数2为进行查询操作的表
        private void NameSearch(string name, string tableName)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                //根据页面中传过来的表名参数来调用对应表的按名称查询的方法
                if (tableName == "Members")
                {
                    List<Members> list = fs.Members.ToList().ToPagedList(1, 10);
                    List<Members> list2 = new List<Members>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].MembersName.ToString().Contains(name))
                        {
                            list2.Add(list[i]);
                        }
                    }
                    ViewBag.Search = list2;
                }
                if (tableName == "Product")
                {
                    List<Product> list = fs.Product.ToList().ToPagedList(1, 10);
                    List<Product> list2 = new List<Product>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].ProductName.ToString().Contains(name))
                        {
                            list2.Add(list[i]);
                        }
                    }
                    ViewBag.Search = list2;
                }
                if (tableName == "Orders")
                {
                    List<Orders> list = fs.Orders.ToList().ToPagedList(1, 10);
                    List<Orders> list2 = new List<Orders>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].OrdersName.ToString().Contains(name))
                        {
                            list2.Add(list[i]);
                        }
                    }
                    ViewBag.Search = list2;
                }
            }
        }
        private void MaterialSearch(string material, string tableName)
        {
            //按花材名称查询
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                List<Product> list = fs.Product.ToList().ToPagedList(1, 10);
                List<Product> list2 = new List<Product>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ProductName.ToString().Contains(material))
                    {
                        list2.Add(list[i]);
                    }
                }
                ViewBag.Search = list2;
            }
        }

        #endregion

        #region 批量删除
        public ActionResult BatchDelete(string idArray, string tableName)
        {
            //判断是否有选中数据
            if (idArray == "" || tableName == "")
            {
                return Content("<script>alert('您还没选中，请先选中至少一项数据再进行操作!');window.location.href = '/Manage/" + tableName + "Manage'</script>");
            }
            //将传进来的所选数据的编号数组进行分割
            string[] deleteValue = idArray.Split(',');
            int[] deleteId = Array.ConvertAll(deleteValue, int.Parse);
            for (int i = 0; i < deleteId.Length; i++)
            {
                using (FlowersSIMEntities fs = new FlowersSIMEntities())
                {
                    int id = deleteId[i];
                    //删除该表名内的数据及其与其他表相关联的数据
                    if (tableName == "Members")
                    {
                        ShoppingCar entity = fs.ShoppingCar.FirstOrDefault(p => p.MembersId == id);
                        if (entity != null)
                        {
                            fs.ShoppingCar.Remove(entity);
                            fs.SaveChanges();
                        }
                        Orders entity1 = fs.Orders.FirstOrDefault(p => p.MembersId == id);
                        if (entity1 != null)
                        {
                            fs.Orders.Remove(entity1);
                            fs.SaveChanges();
                        }
                        Members entity2 = fs.Members.FirstOrDefault(p => p.MembersId == id);
                        if (entity2 != null)
                        {
                            fs.Members.Remove(entity2);
                            fs.SaveChanges();
                        }
                    }
                    if (tableName == "Product")
                    {
                        Product entity = fs.Product.FirstOrDefault(p => p.ProductId == id);
                        if (entity != null)
                        {
                            fs.Product.Remove(entity);
                            fs.SaveChanges();
                        }
                    }
                    if (tableName == "Orders")
                    {
                        Orders entity = fs.Orders.FirstOrDefault(p => p.OrdersId == id);
                        if (entity != null)
                        {
                            fs.Orders.Remove(entity);
                            fs.SaveChanges();
                        }
                    }
                    if (i == deleteId.Length - 1)
                    {
                        return Content("<script>alert('删除成功!,已删除" + deleteId.Length + "项数据');window.location.href = '/Manage/" + tableName + "Manage'</script>");
                    }
                }
            }
            return View();
        }
        #endregion
    }
}
