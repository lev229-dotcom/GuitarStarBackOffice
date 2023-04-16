namespace GuitarStarBackOffice.Shared
{
    public class ProductsSearchResponseModel
    {
        public IEnumerable<Product> Products { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
