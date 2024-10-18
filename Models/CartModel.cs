﻿namespace Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}