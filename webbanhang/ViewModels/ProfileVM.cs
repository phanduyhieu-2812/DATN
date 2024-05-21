using System.ComponentModel.DataAnnotations;

namespace webbanhang.ViewModels
{
    public class ProfileVM
    {

        public DateTime? CreateAt { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }
        public string? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        public string? Name { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        public string? Phone { get; set; }

        public string? Avatar { get; set; }


        [Display(Name = "Tỉnh/Thành Phố")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        public string? Province { get; set; }


        [Display(Name = "Quận/Huyện/Thị xã/Thành Phố thuộc tỉnh")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        public string? Ward { get; set; }


        [Display(Name = "Xã/Phường/Thị trấn")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        public string? Districs { get; set; }



        [Display(Name = "Địa chỉ chính xác")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        public string? Address { get; set; }
    }
}
