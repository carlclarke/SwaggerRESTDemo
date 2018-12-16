using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerRESTDemo.Helpers
{
    public class Pricing
    {
        public List<Models.SpecialPrice> GetProductPrices(Models.Product product)
        {
            List<Models.SpecialPrice> result = new List<Models.SpecialPrice>();
            Models.SpecialPrice defaultPrice = new Models.SpecialPrice();
            bool priceCalculated = false;

            if(product != null)
            {
                // setup a default incase anything goes wrong
                defaultPrice.Minimum = 1;
                defaultPrice.Maximum = int.MaxValue;
                defaultPrice.SalesPrice = product.SalesPrice;

                PriceCalculator pc = new PriceCalculator();

                try
                {
                    result.Add(new Models.SpecialPrice() { Minimum = 1, Maximum = 4, SalesPrice = pc.CalcProductPrices(product.SalesPrice, product.CostPrice, 1, 4) });
                    result.Add(new Models.SpecialPrice() { Minimum = 5, Maximum = 10, SalesPrice = pc.CalcProductPrices(product.SalesPrice, product.CostPrice, 5, 10) });
                    result.Add(new Models.SpecialPrice() { Minimum = 11, Maximum = 20, SalesPrice = pc.CalcProductPrices(product.SalesPrice, product.CostPrice, 11, 20) });
                    result.Add(new Models.SpecialPrice() { Minimum = 21, Maximum = int.MaxValue, SalesPrice = pc.CalcProductPrices(product.SalesPrice, product.CostPrice, 20, int.MaxValue) });

                    priceCalculated = true;
                }
                catch(Exception ex)
                {
                    // log some error details
                }

                if (!priceCalculated)
                {
                    result.Clear();
                    result.Add(defaultPrice);
                }
            }

            return result;
        }

        public class PriceCalculator
        {
            public decimal CalcProductPrices(decimal salesPrice, decimal costPrice, int minimum, int maximum)
            {
                decimal result = salesPrice;

                decimal discount_5_10 = 1.0m - 0.05m;
                decimal discount_11_20 = 1.0m - 0.10m;
                decimal discount_21_plus = 1.0m - 0.15m;

                if (minimum <= 1 && maximum <= 4)
                {
                    result = Decimal.Round(salesPrice, 2);
                }
                else if (minimum >= 5 && maximum <= 10)
                {
                    result = Decimal.Round(salesPrice * discount_5_10, 2);
                }
                else if (minimum >= 11 && maximum <= 20)
                {
                    result = Decimal.Round(salesPrice * discount_11_20, 2);
                }
                else if (minimum >= 21 && maximum <= int.MaxValue)
                {
                    result = Decimal.Round(salesPrice * discount_21_plus, 2);
                }

                return result;
            }
        }

    }
}
