using System.ComponentModel.DataAnnotations;

namespace webbanhang.ViewModels
{
    public class AddGenreVM
    {
        
        [Display(Name = "Tên loại sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập đầy đủ thông tin")]
        public string? GenreName { get; set; }


        

        public string? icon { get; set; }

       

        
    }
}
