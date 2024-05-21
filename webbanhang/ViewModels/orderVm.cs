namespace webbanhang.ViewModels
{
    public class orderVm
    {
        public int? Mahh { get; set; }
        public int DonGia { get; set; }

        public string Hinh { get; set; }
        public string TenHh { get; set; }
        public string? Size { get; set; }

        public int SoLuong { get; set; }

        public int ThanhTien => DonGia * SoLuong;



    }
}
