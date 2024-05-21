using System.ComponentModel.DataAnnotations;

namespace webbanhang.ViewModels
{
    public class RegisterVM
    {
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(20,ErrorMessage ="Tên đăng nhập không được quá 20 kí tự")]
        public string AccountId { get; set; } 



        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,40}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự, 1 chữ cái in hoa, 1 số và 1 kí tự đặc biệt")]
        public string? Password { get; set; }


        [Display(Name = "Xác nhận mật khẩu")] 
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,40}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự , 1 chữ cái in hoa, 1 số và 1 kí tự đặc biệt")]

        public string? ConfirmPassword { get; set; }


        public DateTime? CreateAt { get; set; }

        public string? Status { get; set; }


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


        public int? Role { get; set; }


        public string? RandomKey { get; set; }



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
