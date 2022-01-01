using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class ProductType
    {
    [Key]
    public int typeid { get; set; }
    [Required(),Display(Name ="نوع المنتج"),StringLength(250)]
    public string proytpe { get; set; }

}

