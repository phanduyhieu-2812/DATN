using System.ComponentModel.DataAnnotations;

namespace webbanhang.ViewModels
{
    public class SendcodeVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(20, ErrorMessage = "Tên đăng nhập không được quá 20 kí tự")]
        public string AccountId { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }
    }
}
