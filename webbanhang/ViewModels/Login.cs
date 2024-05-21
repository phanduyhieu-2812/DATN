﻿using System.ComponentModel.DataAnnotations;

namespace webbanhang.ViewModels
{
    public class Login
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(20, ErrorMessage = "Tên đăng nhập không được quá 20 kí tự")]
        public string Username { get; set; }


        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không được quá 50 kí tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,50}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự, 1 chữ cái in hoa, 1 số và 1 kí tự đặc biệt")]
        public string Password { get; set; }
    }
}
