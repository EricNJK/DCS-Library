using System;
using System.Collections.Generic;

namespace DCSLibrary
{
    //struct ProdCount
    //{
    //    public Product Product { get; set; }
    //    public int PCount { get; set; }

    //    public void Add()
    //    {
    //        pc
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        ProdCount p = (ProdCount)obj;
    //        return (Product.Equals(p.Product) &&PCount == p.PCount);
    //    }

    //    public override int GetHashCode()   //Generated code
    //    {
    //        var hashCode = -1462569664;
    //        hashCode = hashCode * -1521134295 + EqualityComparer<Product>.Default.GetHashCode(Product);
    //        hashCode = hashCode * -1521134295 + PCount.GetHashCode();
    //        return hashCode;
    //    }
    //}
    public class Order
    {
        private float totalPayable = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paymentMethod"> Means of payment </param>
        /// <param name="storeName"> Name of the store </param>
        /// <param name="customerId"></param>
        public Order(string paymentMethod, string storeName, int customerId)
        {
            this.PaymentMethod = paymentMethod;
            AmountPaid = 0;
            DoP = DateTime.Now;    //Set order time
            Store = storeName;
            this.CustomerId = customerId;
        }

        public int Id { get; set; }
        public Dictionary<int, Product> Products { get; } = new Dictionary<int, Product>();
        public string PaymentMethod { get; }
        public float AmountPaid { get; }
        public float TotalPayable
        {
            get
            {
                UpdateTotalPayable();
                return totalPayable;
            }
        }

        private void UpdateTotalPayable()
        {
            foreach (var pair in Products)
            {
                totalPayable += (pair.Value.Price * pair.Value.NumInOrder);
            }
        }

        public DateTime DoP { get; }
        public string Store { get; }
        public int CustomerId { get; }

        /// <summary>
        /// Adds product to order
        /// </summary>
        /// <param name="p"> Product</param>
        public void AddProduct(Product p)
        {
            if (Products.ContainsKey(p.Id))
            { //If it exists, increment count
                Products[p.Id].NumInOrder++;
            }
            else
            {   //Add with new key
                Products.Add(p.Id, p);
            }
        }

        /// <summary>
        /// Adds a <see cref="Dictionary<>"/> of products
        /// </summary>
        /// <param name="products"> Products to add </param>
        public void AddProduct(Dictionary<int, Product> products)
        {
            foreach (KeyValuePair<int, Product> pair in products)
            {
                AddProduct(pair.Value);
            }
        }

        /// <summary>
        /// Totally removes product from order
        /// </summary>
        /// <param name="productId"> Product id </param>
        public void RemoveProduct(int productId)
        {
            Products.Remove(productId);
        }

        /// <summary>
        /// Reduces the count of a product
        /// </summary>
        /// <param name="productId"> Product id </param>
        /// <param name="count"> Number of items to remove </param>
        public void RemoveProduct(int productId, int count)
        {
            if (Products[productId].NumInOrder > count)
                Products[productId].NumInOrder -= count;
            else
                RemoveProduct(productId);
        }
    }
}
