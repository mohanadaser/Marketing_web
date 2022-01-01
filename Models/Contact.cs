using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class Contact
    {

    [Key]
    public int id { get; set; }
   
    [Display(Name = "البريد الالكترونى")]
    public string email { get; set; }
    [Required]
    [Display(Name = "اسم المستخدم")]
    public string name { get; set; }
    [Required]
    [StringLength(600), Display(Name = "موضوعك")]
    public string message { get; set; }
    [Required]
    [StringLength(15), Display(Name = "رقم التليفون"),DataType(DataType.PhoneNumber)]
    public string phone_number { get; set; }


}

