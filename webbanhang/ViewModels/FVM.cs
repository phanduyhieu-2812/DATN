namespace webbanhang.ViewModels
{
    public class FVM
    {
        public string FeedbackId { get; set; } = null!;

        public int? GenreId { get; set; }

        public int? DisscountId { get; set; }

        public int RateStar { get; set; }

        public DateTime? CreateAt { get; set; }

        public string? CreateBy { get; set; }

        public string? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string? Status { get; set; }

        public string? Content { get; set; }

        public string? ProductName { get; set; }
        public string? Image { get; set; }


    }
}
