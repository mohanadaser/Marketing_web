using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


    public class Shopping_Order
    {
    [Key]
    public int shopping_id { get; set; }
    [Display(Name ="رقم الطلب")]
    public string code_order { get; set; }
    [Required,StringLength(400)]
    [Display(Name = "اسم العميل")]
    public string name { get; set; }
    [Required,StringLength(700)]
    [Display(Name = "العنوان")]
    public string address { get; set; }
    [Required,DataType(DataType.PhoneNumber)]
    [Display(Name = "رقم التليفون")]
    public string phone { get; set; }

    [Display(Name = "وقت الشراء")] 
    public DateTime time_order { get; set; }

    public int pro_id { get; set; }
    [Display(Name = "اسم المنتج")]
    public string pr_oname { get; set; }

    public string pro_image { get; set; }
    [Display(Name = "الكميه")]
    public int qty { get; set; }
    [Display(Name = "السعر")]

    [ Column(TypeName = "decimal(18,2)")]
    public decimal price { get; set; }


}

