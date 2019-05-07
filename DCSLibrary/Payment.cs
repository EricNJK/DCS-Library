using System.Collections.Generic;

namespace DCSLibrary
{
    public class Payment
    {
        public string Method { get; }
        /// <summary>
        /// Map of product id and amount, for one item each
        /// </summary>
        /// <remarks>
        /// Does not consider Product.Count
        /// </remarks>
        public Dictionary<int, float> Info { get; }

        public Payment(string paymentMethod, Dictionary<int, float> info)
        {
            Method = paymentMethod;
            Info = info;
        }

    }
}
