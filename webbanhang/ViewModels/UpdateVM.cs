using System.ComponentModel.DataAnnotations;

namespace webbanhang.ViewModels
{
    public class UpdateVM
    {
     
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? ProductName { get; set; }
        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public int? Price { get; set; }

        public long? Views { get; set; }

        public long? Byturn { get; set; }
        [Display(Name = "Số lượng hàng trong kho")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public double? Quantity { get; set; }

        public string? Status { get; set; }

        public string? Type { get; set; }
        [Display(Name = "Mô tả ngắn")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? Specifications { get; set; }

        public string? Image1 { get; set; }
        [Display(Name = "Mô tả chi tiết")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? Description { get; set; }

        public string Tenloai { get; set; }

        public int MaLoai { get; set; }

        public int Mabrand { get; set; }

        public string Tenbrand { get; set; }
    }
}
