using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flower.Models;

namespace Flower.Controllers
{
    public class PersonalController : Controller
    {
        //
        // GET: /Personal/

        [HttpGet]
        public ActionResult Index()
        {

            string name = Session["LoginUser"] as string;
            using (FlowersSIMEntities dc = new FlowersSIMEntities())
            {
                Members ad = dc.Members.FirstOrDefault(a => a.MembersName == name);
                var list = dc.Members.FirstOrDefault(l => l.MembersId == ad.MembersId);
                return View(list);
            }


        }
        [HttpGet]
        public ActionResult MenberEdit(string id)
        {
            int memberId;
            if (!int.TryParse(id, out memberId))
            {
                return Content("<script>alert('参数不合法!');window.location.href = '/Personal/Index'</script>");
            }
            //2、退款
            using (FlowersSIMEntities dc = new FlowersSIMEntities())
            {
                //2.1 先查询
                Members entity = dc.Members.FirstOrDefault(u => u.MembersId == memberId);
                //2.2 在数据上下文中修改状态值
                if (entity != null)
                {
                    return View(entity);

                }
                //2.3 保存回数据库中
                if (dc.SaveChanges() > 0)
                {
                    return Content("<script>alert('修改成功!');window.location.href = '/Personal/Index'</script>");
                }
                else
                {
                    return Content("<script>alert('修改失败!');window.location.href = '/Personal/Index'</script>");
                }
            }
        }

        [HttpPost]
        public ActionResult MenberEdit(Members enty)
        {
            using (FlowersSIMEntities dc = new FlowersSIMEntities())
            {
                //2.1 先查询
                Members ape = dc.Members.FirstOrDefault(u => u.MembersId == enty.MembersId);

                //2.2 在数据上下文中修改状态值
                if (ape != null)
                {
                    ape.MembersName = enty.MembersName;
                    ape.MembersPwd = enty.MembersPwd;
                    ape.Phone = enty.Phone;
                    ape.ConfirmPwd = enty.ConfirmPwd;
                    ape.LoginStatus = enty.LoginStatus;
                }
                //2.3 保存回数据库中
                if (dc.SaveChanges() > 0)
                {
                    return Content("<script>alert('修改成功!');window.location.href = '/Personal/Index'</script>");
                }
                else
                {
                    if (ape != null)
                    {
                        return Content("<script>alert('没有进行修改!');window.location.href = '/Personal/Index'</script>");
                    }
                    else {
                        return Content("<script>alert('修改失败!');window.location.href = '/Personal/Index'</script>");
                    }
                  
                }
            }
        }
    }
}
