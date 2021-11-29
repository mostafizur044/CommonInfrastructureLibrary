namespace Services.DTO
{
    public class GetProducts
    {
        public int Page { get; set; } = 0;
        public int PageNo { get; set; } = 10;
        public int Name { get; set; }
        public int SKU { get; set; }
    }
}
