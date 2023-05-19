using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using APIMySqlСoursework.Query;
using Yandex.Checkout.V3;

namespace APIMySqlСoursework.Payments
{
    public class YooKassaPay
    {
        Client Client { get; set; }
        public YooKassaPay()
        {
            Client = new Client(
                shopId: "321198",
                secretKey: "test_zAgW7pQsQ-69v6uY3EhOd8FmGwvqg6BZUbmcomGMjSE");
        }

        public async Task<Payment> GetPayment(double sum)
        {
            var newPayment = new NewPayment
            {
                Amount = new Amount { Value = Convert.ToDecimal(sum), Currency = "RUB" },
                Confirmation = new Confirmation
                {
                    Type = ConfirmationType.Redirect,
                    ReturnUrl = ""
                }
            };
            Payment payment = Client.CreatePayment(newPayment);
            return payment;
        }// Client.GetPayment(id); обновить в бд 
    }
}
