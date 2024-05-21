using System.ComponentModel.DataAnnotations;

namespace webbanhang.ViewModels
{
    public class AddBrand
    {
        [Display(Name = "Tên thương hiệu")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? BrandName { get; set; }
    }
}
