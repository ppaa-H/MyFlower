using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flower.Models
{
    public class MembersView
    {
        [DisplayName("用户编号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public int MembersId { get; set; }

        [DisplayName("用户名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        //[Remote("CheckMembersName", "Members", HttpMethod = "get", ErrorMessage = "{0}已存在，请重新输入")]
        public string MembersName { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string MembersPwd { get; set; }

        [DisplayName("性别")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Gender { get; set; }

        [DisplayName("电话")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Phone { get; set; }

        //[DisplayName("状态")]
        //[Required(ErrorMessage = "{0}不能为空")]
        //public int LoginStatus { get; set; }
    }
}