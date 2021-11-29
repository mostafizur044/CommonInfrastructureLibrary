using Services.Entity;

namespace Services.DTO
{
    public class GetProductResponse : CommonResponse
    {
        public Product Product { get; set; }
    }
}
