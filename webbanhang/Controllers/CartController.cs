using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webbanhang.Data;
using webbanhang.ViewModels;
using webbanhang.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.EntityFrameworkCore;
using webbanhang.Models;
using webbanhang.Services;
namespace webbanhang.Controllers
{

    public class CartController : Controller
    {
        private readonly WebDatnContext db;
        private readonly IVnPayService _vnPayService;

        public CartController(WebDatnContext context, IVnPayService vnPayService)
        {
            db = context;
            _vnPayService = vnPayService;
        }



        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CARTKEY) ?? new List<CartItem>();

        
        public IActionResult Index()
        {
            return View(Cart);
        }


        //public IActionResult Disscount(string? Disscount,int Product_ID) 
        //{
        //    var giohang = Cart;
        //    if (Disscount == null)
        //    {
        //        TempData["Message"] = "Mời nhập mã giảm giá";
        //        return RedirectToAction("Index");
        //    }
        //    if (Disscount != null)
        //    {
        //        var disscount = db.Disscounts.SingleOrDefault(p => p.DisscountCode == Disscount);
        //        if (disscount == null)
        //        {
        //            TempData["Message"] = "Mã giảm giá không chính xác";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            if (disscount.DisscountStart <= DateTime.Now && DateTime.Now <= disscount.DisscountEnd)
        //            {
        //                if (disscount.Quantity < 0) 
        //                {
        //                    TempData["Message"] = "Mã giảm giá đã hết";
        //                    return RedirectToAction("Index");
        //                }
        //                else 
        //                {
        //                    var item = giohang.SingleOrDefault(p => p.Mahh == Product_ID);
        //                    item = new CartItem { DonGia = item.DonGia - disscount.DisscountPrice };
        //                    HttpContext.Session.Set(MySetting.CARTKEY, giohang);
        //                    return RedirectToAction("Index");
        //                }
        //            }
        //        }
        //    }
        //    return View();
        //}
        public IActionResult AddToCart(int id, string size, int quantity = 1)
        {
            var giohang = Cart;
            if (size == null) { size = "S"; }
            var item = giohang.SingleOrDefault(p => p.Mahh == id && p.Size == size);            
            if (item == null)
            {
                var hanghoa = db.Products.SingleOrDefault(p => p.ProductId == id);
                if (hanghoa == null)
                {
                    TempData["Message"] = "Không tìm thấy thông tin sản phẩm";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    Mahh = hanghoa.ProductId,
                    TenHh = hanghoa.ProductName,
                    DonGia = (int)hanghoa.Price,
                    Hinh = hanghoa.Image ?? string.Empty,
                    SoLuong = quantity,
                    Size = size
                };
                giohang.Add(item);

            }
            else { item.SoLuong += quantity; }
            HttpContext.Session.Set(MySetting.CARTKEY, giohang);
            return RedirectToAction("Index");
        }

        public IActionResult ReCart(int id, string size, int quantity = 1)
        {
            var giohang = Cart;
            if (size == null) { size = "S"; }
            var item = giohang.SingleOrDefault(p => p.Mahh == id && p.Size == size);
            if (item == null)
            {
                var hanghoa = db.Products.SingleOrDefault(p => p.ProductId == id);
                if (hanghoa == null)
                {
                    TempData["Message"] = "Không tìm thấy thông tin sản phẩm";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    Mahh = hanghoa.ProductId,
                    TenHh = hanghoa.ProductName,
                    DonGia = (int)hanghoa.Price,
                    Hinh = hanghoa.Image ?? string.Empty,
                    SoLuong = quantity,
                    Size = size
                };
                giohang.Add(item);

            }
            else {
                item.SoLuong -= quantity;
                if (item.SoLuong <=1 ) { item.SoLuong = 1; }
                
            }
            HttpContext.Session.Set(MySetting.CARTKEY, giohang);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id, string size)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.Mahh == id && p.Size == size);
            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set(MySetting.CARTKEY, giohang);
            }
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model,  string payment)
        {
            if (ModelState.IsValid)
            {
                if (payment == "Thanh toán bằng VNPay")
                {

                    var vnPayModel = new VnPaymentRequestModel
                    {
                        Amount = Cart.Sum(p => p.ThanhTien),
                        CreateDate = DateTime.Now,
                        Decription = "Thanh toán đơn hàng",
                        Fullname = model.HoTen,
                        OrderId = new Random().Next(1000,100000)
                    };
                    return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
                }
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var name = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                var tinh = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Tinh").Value;
                var huyen = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Huyen").Value;
                var xa = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Xa").Value;
                var address = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "address").Value;
                var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name").Value;
                var phone = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone).Value;

                var hoadon = new Order
                {
                    AccountId = userName,
                    Total = Cart.Sum(p => p.ThanhTien),
                    Status = "Chờ xác nhận",
                    OrderNote = model.GhiChu,
                    OrderDate = DateTime.Now,
                    CreateAt = DateTime.Now,
                    CreateBy = userName,
                    Phonenumber = model.SoDienThoai,
                    Province    = model.Tinh,
                    Ward = model.Huyen,
                    District = model.Xa,
                    Address = model.DiaChi,
                    Delivery = "",
                    Payment = "COD",
                    Name = model.HoTen,  
                    
                };
                db.Database.BeginTransaction();
                try
                {
                    db.Database.CommitTransaction();
                    db.Add(hoadon);
                    db.SaveChanges();

                    var cthd = new List<OderDetail>();
                    foreach (var item in Cart)
                    {
                        cthd.Add(new OderDetail 
                        {
                            ProductId = item.Mahh,
                            OrderId = hoadon.OrderId,
                            Price = item.DonGia,
                            Quantity = item.SoLuong,
                            CreateAt = DateTime.Now,
                            CreateBy = userName,
                            Size= item.Size,
                            AccountId = userName,
                        });
                    }
                    db.AddRange(cthd);
                    db.SaveChanges();
                    
                    var hd = db.Orders.OrderByDescending(order => order.OrderId).FirstOrDefault();
                    var str_sanpham = "";
                    foreach (var item in Cart) 
                    {
                        str_sanpham += "<tr>";
                        str_sanpham += "<td>" + item.TenHh + "</td>";
                        str_sanpham += "<td>" + item.Size + "</td>";
                        str_sanpham += "<td>" + item.SoLuong + "</td>";
                        str_sanpham += "<td>" + string.Format("{0:N0} VNĐ", item.ThanhTien) + "</td>";
                        str_sanpham += "</tr>";
                    }
                    var thanhtien = string.Format("{0:N0} VNĐ", Cart.Sum(p => p.ThanhTien));
                    string htmlMessage = $@"
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600""
	                                            style=""background-color:#ffffff;border:1px solid #dedede;border-radius:3px"">
	                                            <tbody>
		                                            <tr>
			                                            <td align=""center"" valign=""top"">

				                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""
					                                            style=""background-color:#96588a;color:#ffffff;border-bottom:0;font-weight:bold;line-height:100%;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;border-radius:3px 3px 0 0"">
					                                            <tbody>
						                                            <tr>
							                                            <td style=""padding:36px 48px;display:block"">
								                                            <h1
									                                            style=""font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:30px;font-weight:300;line-height:150%;margin:0;text-align:left;color:#ffffff;background-color:inherit"">
									                                           Cảm ơn bạn đã ủng hộ Katee-Shop</h1>
							                                            </td>
						                                            </tr>
					                                            </tbody>
				                                            </table>

			                                            </td>
		                                            </tr>
		                                            <tr>
			                                            <td align=""center"" valign=""top"">

				                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
					                                            <tbody>
						                                            <tr>
							                                            <td valign=""top"" style=""background-color:#ffffff"">

								                                            <table border=""0"" cellpadding=""20"" cellspacing=""0"" width=""100%"">
									                                            <tbody>
										                                            <tr>
											                                            <td valign=""top"" style=""padding:48px 48px 32px"">
												                                            <div
													                                            style=""color:#636363;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:14px;line-height:150%;text-align:left"">

													                                            <p style=""margin:0 0 16px"">Xin chào {name}</p>
													                                            <p style=""margin:0 0 16px"">Cảm ơn đã đặt hàng.Đơn hàng của bạn đang được xử lí. Trong
														                                            thời gian chờ đợi, đây là lời nhắc về những gì bạn đã đặt hàng:
													                                            </p>


													                                            <h2
														                                            style=""color:#96588a;display:block;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:0 0 18px;text-align:left"">
														                                            [Đơn hàng #{hd.OrderId}] {DateTime.Now}</h2>

													                                            <div style=""margin-bottom:40px"">
														                                            <table cellspacing=""0"" cellpadding=""6"" border=""1""
															                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;width:100%;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif"">
															                                            <thead>
																                                            <tr>
																	                                            <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Sản phẩm</th>
                                                                                                                <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Size</th>
																	                                            <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Số lượng</th>
																	                                            <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Thành tiền</th>
																                                            </tr>
															                                            </thead>
															                                            <tbody>
																                                           {str_sanpham}
															                                            </tbody>
															                                            <tfoot>
																                                            <tr>
																	                                            <th scope=""row"" colspan=""2""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;border-top-width:4px"">
																		                                            Nguyên giá:</th>
																	                                            <td
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;border-top-width:4px"">
																		                                            <span>{thanhtien}</span>
																	                                            </td>
																                                            </tr>
																                                            <tr>
																	                                            <th scope=""row"" colspan=""2""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Phương thức thanh toán:</th>
																	                                            <td
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Thanh toán qua VNPay</td>
																                                            </tr>
																                                            <tr>
																	                                            <th scope=""row"" colspan=""2""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Tổng cộng:</th>
																	                                            <td
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            <span>{thanhtien}</span>
																	                                            </td>
																                                            </tr>
															                                            </tfoot>
														                                            </table>
													                                            </div>


													                                            <table cellspacing=""0"" cellpadding=""0"" border=""0""
														                                            style=""width:100%;vertical-align:top;margin-bottom:40px;padding:0"">
														                                            <tbody>
															                                            <tr>
																                                            <td valign=""top"" width=""50%""
																	                                            style=""text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;border:0;padding:0"">
																	                                            <h2
																		                                            style=""color:#96588a;display:block;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:0 0 18px;text-align:left"">
																		                                            Thông tin người nhận</h2>

																	                                            <address
																		                                            style=""padding:12px;color:#636363;border:1px solid #e5e5e5"">
																		                                            {model.HoTen}<br>{model.DiaChi}-{model.Xa}-{model.Huyen}<br>{model.Tinh}<br><a href=""tel:{model.SoDienThoai}""
																			                                            style=""color:#96588a;font-weight:normal;text-decoration:underline""
																			                                            target=""_blank"">{model.SoDienThoai}</a> <br><a
																			                                            href=""mailto:{email}""
																			                                            target=""_blank"">{email}</a>
																	                                            </address>
																                                            </td>
															                                            </tr>
														                                            </tbody>
													                                            </table>
													                                            <p style=""margin:0 0 16px"">Chúng tôi đang tiến hành hoàn thiện đơn
														                                            đặt hàng của bạn</p>
												                                            </div>
											                                            </td>
										                                            </tr>
									                                            </tbody>
								                                            </table>

							                                            </td>
						                                            </tr>
					                                            </tbody>
				                                            </table>

			                                            </td>
		                                            </tr>
	                                            </tbody>
                                            </table>";
                    var message = new MimeMessage();

                    // Đặt địa chỉ email người gửi
                    message.From.Add(new MailboxAddress("Katee", "phanduyhieu28122002@gmail.com"));

                    // Đặt địa chỉ email người nhận
                    message.To.Add(new MailboxAddress("Recipient Name", email));

                    // Đặt chủ đề của email
                    message.Subject = "Đặt hàng thành công";

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = htmlMessage;
                    message.Body = bodyBuilder.ToMessageBody();
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
                    HttpContext.Session.Set<List<CartItem>>(MySetting.CARTKEY, new List<CartItem>());

                    return View("CheckoutSuccess");

                }
                catch 
                {
                    db.Database.RollbackTransaction();
                }
            }
            return View(Cart);
        }
        [Authorize]
        public IActionResult OderHistory()
        {
           
            var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name").Value;
            var dt = db.OderDetails.Include(p => p.Product).Where(p => p.AccountId == userName);
            var result = dt.Select(p => new orderVm
            {
                Mahh = p.ProductId,
                TenHh= p.Product.ProductName,
                DonGia = (int)p.Price,
                Hinh = p.Product.Image,
                Size = p.Size,
                SoLuong = (int)p.Quantity
            });
            if (result == null)
            {
                TempData["Message"] = "Bạn chưa đặt đơn hàng nào";
            }
            return View(result);
        }


        [Authorize]
        public IActionResult PaymentFail() 
        {
            return View();
        }

        [Authorize]       
        public IActionResult PaymentBack(CheckoutVM model)
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if(response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = "Lỗi thánh toán VNPay";
                return RedirectToAction("PaymentFail");
            }
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var name = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var tinh = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Tinh").Value;
            var huyen = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Huyen").Value;
            var xa = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Xa").Value;
            var address = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "address").Value;
            var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "User_name").Value;
            var phone = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone).Value;

            var hoadon = new Order
            {
                AccountId = userName,
                Total = Cart.Sum(p => p.ThanhTien),
                Status = "Đã thanh toán",
                
                OrderDate = DateTime.Now,
                CreateAt = DateTime.Now,
                CreateBy = userName,
                Phonenumber = phone,
                Province = tinh,
                Ward = huyen,
                District = xa,
                Address = address,
                Delivery = "",
                Payment = "VNPay",
                Name = name,
            };
            db.Database.BeginTransaction();
            try
            {
                db.Database.CommitTransaction();
                db.Add(hoadon);
                db.SaveChanges();

                var cthd = new List<OderDetail>();
                foreach (var item in Cart)
                {
                    cthd.Add(new OderDetail
                    {
                        ProductId = item.Mahh,
                        OrderId = hoadon.OrderId,
                        Price = item.DonGia,
                        Quantity = item.SoLuong,
                        CreateAt = DateTime.Now,
                        CreateBy = userName,
                        Size = item.Size,
                        AccountId = userName,
                    });
                }
                db.AddRange(cthd);
                db.SaveChanges();

                var hd = db.Orders.OrderByDescending(order => order.OrderId).FirstOrDefault();
                var str_sanpham = "";
                foreach (var item in Cart)
                {
                    str_sanpham += "<tr>";
                    str_sanpham += "<td>" + item.TenHh + "</td>";
                    str_sanpham += "<td>" + item.Size + "</td>";
                    str_sanpham += "<td>" + item.SoLuong + "</td>";
                    str_sanpham += "<td>" + string.Format("{0:N0} VNĐ", item.ThanhTien) + "</td>";
                    str_sanpham += "</tr>";
                }
                var thanhtien = string.Format("{0:N0} VNĐ", Cart.Sum(p => p.ThanhTien));
                string htmlMessage = $@"
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600""
	                                            style=""background-color:#ffffff;border:1px solid #dedede;border-radius:3px"">
	                                            <tbody>
		                                            <tr>
			                                            <td align=""center"" valign=""top"">

				                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%""
					                                            style=""background-color:#96588a;color:#ffffff;border-bottom:0;font-weight:bold;line-height:100%;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;border-radius:3px 3px 0 0"">
					                                            <tbody>
						                                            <tr>
							                                            <td style=""padding:36px 48px;display:block"">
								                                            <h1
									                                            style=""font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:30px;font-weight:300;line-height:150%;margin:0;text-align:left;color:#ffffff;background-color:inherit"">
									                                           Cảm ơn bạn đã ủng hộ Katee-Shop</h1>
							                                            </td>
						                                            </tr>
					                                            </tbody>
				                                            </table>

			                                            </td>
		                                            </tr>
		                                            <tr>
			                                            <td align=""center"" valign=""top"">

				                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
					                                            <tbody>
						                                            <tr>
							                                            <td valign=""top"" style=""background-color:#ffffff"">

								                                            <table border=""0"" cellpadding=""20"" cellspacing=""0"" width=""100%"">
									                                            <tbody>
										                                            <tr>
											                                            <td valign=""top"" style=""padding:48px 48px 32px"">
												                                            <div
													                                            style=""color:#636363;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:14px;line-height:150%;text-align:left"">

													                                            <p style=""margin:0 0 16px"">Xin chào {name}</p>
													                                            <p style=""margin:0 0 16px"">Cảm ơn đã đặt hàng.Đơn hàng của bạn đang được xử lí. Trong
														                                            thời gian chờ đợi, đây là lời nhắc về những gì bạn đã đặt hàng:
													                                            </p>


													                                            <h2
														                                            style=""color:#96588a;display:block;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:0 0 18px;text-align:left"">
														                                            [Đơn hàng #{hd.OrderId}] {DateTime.Now}</h2>

													                                            <div style=""margin-bottom:40px"">
														                                            <table cellspacing=""0"" cellpadding=""6"" border=""1""
															                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;width:100%;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif"">
															                                            <thead>
																                                            <tr>
																	                                            <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Sản phẩm</th>
                                                                                                                <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Size</th>
																	                                            <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Số lượng</th>
																	                                            <th scope=""col""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Thành tiền</th>
																                                            </tr>
															                                            </thead>
															                                            <tbody>
																                                           {str_sanpham}
															                                            </tbody>
															                                            <tfoot>
																                                            <tr>
																	                                            <th scope=""row"" colspan=""2""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;border-top-width:4px"">
																		                                            Nguyên giá:</th>
																	                                            <td
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;border-top-width:4px"">
																		                                            <span>{thanhtien}</span>
																	                                            </td>
																                                            </tr>
																                                            <tr>
																	                                            <th scope=""row"" colspan=""2""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Phương thức thanh toán:</th>
																	                                            <td
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Thanh toán khi nhận hàng</td>
																                                            </tr>
																                                            <tr>
																	                                            <th scope=""row"" colspan=""2""
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            Tổng cộng:</th>
																	                                            <td
																		                                            style=""color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left"">
																		                                            <span>{thanhtien}</span>
																	                                            </td>
																                                            </tr>
															                                            </tfoot>
														                                            </table>
													                                            </div>


													                                            <table cellspacing=""0"" cellpadding=""0"" border=""0""
														                                            style=""width:100%;vertical-align:top;margin-bottom:40px;padding:0"">
														                                            <tbody>
															                                            <tr>
																                                            <td valign=""top"" width=""50%""
																	                                            style=""text-align:left;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;border:0;padding:0"">
																	                                            <h2
																		                                            style=""color:#96588a;display:block;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;font-size:18px;font-weight:bold;line-height:130%;margin:0 0 18px;text-align:left"">
																		                                            Thông tin người nhận</h2>

																	                                            <address
																		                                            style=""padding:12px;color:#636363;border:1px solid #e5e5e5"">
																		                                            {model.HoTen}<br>{model.DiaChi}-{model.Xa}-{model.Huyen}<br>{model.Tinh}<br><a href=""tel:{model.SoDienThoai}""
																			                                            style=""color:#96588a;font-weight:normal;text-decoration:underline""
																			                                            target=""_blank"">{model.SoDienThoai}</a> <br><a
																			                                            href=""mailto:{email}""
																			                                            target=""_blank"">{email}</a>
																	                                            </address>
																                                            </td>
															                                            </tr>
														                                            </tbody>
													                                            </table>
													                                            <p style=""margin:0 0 16px"">Chúng tôi đang tiến hành hoàn thiện đơn
														                                            đặt hàng của bạn</p>
												                                            </div>
											                                            </td>
										                                            </tr>
									                                            </tbody>
								                                            </table>

							                                            </td>
						                                            </tr>
					                                            </tbody>
				                                            </table>

			                                            </td>
		                                            </tr>
	                                            </tbody>
                                            </table>";
                var message = new MimeMessage();

                // Đặt địa chỉ email người gửi
                message.From.Add(new MailboxAddress("Katee", "phanduyhieu28122002@gmail.com"));

                // Đặt địa chỉ email người nhận
                message.To.Add(new MailboxAddress("Recipient Name", email));

                // Đặt chủ đề của email
                message.Subject = "Đặt hàng thành công";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = htmlMessage;
                message.Body = bodyBuilder.ToMessageBody();
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
                HttpContext.Session.Set<List<CartItem>>(MySetting.CARTKEY, new List<CartItem>());

                

            }
            catch
            {
                db.Database.RollbackTransaction();
            }           
            return View("CheckoutSuccess");
        }

    }
}
