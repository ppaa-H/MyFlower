using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flower.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    [MetadataType(typeof(MembersView))]
    public partial class Members
    {
        [DisplayName("确认密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [System.Web.Mvc.Compare("MembersPwd", ErrorMessage = "密码不一致")]
        public string ConfirmPwd { get; set; }
    }
}