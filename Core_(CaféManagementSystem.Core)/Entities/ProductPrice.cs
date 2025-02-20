using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core__CaféManagementSystem.Core_.Entities
{
    public class ProductPrice
    {
        public int PriceId { get; set; }
        public int ProductId { get; set; }
        public double ImportPrice { get; set; }
        public DateTime EffectiveDate { get; set; }

        public ProductPrice(int productId,double importPrice)
        {
            ProductId = productId;
            ImportPrice = importPrice;
            EffectiveDate = DateTime.Now;
        }
    }
}
