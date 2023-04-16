namespace GuitarStarBackOffice.Shared
{
    public class ProductsSearchRequestModel
    {
        public string Query { get; set; }

        public Guid? Category { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public int Page { get; set; } = 1;
    }
}
