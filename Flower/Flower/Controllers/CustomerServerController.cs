using Flower.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flower.Controllers
{
    public class CustomerServerController : Controller
    {
        //
        // GET: /CustomerServer/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getContent(int id)
        {
            ArrayList array = new ArrayList();
            string name = Session["LoginUser"] as string;
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                if (id == 1)
                {
                    Members mem = fs.Members.FirstOrDefault(m => m.MembersName == name);
                    var chat = fs.Chat.Where(c => c.MembersId == 1 || c.MembersId == mem.MembersId);
                    foreach (var item in chat)
                    {
                        if (item.MembersId == 1)
                        {
                            if (item.SendMemId == mem.MembersId)
                            {
                                Members m = fs.Members.FirstOrDefault(m1 => m1.MembersId == 1);
                                array.Add(item.MembersId);
                                array.Add(m.MembersName);
                                array.Add(item.ChatContent);
                                array.Add(item.ChatCreatTime.ToString());
                            }
                            else
                            { }
                        }
                        else
                        {
                            array.Add(item.MembersId);
                            array.Add(name);
                            array.Add(item.ChatContent);
                            array.Add(item.ChatCreatTime.ToString());
                        }
                    }
                }
                else
                {
                    var chat = fs.Chat.Where(c => c.MembersId == id || c.MembersId == 1);
                    foreach (var item in chat)
                    {
                        if (item.MembersId == 1)
                        {
                            if (item.SendMemId == id)
                            {

                                array.Add(item.MembersId);
                                array.Add(name);
                                array.Add(item.ChatContent);
                                array.Add(item.ChatCreatTime.ToString());
                            }
                            else
                            { }
                        }
                        else
                        {
                            Members m = fs.Members.FirstOrDefault(m1 => m1.MembersId == id);
                            array.Add(item.MembersId);
                            array.Add(m.MembersName);
                            array.Add(item.ChatContent);
                            array.Add(item.ChatCreatTime.ToString());
                        }
                        //Chat cc = fs.Chat.FirstOrDefault(c => c.MembersId == item.MembersId);
                        ////cc.ChatStatus = 1;
                        //fs.SaveChanges();
                    }
                }
                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SendContent(string SendContent, int SendMemId)
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                int Id = Convert.ToInt32(Session["LoginId"]);
                Chat chat = new Chat();
                chat.MembersId = Id;
                chat.ChatContent = SendContent;
                chat.ChatCreatTime = DateTime.Now;
                chat.SendMemId = SendMemId;
                chat.ChatStatus = 0;
                fs.Chat.Add(chat);
                fs.SaveChanges();
            }
            return Json(SendMemId, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getMembersId()
        {
            using (FlowersSIMEntities fs = new FlowersSIMEntities())
            {
                ArrayList array = new ArrayList();
                List<Chat> membersId = fs.Chat.GroupBy(c => c.MembersId).Select(x => x.FirstOrDefault()).ToList();
                foreach (var item in membersId)
                {
                    array.Add(item.MembersId);
                    Members mem = fs.Members.FirstOrDefault(m => m.MembersId == item.MembersId);
                    array.Add(mem.MembersName);
                }
                return Json(array, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
