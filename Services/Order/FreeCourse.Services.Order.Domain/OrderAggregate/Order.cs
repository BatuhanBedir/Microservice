using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //Ef core features
    //-owned type
    //-shadow property
    //-backing field
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; } //owned entity type
        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems; //ef core içerisinde okuma ve yazma işlemini field üzerinden gerçekleştiriliyorsa -> backing field.
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }
        public Order()
        {
            
        }
        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
