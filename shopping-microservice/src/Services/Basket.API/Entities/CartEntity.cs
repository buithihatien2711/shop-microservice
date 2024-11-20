namespace Basket.API.Entities
{
    public class CartEntity
    {
        public string UserName { get; set; }

        public List<CartItemEntity> Items { get; set; } = new List<CartItemEntity>();

        public CartEntity()
        {
            
        }

        public CartEntity(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice()
        {
            return Items.Sum(item => item.ItemPrice * item.Quantity);
        }
    }
}
