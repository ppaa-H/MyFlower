using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Flower.Models
{
    public class ProductView
    {
        [DisplayName("商品编号")]
        public int ProductId { get; set; }

        [DisplayName("商品名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string ProductName { get; set; }

        [DisplayName("商品类型")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string ProductVolume { get; set; }

        [DisplayName("按节日分类")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string FestivalId { get; set; }

        [DisplayName("按颜色分类")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string ColorId { get; set; }

        [DisplayName("按花材分类")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string FlowerKindId { get; set; }
        [DisplayName("材料")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Material { get; set; }

        [DisplayName("包装")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Package { get; set; }

        [DisplayName("花语")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string FlowerLanguage { get; set; }

        [DisplayName("原价")]
        public decimal OriginalPrice { get; set; }

        [DisplayName("现价")]
        [Required(ErrorMessage = "{0}不能为空")]
        public decimal NowPrice { get; set; }

        [DisplayName("库存")]
        [Required(ErrorMessage = "{0}不能为空")]
        public int Inventory { get; set; }
    }
}