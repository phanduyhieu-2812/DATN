using Microsoft.Build.Framework;

namespace webbanhang.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }

        public string? BrandName { get; set; }

        public string? GenreName { get; set; }

        public string? ProductName { get; set; }

        public int? Price { get; set; }

        public long? Views { get; set; }

        public long? Byturn { get; set; }

        public double? Quantity { get; set; }

        public string? Status { get; set; }

        public string? Type { get; set; }

        public string? Specifications { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? CreateBy { get; set; }

        public string? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }
    }

    
}


