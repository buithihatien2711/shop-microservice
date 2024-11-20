namespace Basket.API.Entities
{
    public class BasketCheckoutEntity
    {
        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAdress { get; set; }
    }
}
