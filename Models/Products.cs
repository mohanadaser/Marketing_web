using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Products
{
    [Key]
    
    public int pro_id { get; set; }
    [Required(ErrorMessage = "يجب ادخال اسم المنتج"), Display(Name = "اسم المنتج"), StringLength(250)]
    public string proname { get; set; }
    [Display(Name = "صورة المنتج"), StringLength(250)]
    public string proimg { get; set; }
    [Display(Name = "وصف المنتج"), StringLength(700)]
    public string prodesc { get; set; }
    [Required(ErrorMessage = "يجب ادخال سعر المنتج"), Display(Name = "سعر المنتج"), DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal price { get; set; }
    
    public int typeid { get; set; }
    [ForeignKey("typeid"), Display(Name = "نوع المنتج")]
    public ProductType productType { get; set; }



}

