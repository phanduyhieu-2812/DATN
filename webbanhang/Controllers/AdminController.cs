using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using webbanhang.Data;
using webbanhang.ViewModels;
using Microsoft.EntityFrameworkCore;
using webbanhang.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using MimeKit;
using System.Net.Mail;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace webbanhang.Controllers
{
    public class AdminController : Controller
    {
        private readonly WebDatnContext db;

        public AdminController(WebDatnContext context)
        {
            db = context;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]

        public IActionResult Product(int? page)
        {
            int pagenumber;
            if (page != null)
            {
                pagenumber = page.Value - 1;
            }
            else
            {
                pagenumber = 0;
            }

            var result = db.Products.Include(p => p.Genre).Include(p => p.Brand).Skip(pagenumber * 9).Take(9).Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                BrandName = p.Brand.BrandName,
                Byturn = p.Byturn,
                GenreName = p.Genre.GenreName,
                ProductName = p.ProductName,
                Price = p.Price,
                Views = p.Views,
                Quantity = p.Quantity,
                Specifications = p.Specifications,
                Image = p.Image,
                Description = p.Description,
                CreateAt = p.CreateAt ?? DateTime.Now,
                CreateBy = p.CreateBy ?? "",
                UpdateAt = p.UpdateAt ?? DateTime.Now,
                UpdateBy = p.UpdateBy ?? ""
            });
            return View(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddProduct()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddProduct(AddproductVM model, int Genre_ID, int Brand_ID, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                var userNameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name");
                string userNameValue = userNameClaim.Value;
                var add = new Product
                {
                    BrandId = Brand_ID,
                    GenreId = Genre_ID,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Specifications = model.Specifications,
                    Image = Util.UploadHinh(Image, "product1"),
                    Description = model.Description,
                    CreateAt = DateTime.Now,
                    CreateBy = userNameValue,
                };
                db.Add(add);
                db.SaveChanges();
                TempData["Messagei"] = "Thêm sản phẩm mới thành công";

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
                return View();
            }
            
            return View();
        }

        

        [Authorize(Roles = "admin")]
        public IActionResult Search(string? query)
        {

            var HangHoas = db.Products.AsQueryable();

            if (query != null)
            {
                HangHoas = HangHoas.Where(p => p.ProductName.Contains(query) || p.Genre.GenreName.Contains(query) || p.Brand.BrandName.Contains(query));
            }

            var reusult = HangHoas.Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                BrandName = p.Brand.BrandName,
                Byturn = p.Byturn,
                GenreName = p.Genre.GenreName,
                ProductName = p.ProductName,
                Price = p.Price,
                Views = p.Views,
                Quantity = p.Quantity,
                Specifications = p.Specifications,
                Image = p.Image,
                Description = p.Description,
                CreateAt = p.CreateAt ?? DateTime.Now,
                CreateBy = p.CreateBy ?? "",
                UpdateAt = p.UpdateAt ?? DateTime.Now,
                UpdateBy = p.UpdateBy ?? ""
            });


            return View(reusult);

        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var data = db.Products.SingleOrDefault(p => p.ProductId == id);
            // Lấy tất cả các phần tử của ProductImages trừ phần tử đầu tiên
            var productImagesToRemove = db.ProductImages.Where(p => p.ProductId == id).ToList();
            foreach (var item in productImagesToRemove)
            {
                db.ProductImages.Remove(item);
                db.SaveChanges();
            }

            // Lấy tất cả các phần tử của OderDetails trừ phần tử đầu tiên
            var oderDetailsToRemove = db.OderDetails.Where(p => p.ProductId == id).ToList();
            foreach (var item in oderDetailsToRemove)
            {
                db.OderDetails.Remove(item);
                db.SaveChanges();
            }

            // Lấy tất cả các phần tử của Feedbacks trừ phần tử đầu tiên
            var feedbacksToRemove = db.Feedbacks.Where(p => p.ProductId == id).ToList();
            foreach (var item in feedbacksToRemove)
            {
                db.Feedbacks.Remove(item);
                db.SaveChanges();
            }

            // Lưu thay đổi vào cơ sở dữ liệu


            db.Products.Remove(data);
            db.SaveChanges();

            return RedirectToAction("Product");
        }
        [Authorize(Roles = "admin")]
        public IActionResult DeleteG(int id)
        {
            var data = db.Genres.SingleOrDefault(p => p.GenreId == id);
            // Lấy tất cả các phần tử của ProductImages trừ phần tử đầu tiên
            var productImagesToRemove = db.ProductImages.Where(p => p.GenreId == id).ToList();
            foreach (var item in productImagesToRemove)
            {
                db.ProductImages.Remove(item);
                db.SaveChanges();
            }

            // Lấy tất cả các phần tử của OderDetails trừ phần tử đầu tiên
            var oderDetailsToRemove = db.Feedbacks.Where(p => p.GenreId == id).ToList();
            foreach (var item in oderDetailsToRemove)
            {
                db.Feedbacks.Remove(item);
                db.SaveChanges();
            }

            // Lấy tất cả các phần tử của Feedbacks trừ phần tử đầu tiên
            var feedbacksToRemove = db.Products.Where(p => p.ProductId == id).ToList();
            foreach (var item in feedbacksToRemove)
            {
                db.Products.Remove(item);
                db.SaveChanges();
            }
            // Lưu thay đổi vào cơ sở dữ liệu

            db.Genres.Remove(data);
            db.SaveChanges();

            return RedirectToAction("Genres");
        }

        [Authorize(Roles = "admin")]
        public IActionResult DeleteB(int id)
        {
            var data = db.Brands.SingleOrDefault(p => p.BrandId == id);
            // Lấy tất cả các phần tử của ProductImages trừ phần tử đầu tiên
           

            // Lấy tất cả các phần tử của Feedbacks trừ phần tử đầu tiên
            var feedbacksToRemove = db.Products.Where(p => p.ProductId == id).ToList();
            foreach (var item in feedbacksToRemove)
            {
                db.Products.Remove(item);
                db.SaveChanges();
            }
            // Lưu thay đổi vào cơ sở dữ liệu

            db.Brands.Remove(data);
            db.SaveChanges();

            return RedirectToAction("Brand");
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            

            var result = db.Products.Where(p => p.ProductId == id).Select( p =>new UpdateVM
            {
                ProductName = p.ProductName ?? "",
                Price = p.Price ?? 0,
                Quantity = p.Quantity ?? 0,
                Specifications = p.Specifications,
                Description = p.Description,
                Image1=p.Image
            });
            var data = db.Brands.Select(lo => new UpdateVM
            {
                Mabrand = lo.BrandId,
                Tenbrand = lo.BrandName
            });
            var data1 = db.Genres.Select(lo => new UpdateVM
            {
                MaLoai = lo.GenreId,
                Tenloai = lo.GenreName
            });
            ViewBag.Brands = data;
            ViewBag.Genres = data1;
            ViewBag.Products = result;
            
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Update(UpdateVM model, int Genre_ID, int Brand_ID, IFormFile? Image, int? id)
        {
            
            if (ModelState.IsValid)
            {
                
                var userNameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name");
                string userNameValue = userNameClaim.Value;
                var update = db.Products.FirstOrDefault(p => p.ProductId == id);
                update.BrandId = Brand_ID;
                update.GenreId = Genre_ID;
                update.ProductName = model.ProductName;
                update.Description = model.Description;
                update.Price = model.Price;
                update.Specifications = model.Specifications;
                update.UpdateAt = DateTime.Now;
                update.UpdateBy = userNameValue;
                update.Quantity = model.Quantity;
                if (Image != null)
                {
                    update.Image = Util.UploadHinh(Image, "product1");
                }
               
                db.Update(update);
                db.SaveChanges();
                TempData["Messagei"] = "Cập nhật thông tin sản phẩm thành công";
                var result = db.Products.Where(p => p.ProductId == id).Select(p => new UpdateVM
                {
                    ProductName = p.ProductName ?? "",
                    Price = p.Price ?? 0,
                    Quantity = p.Quantity ?? 0,
                    Specifications = p.Specifications,
                    Description = p.Description,
                    Image1 = p.Image
                });
                var data = db.Brands.Select(lo => new UpdateVM
                {
                    Mabrand = lo.BrandId,
                    Tenbrand = lo.BrandName
                });
                var data1 = db.Genres.Select(lo => new UpdateVM
                {
                    MaLoai = lo.GenreId,
                    Tenloai = lo.GenreName
                });
                ViewBag.Brands = data;
                ViewBag.Genres = data1;
                ViewBag.Products = result;

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


        [Authorize(Roles = "admin")]
        public IActionResult Genres ()
        {
            var data = db.Genres.Include(p => p.Products).Select(lo => new GenresVM
            {
                GenreId = lo.GenreId,
                GenreName = lo.GenreName,
                CreateAt = lo.CreateAt,
                CreateBy = lo.CreateBy,
                Icon = lo.Icon,
                Soluong = lo.Products.Count,UpdateAt = lo.UpdateAt,UpdateBy = lo.UpdateBy,
                Quantity = lo.Products.Sum(p => p.Quantity),
                Byturn = lo.Products.Sum(p => p.Byturn),
                
            });

            return View(data);

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddGenre()
        {
          
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddGenre(AddGenreVM model, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                var userNameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name");
                string userNameValue = userNameClaim.Value;
                Random random = new Random();

                // Tạo ra một số ngẫu nhiên từ 0 đến 9999
                int randomNumber = random.Next(0, 10000);
                var result = new Genre
                {
                    GenreId = randomNumber,
                    GenreName = model.GenreName,
                    CreateAt = DateTime.Now,
                    CreateBy = userNameValue,
                    Icon = Util.UploadHinh(Image, "icon")
                };
                db.Add(result);
                db.SaveChanges();
                TempData["Messagei"] = "Thêm loại sản phẩm mới thành công";
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult UpdateGenre(int id)
        {
            var genre = db.Genres.FirstOrDefault(p => p.GenreId == id);
            var model = new AddGenreVM { GenreName = genre.GenreName, icon = genre.Icon };
            return View(model);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult UpdateGenre(AddGenreVM addGenreVM, int id, IFormFile? Image)
        {
            if(ModelState.IsValid) 
            {
                var userNameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name");
                string userNameValue = userNameClaim.Value;
                var genre = db.Genres.FirstOrDefault(p => p.GenreId == id);
                genre.GenreName = addGenreVM.GenreName;
                genre.UpdateAt = DateTime.Now;  
                genre.UpdateBy = userNameValue;
                if (Image != null)
                {
                    genre.Icon = Util.UploadHinh(Image, "icon");
                }    
                db.Update(genre);
                db.SaveChanges();
                TempData["Messagei"] = "Cập nhật  loại sản phẩm  thành công";
                var genrei = db.Genres.FirstOrDefault(p => p.GenreId == id);
                var model = new AddGenreVM { GenreName = genrei.GenreName };
                return View(model);
            }
            return View();
           
            
        }
        [Authorize(Roles = "admin")]
        public IActionResult Brand()
        {
            var data = db.Brands.Include(p => p.Products).Select(lo => new GenresVM
            {
                BrandId = lo.BrandId,
                BrandName = lo.BrandName,
                CreateAt = lo.CreateAt,
                CreateBy = lo.CreateBy,               
                Soluong = lo.Products.Count,
                UpdateAt = lo.UpdateAt,
                UpdateBy = lo.UpdateBy,
                Quantity = lo.Products.Sum(p => p.Quantity),
                Byturn = lo.Products.Sum(p => p.Byturn),
            });
            return View(data);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddBrand()
        {

            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddBrand(AddBrand model)
        {
            if (ModelState.IsValid)
            {
                var userNameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name");
                string userNameValue = userNameClaim.Value;
                Random random = new Random();

                // Tạo ra một số ngẫu nhiên từ 0 đến 9999
                int randomNumber = random.Next(0, 10000);
                var result = new Brand
                {
                    BrandId = randomNumber,
                    BrandName = model.BrandName,
                    CreateAt = DateTime.Now,
                    CreateBy = userNameValue,

                };
                db.Add(result);
                db.SaveChanges();
                TempData["Messagei"] = "Thêm nhãn hàng  mới thành công";
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult UpdateBrand(int id)
        {
            var genre = db.Brands.FirstOrDefault(p => p.BrandId == id);
            var model = new AddBrand { BrandName = genre.BrandName };
            return View(model);
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult UpdateBrand(AddBrand addGenreVM, int id)
        {
            if (ModelState.IsValid)
            {
                var userNameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name");
                string userNameValue = userNameClaim.Value;
                var genre = db.Brands.FirstOrDefault(p => p.BrandId == id);
                genre.BrandName = addGenreVM.BrandName;
                genre.UpdateAt = DateTime.Now;
                genre.UpdateBy = userNameValue;               
                db.Update(genre);
                db.SaveChanges();
                TempData["Messagei"] = "Cập nhật thông tin nhãn hàng thành công";
                var genrei = db.Brands.FirstOrDefault(p => p.BrandId == id);
                var model = new AddBrand { BrandName = genre.BrandName };
                return View(model);
            }
            return View();


        }
        [Authorize(Roles = "admin")]
        public IActionResult Checkout()
        {
            var data = db.Orders.Where(p => p.Status == "Chờ xác nhận" || p.Status == "Đã thanh toán").Select(p => new CheckoutAdminVM
            {
                OrderId = p.OrderId,
                AccountId = p.AccountId,
                Total = p.Total,
                Status = p.Status,  
                OrderDate = p.OrderDate,
                OrderNote = p.OrderNote,
                CreateAt = p.CreateAt,
                CreateBy = p.CreateBy,
                UpdateAt = p.UpdateAt,
                UpdateBy = p.UpdateBy,
                Phonenumber = p.Phonenumber,
                Province = p.Province,
                Ward = p.Ward,
                District = p.District,
                Address = p.Address,
                Payment = p.Payment,
                Name = p.Name
            });
            var data1 = db.Orders.Where(p => p.Status == "Chờ giao hàng").Select(p => new CheckoutAdminVM
            {
                OrderId = p.OrderId,
                AccountId = p.AccountId,
                Total = p.Total,
                Status = p.Status,
                OrderDate = p.OrderDate,
                OrderNote = p.OrderNote,
                CreateAt = p.CreateAt,
                CreateBy = p.CreateBy,
                UpdateAt = p.UpdateAt,
                UpdateBy = p.UpdateBy,
                Phonenumber = p.Phonenumber,
                Province = p.Province,
                Ward = p.Ward,
                District = p.District,
                Address = p.Address,
                Payment = p.Payment,
                Name = p.Name
            });
            var data2 = db.Orders.Where(p => p.Status == "Đang giao hàng").Select(p => new CheckoutAdminVM
            {
                OrderId = p.OrderId,
                AccountId = p.AccountId,
                Total = p.Total,
                Status = p.Status,
                OrderDate = p.OrderDate,
                OrderNote = p.OrderNote,
                CreateAt = p.CreateAt,
                CreateBy = p.CreateBy,
                UpdateAt = p.UpdateAt,
                UpdateBy = p.UpdateBy,
                Phonenumber = p.Phonenumber,
                Province = p.Province,
                Ward = p.Ward,
                District = p.District,
                Address = p.Address,
                Payment = p.Payment,
                Name = p.Name
            });
            var data3 = db.Orders.Where(p => p.Status == "Giao hàng thành công").Select(p => new CheckoutAdminVM
            {
                OrderId = p.OrderId,
                AccountId = p.AccountId,
                Total = p.Total,
                Status = p.Status,
                OrderDate = p.OrderDate,
                OrderNote = p.OrderNote,
                CreateAt = p.CreateAt,
                CreateBy = p.CreateBy,
                UpdateAt = p.UpdateAt,
                UpdateBy = p.UpdateBy,
                Phonenumber = p.Phonenumber,
                Province = p.Province,
                Ward = p.Ward,
                District = p.District,
                Address = p.Address,
                Payment = p.Payment,
                Name = p.Name
            });
            var data4 = db.Orders.Where(p => p.Status == "Khách hàng không nhận hàng").Select(p => new CheckoutAdminVM
            {
                OrderId = p.OrderId,
                AccountId = p.AccountId,
                Total = p.Total,
                Status = p.Status,
                OrderDate = p.OrderDate,
                OrderNote = p.OrderNote,
                CreateAt = p.CreateAt,
                CreateBy = p.CreateBy,
                UpdateAt = p.UpdateAt,
                UpdateBy = p.UpdateBy,
                Phonenumber = p.Phonenumber,
                Province = p.Province,
                Ward = p.Ward,
                District = p.District,
                Address = p.Address,
                Payment = p.Payment,
                Name = p.Name
            });
            ViewBag.Orders1 = data;
            ViewBag.Orders2 = data1;
            ViewBag.Orders3 = data2;
            ViewBag.Orders4 = data3;
            ViewBag.Orders5 = data4;
            return View(); 
        }
        [Authorize(Roles = "admin")]
        public IActionResult Confirm(int id)
        {
            var emai = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var checkout = db.Orders.SingleOrDefault(p => p.OrderId == id);
            if (checkout.Status == "Chờ xác nhận" || checkout.Status == "Đã thanh toán") 
            { 
                checkout.Status = "Chờ giao hàng"; 
                db.Update(checkout);
                db.SaveChanges();
            }
            else if (checkout.Status == "Chờ giao hàng" )
            {
                checkout.Status = "Đang giao hàng";
                db.Update(checkout);
                db.SaveChanges();
                var message = new MimeMessage();

                // Đặt địa chỉ email người gửi
                message.From.Add(new MailboxAddress("Katee", "phanduyhieu28122002@gmail.com"));

                // Đặt địa chỉ email người nhận
                message.To.Add(new MailboxAddress("Recipient Name", emai));

                // Đặt chủ đề của email
                message.Subject = "Xác nhận giao hàng";
                //var random = new Random();
                //var verificationCode = random.Next(100000, 999999).ToString();
                // Tạo nội dung của email dưới dạng văn bản thuần túy và HTML
                var builder = new BodyBuilder();
                builder.TextBody = $"Đơn hàng {checkout.OrderId} đang được giao đến địa chỉ {checkout.Province}, {checkout.Ward}, {checkout.District},{checkout.Address} bạn sẽ nhận được hàng trong 3 đến 5 ngày tới. Cảm ơn bạn đã ủng hộ Katee";
                

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
            else if (checkout.Status == "Đang giao hàng")
            {
                checkout.Status = "Giao hàng thành công";
                db.Update(checkout);
                db.SaveChanges();
            }
           
            return RedirectToAction("Checkout");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Bom(int id)
        {
            var checkout = db.Orders.SingleOrDefault(p => p.OrderId == id);
            checkout.Status = "Khách hàng không nhận hàng";
            db.Update(checkout);
            db.SaveChanges();
            return RedirectToAction("Checkout");
        }

        public IActionResult OrderDetail(int id)
        {
            var result = db.OderDetails.Include(p=>p.Product).Where(p => p.OrderId == id).Select(p => new ODetailVM
            {
                Price = p.Price,
                Quantity = p.Quantity,
                CreateAt = p.CreateAt,
                UpdateAt = p.UpdateAt,
                CreateBy = p.CreateBy,
                UpdateBy = p.UpdateBy,
                OrderId = id,
                Size = p.Size,
                AccountId = p.AccountId,
                Image = p.Product.Image,
                ProductName = p.Product.ProductName,
            });
            return View(result);
        }

        [Authorize(Roles = "admin")]

        public IActionResult Feedback() 
        {
            var feedbakc = db.Feedbacks.Where(p => p.Status == "Chưa xem").Include(p => p.Product).Select(p => new FVM
            {
                FeedbackId = p.FeedbackId,
                RateStar = p.RateStar,
                CreateAt = p.CreateAt,
                CreateBy = p.CreateBy,
                Status = p.Status,
                Content = p.Content,
                ProductName = p.Product.ProductName,
                Image = p.Product.Image

            });
            var feedback = db.Feedbacks.Where(p => p.Status == "Đã xem").Include(p => p.Product).Select(p => new FVM
            {
                FeedbackId = p.FeedbackId,
                RateStar = p.RateStar,
                CreateAt = p.CreateAt,
                CreateBy = p.CreateBy,
                Status = p.Status,
                Content = p.Content,
                ProductName = p.Product.ProductName,
                Image = p.Product.Image

            });
            ViewBag.Feedback1 = feedbakc;
            ViewBag.Feedback2 = feedback;


            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult ReplyFeedback(string id)
        {
            var feedback = db.Feedbacks.FirstOrDefault(p => p.FeedbackId == id);
            feedback.Status = "Đã xem";
            db.Update(feedback);
            db.SaveChanges();
            return RedirectToAction("Feedback");
        }



        [Authorize(Roles = "admin")]
        public IActionResult Customer(int? page)
        {
            int pagenumber;
            if (page != null)
            {
                pagenumber = page.Value - 1;
            }
            else
            {
                pagenumber = 0;
            }
            var customer = db.Accounts.Include(p => p.Orders).Skip(pagenumber * 5).Take(5).Select(p => new AccountVM
            {
                AccountId = p.AccountId,
                Name = p.Name,
                Password = p.Password,
                CreateAt = p.CreateAt,
                Status = p.Status,
                Email = p.Email,
                UpdateAt = p.UpdateAt,
                UpdateBy = p.UpdateBy,
                Phone = p.Phone,
                Avatar = p.Avatar,
                Province = p.Province,
                Ward = p.Ward,
                Districs = p.Districs,
                Address = p.Address,
                Role = p.Role,
                Quantity = p.Orders.Count(),
                Total = p.Orders.Sum(p => p.Total)
            });
            return View(customer);
        }

        public IActionResult CloseStatus(string id)
        { 
            var status = db.Accounts.FirstOrDefault(p => p.AccountId == id);
            status.Status = "0";
            db.Update(status);
            db.SaveChanges();
            return RedirectToAction("Customer"); 
        }

        public IActionResult OpenStatus(string id)
        {
            var status = db.Accounts.FirstOrDefault(p => p.AccountId == id);
            status.Status = "1";
            db.Update(status);
            db.SaveChanges();
            return RedirectToAction("Customer");
        }

        public IActionResult OpenRole(string id)
        {
            var status = db.Accounts.FirstOrDefault(p => p.AccountId == id);
            status.Role = "admin";
            db.Update(status);
            db.SaveChanges();
            return RedirectToAction("Customer");
        }

        public IActionResult CloseRole(string id)
        {
            var status = db.Accounts.FirstOrDefault(p => p.AccountId == id);
            status.Role = "customer";
            db.Update(status);
            db.SaveChanges();
            return RedirectToAction("Customer");
        }
    }
}
