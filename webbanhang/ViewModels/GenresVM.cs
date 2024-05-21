namespace webbanhang.ViewModels
{
    public class GenresVM
    {
        public int GenreId { get; set; }

        public string? GenreName { get; set; }

        public int BrandId { get; set; }

        public string? BrandName { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? CreateBy { get; set; }

        public string? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string? Icon { get; set; }

        public int Soluong {  get; set; }

        public long? Byturn { get; set; }

        public double? Quantity { get; set; }


    }
}
