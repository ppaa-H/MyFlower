using Flower.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flower.Controllers
{
    public class LoginController : Controller
    {

        //
        // GET: /Login/
        private string Vcode(int count)
        {
            //实例化
            Random r = new Random();
            //定义随机字符串
            string code = string.Empty;
            //定义备选的字符库
            string str = "0123456789abcdefghijkmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
            //循环遍历
            for (int i = 0; i < count; i++)
            {
                //获取随机的索引 0 ~ str.Length - 1 
                int index = r.Next(str.Length);
                //获取随机索引位置上的字符，添加到code变量中
                code += str[index];
            }
            //返回随机字符串
            return code;
        }
        public ActionResult GetVcode()
        {
            //调用方法，获取随机字符串
            string vcode = Vcode(4);
            Session["ValidateCode"] = vcode;
            //获取画布的宽度
            int width = (int)(vcode.Length * 12.5);
            //创建画布对象
            //using块的好处：保证创建的对象，能够在执行using块中的代码后，自动释放所占用的资源
            using (Image img = new Bitmap(width, 20))
            {
                //创建画家对象
                using (Graphics g = Graphics.FromImage(img))
                {
                    //开始绘画
                    //将画布清空为白色
                    g.Clear(System.Drawing.Color.White);
                    //绘制矩形边框
                    g.DrawRectangle(Pens.Tomato, 0, 0, width, 20);
                    //定义字体
                    Font f = new Font("黑体", 15, FontStyle.Bold);
                    //绘制字符串
                    g.DrawString(vcode, f, Brushes.Red, 0, 0);
                }
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                img.Save(stream, ImageFormat.Jpeg);
                //内存流转换为二进制数组
                byte[] bytes = stream.ToArray();
                //返回结果
                return File(bytes, @"image/jpeg");
            }
        }

        //登录验证功能
        public ActionResult Login(FormCollection fc)
        {
            string Name = fc["membername"];
            string Pwd = fc["memberpwd"];
            string vcode = fc["vcode"];
            string isremember = fc["isremember"];  //isremember=on 没有勾选复选框的时候则什么都没有
            //2、检查验证码是否为空
            if (string.IsNullOrEmpty(vcode))
            {
                return Content("<script>alert('验证码不能为空！');window.location.href='/Login/LoginOrRigister'</script>");
            }
            //3、判断Session会话是否过期
            if (Session["ValidateCode"] == null)
            {
                return Content("<script>alert('验证码已过期！');window.location.href='/Login/LoginOrRigister'</script>");
            }
            //4、比较用户输入的验证码 和 存放在Session中的验证码 是否一致
            string validateCode = Session["ValidateCode"].ToString();
            if (!validateCode.Equals(vcode, StringComparison.OrdinalIgnoreCase)) //忽略大小写比较两者是否相等
            {
                return Content("<script>alert('验证码输入错误！');window.location.href='/Login/LoginOrRigister'</script>");
            }
            //5、验证用户名和密码正确性
            using (FlowersSIMEntities dc = new FlowersSIMEntities())
            {
                //使用FirstOrDefault方法，查询一个实体
                Admins entity = dc.Admins.FirstOrDefault(a => a.AdminsName == Name && a.AdminsPwd == Pwd);

                //判断 用户名和密码正确
                if (entity != null)
                {
                    //判断用户是否勾选了是否保存登录信息的复选框
                    if (!string.IsNullOrEmpty(isremember))
                    {
                        //将用户名写入到Cookie中
                        HttpCookie cookie = new HttpCookie("loginUser", Name);
                        cookie.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie);
                    }
                    //在TempData中存储用户名
                    //TempData["LoginUser"] = Name;
                    Session["LoginUser"] = Name;

                    return Content("<script>alert('管理员登录成功！');window.location.href='/Manage/Index'</script>");
                }
                else
                {
                    Members meb = dc.Members.FirstOrDefault(m => m.MembersName == Name && m.MembersPwd == Pwd);
                    if (meb != null)
                    {
                        if (Name == "kefu" && Pwd == "123456")
                        {
                            Session["LoginUser"] = Name;
                            Session["LoginId"] = meb.MembersId;
                            return Content("<script>alert('客服登录成功！');window.location.href='/CustomerServer/Index'</script>");
                        }
                        else
                        {
                            //判断用户是否勾选了是否保存登录信息的复选框
                            if (!string.IsNullOrEmpty(isremember))
                            {
                                //将用户名写入到Cookie中
                                HttpCookie cookie = new HttpCookie("loginUser", Name);
                                cookie.Expires = DateTime.Now.AddDays(7);
                                Response.Cookies.Add(cookie);
                            }
                            //在TempData中存储用户名
                            //TempData["LoginUser"] = Name;
                            Session["LoginUser"] = Name;
                            Session["LoginId"] = meb.MembersId;
                            return Content("<script>alert('用户登录成功！');window.location.href='/Home/Index'</script>");
                        }
                    }
                    else
                    {
                        return Content("<script>alert('请使用正确的管理员账号或用户账号重新点击登录');window.location.href='/Home/Index'</script>");
                    }
                }
            }

        }
        public ActionResult Logout()
        {
            if (Session["LoginUser"] != null)
            {
                Session["LoginUser"] = null;
                return Content("<script>;window.location.href='/Home/Index'</script>");
            }

            return View();
        }


        #region 用户注册
        //[HttpGet]  //限定此Action仅能通过Get请求访问      
        //public ActionResult Register(FormCollection fc)
        //{
        //    ViewData["titlebar"] = "用户注册";
        //    return View();
        //}
        //处理视图提交过来的用户数据的功能
        [HttpPost]  //限定此Action仅能通过Post请求访问
        //模型映射规则：实体模型属性 和 目标元素名称 一致
        //MVC会将视图页面中各元素的值，赋值给模型对象entity的同名属性
        public ActionResult Register(Members members)
        {
            members.LoginStatus = 0;
            members.Phone = "123456789";
            using (FlowersSIMEntities dc = new FlowersSIMEntities())
            {
                //增加用户功能
                dc.Members.Add(members);

                if (dc.SaveChanges() > 0)
                {
                    return Content("<script>alert('用户注册成功！');window.location.href='/Main/Index'</script>");
                }
                else
                {
                    return Content("<script>alert('用户注册失败！');window.location.href='/Home/Index'</script>");
                }
            }
        }
        #endregion
        public ActionResult LoginOrRigister()
        {
            return View();
        }
        public ActionResult LoginAndRigister()
        {
            return View();
        }

    }
}
