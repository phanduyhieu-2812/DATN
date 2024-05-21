using System.ComponentModel.DataAnnotations;
namespace webbanhang.ViewModels
{
    public class AddproductVM
       
    {
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? ProductName { get; set; }
        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public int? Price { get; set; }

        [Display(Name = "Số lượng hàng trong kho")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public double? Quantity { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? Specifications { get; set; }

        
        
        [Display(Name = "Mô tả chi tiết")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? Description { get; set; }


    }
}
