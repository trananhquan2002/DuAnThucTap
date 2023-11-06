using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDatDoAnAPI.Entities;
using QuanLyDatDoAnAPI.IServices;
using QuanLyDatDoAnAPI.Services;

namespace QuanLyDatDoAnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices accountServices;

        public AccountController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromQuery] string userName, string email, string password)
        {
            var res = await accountServices.CreateAccount(userName, email, password);
            return Ok(res);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyAccount([FromQuery] int userId, string verificationCode)
        {
            var res = await accountServices.VerifyAccount(userId, verificationCode);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await accountServices.Login(username, password);
            return Ok(user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery] int userId, string verificationCode)
        {
            var user = await accountServices.Authenticate(userId, verificationCode);
            return Ok(user);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await accountServices.ForgotPassword(email);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string resetPasswordToken, string newPassword)
        {
            var result = await accountServices.ResetPassword(resetPasswordToken, newPassword);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("getListUser")]
        public IActionResult GetUser()
        {
            var user = accountServices.GetUser();
            return Ok(user);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(int accountId, Account user)
        {
            var result = await accountServices.UpdateUser(accountId, user);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> GetAlls(int pageSize, int pageNumber)
        {
            return Ok(accountServices.GetAlls(pageSize, pageNumber));
        }

        [HttpGet("getListProducts")]
        public IActionResult GetListProducts()
        {
            var res = accountServices.GetProduct();
            return Ok(res);
        }

        [HttpGet("getRelatedProducts")]
        public IActionResult GetRelatedProducts()
        {
            var res = accountServices.GetRelatedProduct();
            return Ok(res);
        }

        [HttpGet("getFeaturedProducts")]
        public IActionResult GetFeaturedProducts()
        {
            var res = accountServices.GetFeaturedProducts();
            return Ok(res);
        }

        [HttpPost("order")]
        public async Task<IActionResult> Order([FromBody] Carts carts)
        {
            var order = await accountServices.Order(carts);
            return Ok(order);
        }

        [HttpGet("getListProductTypes")]
        public IActionResult GetListProductTypes()
        {
            var res = accountServices.GetProductType();
            return Ok(res);
        }

        [HttpGet("getNumberOfView")]
        public IActionResult GetNumberOfView()
        {
            var res = accountServices.GetNumberOfView();
            return Ok(res);
        }

        [HttpPost("createProductReview")]
        public async Task<IActionResult> CreateProductReview([FromBody] ProductReview productReview)
        {
            var productReviews = await accountServices.CreateProductReview(productReview);
            if (productReviews != null)
            {
                return Ok(productReviews);
            }
            return NotFound();
        }

        [HttpGet("getOrder")]
        public IActionResult GetOrder()
        {
            var res = accountServices.GetOrder();
            return Ok(res);
        }

        [HttpGet("getOrderDetail")]
        public IActionResult GetOrderDetail()
        {
            var res = accountServices.GetOrderDetail();
            return Ok(res);
        }

        [HttpPost("createOrder")]
        public IActionResult CreateOrder(int userId, Order orderModel)
        {
            var response = accountServices.CreateOrder(userId, orderModel);
            return Ok(response);
        }

        [HttpPost("executePayment")]
        public IActionResult PaymentCallback()
        {
            var response = accountServices.PaymentExecute(Request.Query);
            return Ok(response);
        }
    }
}