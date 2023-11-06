using QuanLyDatDoAnAPI.Entities;

namespace QuanLyDatDoAnAPI.IServices
{
    public interface IAccountServices
    {
        // yêu cầu tuần trước
        Task<Account> CreateAccount(string userName, string email, string password);
        Task<bool> VerifyAccount(int userId, string verificationCode);
        Task<bool> Login(string username, string password);
        Task<bool> Authenticate(int userId, string verificationCode);
        IEnumerable<Account> GetUser();
        Task<bool> UpdateUser(int userId, Account account);
        Task<bool> ForgotPassword(string email);
        Task<bool> ResetPassword(string resetPasswordToken, string newPassword);
        Task<IEnumerable<Account>> GetAlls(int pageSize, int pageNumber);
        IEnumerable<Product> GetProduct();
        Task<List<Product>> GetRelatedProduct();
        Task<List<Product>> GetFeaturedProducts();
        Task<bool> Order(Carts carts);
        IEnumerable<ProductType> GetProductType();
        IEnumerable<ProductReview> GetProductReview();
        IEnumerable<Product> GetNumberOfView();
        Task<ProductReview> CreateProductReview(ProductReview productReview);
        IEnumerable<Order> GetOrder();
        IEnumerable<OrderDetail> GetOrderDetail();
        Task<Order> CreateOrder(int userId, Order orderModel);
        string CreatePaymentUrl(int orderId, HttpContext context);
        PaymentResponse PaymentExecute(IQueryCollection collections);
        string SendEmail(string email);
    }
}