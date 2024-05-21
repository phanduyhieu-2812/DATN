using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webbanhang.Data;
using webbanhang.ViewModels;
using webbanhang.Helpers;
using AutoMapper;
using NuGet.Protocol.Plugins;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace webbanhang.Controllers
{
    public class RegisterController : Controller
    {
        private readonly WebDatnContext db;
        private readonly IMapper _mapper;

        public RegisterController(WebDatnContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;

        }



        #region Đăng ký
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Avatar)
        {

            if (ModelState.IsValid)
            {


                if (db.Accounts.Any(x => x.AccountId == model.AccountId))
                {

                    TempData["Message"] = "Tên người dùng đã tồn tại";
                    return View(model);
                }

                var khachhang = _mapper.Map<Account>(model);
                //khachhang.RandomKey = Util.GenerateRandonKey();
                khachhang.Password = model.Password;
                khachhang.Role = "customer";
                khachhang.CreateAt = DateTime.Now;

                if (Avatar != null)
                {
                    khachhang.Avatar = Util.UploadHinh(Avatar, "Account");
                }

                db.Add(khachhang);
                db.SaveChanges();
                return RedirectToAction("Success", "Register");
            }
            if (!ModelState.IsValid)
            {
                var errorMessages = new List<string>();

                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Any())
                    {
                        var fieldName = entry.Key;
                        var errorMessagesForField = entry.Value.Errors.Select(error => error.ErrorMessage);
                        var concatenatedErrorMessages = string.Join("; ", errorMessagesForField);
                        errorMessages.Add($"Thông tin tại {fieldName} đang không chính xác: {concatenatedErrorMessages}");
                    }
                }
                TempData["Message"] = string.Join(" | ", errorMessages);
                return View(model);
            }
            return View();
        }
        #endregion 

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult UpdateSuccess()
        {
            return View();
        }

        #region Đăng nhập
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(string? ReturnUrl, Login model)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachhang = db.Accounts.Include(p => p.AccountAddresses)
                    .SingleOrDefault(kh => kh.AccountId == model.Username);
                if (khachhang == null)
                {
                    TempData["Message"] = "Sai tên đăng nhập";
                    return View(model);
                }
                else
                {
                    if (khachhang.Password != model.Password)
                    {
                        TempData["Message"] = "Sai mật khẩu";
                        return View(model);
                    }
                    else
                    {
                        if (khachhang.Status == "0")
                        {
                            TempData["Message"] = "Tài khoản đã bị vô hiệu hóa";
                            return View(model);
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Email, khachhang.Email),
                            new Claim(ClaimTypes.Name, khachhang.Name),
                            new Claim(ClaimTypes.MobilePhone, khachhang.Phone),
                            new Claim("Tinh",khachhang.Province),
                            new Claim("Huyen",khachhang.Ward),
                            new Claim("Xa",khachhang.Districs),
                            new Claim("User_name",khachhang.AccountId),
                            new Claim("address",khachhang.Address),
                            new Claim(ClaimTypes.Role,khachhang.Role),

                            };
                            if (khachhang.CreateAt != null)
                            {
                                var createAtClaim = new Claim("NgayTao", khachhang.CreateAt.Value.ToString("yyyy-MM-dd"));
                                claims.Add(createAtClaim);
                            }
                            if (khachhang.Avatar != null)
                            {
                                var Avatar = new Claim("Hinh", khachhang.Avatar);
                                claims.Add(Avatar);
                            }

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else { return RedirectToAction("Profile", "Register"); }
                        }
                    }    

                        
                }
            }
            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("DangNhap", "Register");
        }


        [HttpGet]
        public IActionResult CapNhatProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CapNhatProfile(ProfileVM model, IFormFile Avatar, List<Claim> claims)
        {
            if (ModelState.IsValid)
            {
                string avatarFileName = Avatar.FileName;
                var userNameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name");
                string userNameValue = userNameClaim.Value;
                var khachhang = db.Accounts.FirstOrDefault(p => p.AccountId == userNameValue);
                khachhang.Email = model.Email;
                khachhang.UpdateAt = DateTime.Now;
                khachhang.UpdateBy = userNameValue;
                khachhang.Name = model.Name;
                khachhang.Phone = model.Phone;
                khachhang.Province = model.Province;
                khachhang.Ward = model.Ward;
                khachhang.Districs = model.Districs;
                khachhang.Address = model.Address;
                khachhang.Avatar = avatarFileName;


                if (Avatar != null)
                {
                    khachhang.Avatar = Util.UploadHinh(Avatar, "Account");
                }

                db.Update(khachhang);
                db.SaveChanges();
                return RedirectToAction("UpdateSuccess", "Register");
            }
            return View();
        }


        [HttpGet]
        public IActionResult SendCode()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendCode(SendcodeVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                var khachhang = db.Accounts.FirstOrDefault(p => p.AccountId == model.AccountId);
                if (khachhang == null)
                {
                    TempData["Message"] = "Tên đăng nhập không chính xác";
                    return View(model);
                }
                if (khachhang.Email != model.Email)
                {
                    TempData["Message"] = "Địa chỉ email không chính xác";
                    return View(model);
                }
                if (khachhang.Email == model.Email)
                {
                    // Khởi tạo một đối tượng MimeMessage để đại diện cho một email
                    var message = new MimeMessage();

                    // Đặt địa chỉ email người gửi
                    message.From.Add(new MailboxAddress("Katee", "phanduyhieu28122002@gmail.com"));

                    // Đặt địa chỉ email người nhận
                    message.To.Add(new MailboxAddress("Recipient Name", model.Email));

                    // Đặt chủ đề của email
                    message.Subject = "Lấy lại mật khẩu";
                    //var random = new Random();
                    //var verificationCode = random.Next(100000, 999999).ToString();
                    // Tạo nội dung của email dưới dạng văn bản thuần túy và HTML
                    var builder = new BodyBuilder();
                    builder.TextBody = $"Mật khẩu đăng nhập Katee-Shop của bạn là:  {khachhang.Password}";
                    builder.HtmlBody = $"<strong>Mật khẩu đăng nhập Katee-Shop của bạn là: {khachhang.Password}</strong>";

                    // Gán nội dung đã tạo cho email
                    message.Body = builder.ToMessageBody();

                    // Khởi tạo một đối tượng SmtpClient để gửi email
                    using (var smtpClient = new SmtpClient())
                    {
                        // Kết nối đến máy chủ SMTP
                        smtpClient.Connect("smtp.gmail.com", 587, false);

                        // Đăng nhập vào máy chủ SMTP nếu cần
                        smtpClient.Authenticate("phanduyhieu28122002@gmail.com", "aaachrbxewqvbwlh");

                        // Gửi email
                        smtpClient.Send(message);


                        // Ngắt kết nối với máy chủ SMTP
                        smtpClient.Disconnect(true);

                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var khachhang = db.Accounts.FirstOrDefault(p => p.AccountId == model.AccountId);
                if (khachhang == null)
                {
                    TempData["Message"] = "Tên đăng nhập không chính xác";
                    return View(model);
                }
                else
                {
                    if (khachhang.Password != model.OldPassword)
                    {
                        TempData["Message"] = "Mật khẩu không chính xác";
                        return View(model);
                    }
                    else
                    {
                        khachhang.Password = model.NewPassword;
                        db.Update(khachhang);
                        db.SaveChanges();
                        return RedirectToAction("UpdateSuccess", "Register");
                    }
                }
            }
            return View();
        }

     




    }
}
