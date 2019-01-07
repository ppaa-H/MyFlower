using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Flower.Models
{
    public class OrdersView
    {

        [DisplayName("寄件人")]
        [Required(ErrorMessage = "{0}不能为空")]
        //[Remote("CheckMembersName", "Members", HttpMethod = "get", ErrorMessage = "{0}已存在，请重新输入")]
        public string OrdersName { get; set; }

        [DisplayName("寄件人电话")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string OrdersPhone { get; set; }

        [DisplayName("收件人")]
        [Required(ErrorMessage = "{0}不能为空")]
        //[Remote("CheckMembersName", "Members", HttpMethod = "get", ErrorMessage = "{0}已存在，请重新输入")]
        public string ConsigneeName { get; set; }

        [DisplayName("收件人电话")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string ConsigneePhone { get; set; }

        [DisplayName("收件人地址")]
        [Required(ErrorMessage = "{0}不能为空")]
        //[Remote("CheckMembersName", "Members", HttpMethod = "get", ErrorMessage = "{0}已存在，请重新输入")]
        public string ConsigneeAddress { get; set; }

    }
}