namespace OrderApp.Endpoints.Products
{
    public class ProductGet
    {
        public static string Template => "/products";

        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

        public static Delegate Handle => Action;

        public static async Task<IResult> Action(ApplicationDbContext context)
        {
            var products = context.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();

            if (products is null || products.Any() == false)
            {
                return Results.NotFound("There is no record in the database");
            }

            var results = products.Select(p => 
            new ProductResponse
            {
                Name = p.Name,
                CategoryName = p.Category.Name,
                Description = p.Description,
                HasStock = p.HasStock,
                Price = p.Price,
                Active = p.Active,
            });

            return Results.Ok(results);
        }
    }
}
