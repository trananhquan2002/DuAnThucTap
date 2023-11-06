using QuanLyDatDoAnAPI.Entities;
using QuanLyDatDoAnAPI.IServices;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;
using System.Net;
using BCryptNet = BCrypt.Net.BCrypt;
using QuanLyDatDoAnAPI.Libraries;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace QuanLyDatDoAnAPI.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration _configuration;

        public AccountServices()
        {
            
        }
        
        public AccountServices(IConfiguration configuration, AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task<Account> CreateAccount(string userName, string email, string password)
        {
            var exsitingAccount = await appDbContext.Account.FirstOrDefaultAsync(a => a.Email == email);
            if (exsitingAccount != null)
            {
                throw new InvalidOperationException("Email da duoc su dung cho tai khoan khac");
            }
            var exsitingAccounts = await appDbContext.Account.FirstOrDefaultAsync(x => x.UserName == userName);
            if (exsitingAccounts != null)
            {
                throw new InvalidOperationException("UserName da ton tai");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var verificationCode = GenerateVerificationCode();
            var account = new Account()
            {
                UserName = userName,
                Email = email,
                Password = hashedPassword,
                ResetPasswordToken = verificationCode
            };
            appDbContext.Account.Add(account);
            await appDbContext.SaveChangesAsync();
            string userEmailAddress = account.Email;
            SendVerificationEmail(userEmailAddress, verificationCode);
            return account;
        }

        private string GenerateVerificationCode()
        {
            string verificationCode = GenerateRandomCode();
            return verificationCode;
        }

        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int codeLength = 32;
            var random = new Random();
            var code = new string(Enumerable.Repeat(chars, codeLength).Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }

        public string SendVerificationEmail(string userEmailAddress, string verificationCode)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("trananhquan23702@gmail.com", "epabduexltgnjhfw"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("trananhquan23702@gmail.com");
                message.To.Add(new MailAddress("", userEmailAddress));
                message.Subject = "Xac thuc tai khoan";
                message.Body = "Ma xac thuc cua ban: " + verificationCode;
                message.IsBodyHtml = true;
                smtpClient.Send(message);
                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }

        public async Task<bool> VerifyAccount(int userId, string verificationCode)
        {
            var account = await appDbContext.Account.FindAsync(userId);
            if (account != null && account.ResetPasswordToken == verificationCode)
            {
                account.Status = 1;
                account.ResetPasswordToken = verificationCode;
                account.ResetPasswordTokenExpiry = null;
                appDbContext.Account.Update(account);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
        public async Task<bool> Login(string username, string password)
        {
            var account = await appDbContext.Account.FirstOrDefaultAsync(x => x.UserName == username);
            var accountPass = BCrypt.Net.BCrypt.Verify(password, account.Password);
            if (account != null && accountPass)
            {
                return true;
            }
            var verificationCode = GenerateVerificationCode();
            string userEmailAddress = account.Email;
            SendVerificationEmail(userEmailAddress, verificationCode);
            return false;
        }

        public async Task<bool> Authenticate(int userId, string verificationCode)
        {
            var account = await appDbContext.Account.FindAsync(userId);
            if (account != null && account.ResetPasswordToken == verificationCode)
            {
                account.ResetPasswordToken = verificationCode;
                account.ResetPasswordTokenExpiry = null;
                appDbContext.Account.Update(account);
                await appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
        public IEnumerable<Account> GetUser()
        {
            var users = appDbContext.Account.AsQueryable();
            return users;
        }
        
        public async Task<bool> UpdateUser(int userId, Account account)
        {
            var userMoi = await appDbContext.Account.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userMoi == null)
            {
                return false;
            }
            account.Avatar = userMoi.Avatar;
            account.Status = userMoi.Status;
            account.Phone = userMoi.Phone;
            account.Address = userMoi.Address;
            appDbContext.Account.Update(account);
            await appDbContext.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> ForgotPassword(string email)
        {
            var existingEmail = await appDbContext.Account.FirstOrDefaultAsync(x => x.Email == email);
            if (existingEmail == null)
            {
                return false;
            }
            string userEmailAddress = existingEmail.Email;
            string oldPassword = existingEmail.Password;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("trananhquan23702@gmail.com", "epabduexltgnjhfw"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("trananhquan23702@gmail.com");
                message.To.Add(new MailAddress("", userEmailAddress));
                message.Subject = "Xac nhan viec thay doi mat khau";
                string newPassword = BCryptNet.HashPassword(oldPassword);
                message.Body = "Mat khau cua ban da duoc thay doi thanh: " + newPassword;
                message.IsBodyHtml = true;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> ResetPassword(string resetToken, string newPassword)
        {
            var account = await appDbContext.Account.FirstOrDefaultAsync(x => x.ResetPasswordToken == resetToken);
            if (account != null)
            {
                if (account.ResetPasswordTokenExpiry.HasValue && account.ResetPasswordTokenExpiry > DateTime.Now)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    account.Password = hashedPassword;
                    account.ResetPasswordToken = resetToken;
                    account.ResetPasswordTokenExpiry = null;
                    account.CreatedAt = DateTime.Now;
                    appDbContext.Account.Update(account);
                    await appDbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Account>> GetAlls(int pageSize, int pageNumber)
        {
            var list = await appDbContext.Account.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return list;
        }

        public IEnumerable<Product> GetProduct()
        {
            var lstProduct = appDbContext.Product.AsQueryable();
            return lstProduct;
        }

        public async Task<List<Product>> GetRelatedProduct()
        {
            var lstProduct = await appDbContext.Product.FromSqlRaw("select * from Product join ProductType on Product.ProductTypeId = ProductType.ProductTypeId").ToListAsync();
            return lstProduct;
        }

        public async Task<List<Product>> GetFeaturedProducts()
        {
            var lstproducts = await appDbContext.Product.FromSqlRaw("select * from Product join ProductType on Product.ProductTypeId = ProductType.ProductTypeId order by Product.NumberOfViews desc").ToListAsync();
            return lstproducts;
        }

        public async Task<bool> Order(Carts carts)
        {
            appDbContext.Carts.Add(carts);
            await appDbContext.SaveChangesAsync();
            int cartsId = carts.CartId;
            List<CartItem> lstCartItems = new List<CartItem>();
            foreach (var item in lstCartItems)
            {
                CartItem cartItem = new CartItem();
                cartItem.Quantity = item.Quantity;
                cartItem.CartId = cartsId;
                cartItem.ProductId = item.Product.ProductId;
                lstCartItems.Add(cartItem);
            }
            appDbContext.CartItem.AddRange(lstCartItems);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        public IEnumerable<ProductType> GetProductType()
        {
            var lstProductType = appDbContext.ProductType.AsQueryable();
            return lstProductType;
        }

        public async Task<ProductReview> CreateProductReview(ProductReview productReview)
        {
            using (var transaction = appDbContext.Database.BeginTransaction())
            {
                var existingProductReview = await appDbContext.ProductReview.FirstOrDefaultAsync(x => x.ProductReviewId == productReview.ProductReviewId);
                if (existingProductReview == null)
                {
                    return null;
                }
                appDbContext.ProductReview.Add(productReview);
                await appDbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return productReview;
        }

        public IEnumerable<ProductReview> GetProductReview()
        {
            var lstProductReview = appDbContext.ProductReview.AsQueryable();
            return lstProductReview;
        }

        public IEnumerable<Product> GetNumberOfView()
        {
            var lstNumberOfView = appDbContext.Product.AsQueryable();
            return lstNumberOfView;
        }

        public IEnumerable<Order> GetOrder()
        {
            var lstOrder = appDbContext.Order.AsQueryable();
            return lstOrder;
        }

        public IEnumerable<OrderDetail> GetOrderDetail()
        {
            var lstOrderDetail = appDbContext.OrderDetail.AsQueryable();
            return lstOrderDetail;
        }

        public string CreatePaymentUrl(int orderId, HttpContext context)
        {
            var order = appDbContext.Order.FirstOrDefault(x => x.OrderId == orderId);
            if (order == null)
            {
                Console.WriteLine("Don hang khong ton tai");
            }
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];
            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)order.ActualPrice * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{order.FullName} thanh toan so tien {order.ActualPrice} cho don hang {order.OrderId}");
            pay.AddRequestData("vnp_OrderType", order.Payment.PaymentMethod);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);
            var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);
            return paymentUrl;
        }

        public PaymentResponse PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            return response;
        }

        public string SendEmail(string email)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("trananhquan23702@gmail.com", "epabduexltgnjhfw"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("trananhquan23702@gmail.com");
                message.To.Add(new MailAddress("", email));
                message.Subject = "Thong bao tinh trang don hang";
                message.Body = "Ban da dat hang thanh cong";
                message.IsBodyHtml = true;
                smtpClient.Send(message);
                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }

        public async Task<Order> CreateOrder(int userId, Order orderModel)
        {
            using (var transaction = await appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await appDbContext.Account.FirstOrDefaultAsync(x => x.UserId == userId);
                    if (user == null)
                    {
                        throw new Exception("Nguoi dung khong ton tai");
                    }
                    else
                    {
                        var order = new Order()
                        {
                            UserId = user.UserId,
                            PaymentId = orderModel.PaymentId,
                            Email = orderModel.Email,
                            Phone = orderModel.Phone,
                            FullName = orderModel.FullName,
                            Address = orderModel.Address,
                            ActualPrice = 0.0,
                            OriginalPrice = 0.0,
                            OrderStatusId = 1,
                            CreatedAt = DateTime.Now
                        };
                        double? totalOrderPrice = 0.0;
                        var orderDetails = new List<OrderDetail>();
                        foreach(var item in orderModel.OrderDetails)
                        {
                            var product = appDbContext.Product.Find(item.ProductId);
                            if (product == null)
                            {
                                throw new Exception("San pham khong ton tai");
                            }
                            else
                            {
                                var orderDetail = new OrderDetail()
                                {
                                    OrderId = order.OrderId,
                                    ProductId = item.ProductId,
                                    Quantity = item.Quantity,
                                    PriceTotal = product.Price * item.Quantity * (100 - product.Discount) / 100,
                                    CreatedAt= DateTime.Now,
                                    UpdateAt= DateTime.Now
                                };
                                totalOrderPrice += orderDetail.PriceTotal;
                                orderDetails.Add(orderDetail);
                                await appDbContext.SaveChangesAsync();
                            }
                        }
                        order.OrderDetails = orderDetails;
                        order.OriginalPrice = totalOrderPrice;
                        order.ActualPrice = order.OriginalPrice * 0.9;
                        appDbContext.OrderDetail.AddRange(orderDetails);
                        appDbContext.Order.Add(order);
                        await appDbContext.SaveChangesAsync();
                        var jsonOptions = new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve
                        };
                        var orderJson = JsonSerializer.Serialize(order, jsonOptions);
                        transaction.Commit();
                        return order;
                    }
                } catch (Exception ex) 
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}