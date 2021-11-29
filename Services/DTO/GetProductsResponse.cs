using Services.Entity;
using System.Collections.Generic;

namespace Services.DTO
{
    public class GetProductsResponse : CommonResponse
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public long TotalCount { get; set; }
    }
}
