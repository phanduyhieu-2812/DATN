using System.Drawing;
using System.Text;

namespace webbanhang.Helpers
{
    public class Util
    {
        public static string UploadHinh(IFormFile Avatar, string folder)
        {
            try
            {
                var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Avatar.FileName);
                using (var myfile = new FileStream(fullpath, FileMode.CreateNew))
                {
                    Avatar.CopyTo(myfile);
                }
                return Avatar.FileName;
            } catch(Exception ex) 
            {
                return string.Empty;
            }
        }
        public static string UploadHinh1(IFormFile Image, string folder)
        {
            try
            {
                var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Image.FileName);
                using (var myfile = new FileStream(fullpath, FileMode.CreateNew))
                {
                    Image.CopyTo(myfile);
                }
                return Image.FileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string GenerateRandonKey(int lenght = 6)
        {
            var parttern = @"qwertyuiopasdfghjklzxcvbnm0123456789";
            var sb = new StringBuilder();
            var rb = new Random();

            for (int i = 0; i < lenght; i++)
            {
                sb.Append(parttern[rb.Next(0, parttern.Length)]);
            }
            return sb.ToString();
        }
    }
}
