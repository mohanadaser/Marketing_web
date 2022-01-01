using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


    public class Users
    {
    [Key]
    public int id { get; set; }

    [StringLength(250),Required(ErrorMessage = "يجب ادخال اسم المستخدم"),Display(Name ="اسم المستخدم")]
    public string username { get; set; }

    [StringLength(250), Display(Name = "البريدالالكترونى"),DataType(DataType.EmailAddress)]
    public string email { get; set; }

    [StringLength(100, ErrorMessage = "يجب ان يتكون الرقم السرى على الاقل من 6 ارقام او حروف", MinimumLength = 6), Required(), Display(Name = "الرقم السرى"), DataType(DataType.Password)]
    public string password { get; set; }

    [StringLength(100), Required(), Display(Name = "تأكيدالرقم السرى"), DataType(DataType.Password),Compare("password")]
    public string confirmpassword { get; set; }

    [StringLength(250), Required(ErrorMessage = "يجب ادخال رقم التليفون"), Display(Name = "رقم التليفون"), DataType(DataType.PhoneNumber)]
    public string phone { get; set; }

    public bool chkemail { get; set; }




}

