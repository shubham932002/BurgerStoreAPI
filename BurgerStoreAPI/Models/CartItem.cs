namespace BurgerStoreAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string BurgerName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public decimal TotalPrice => Quantity * Price;
    }
}
