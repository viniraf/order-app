namespace OrderApp.Endpoints.Products
{
    public class ProductGetShowcase
    {
        public static string Template => "/products/showcase";

        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };

        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static async Task<IResult> Action(ApplicationDbContext context, [FromQuery] int page = 1, [FromQuery] int rows = 10, [FromQuery] string orderBy = "name")
        {
            if (rows > 1000)
            {
                return Results.Problem(title: "Is not possible to fetch this number of rows", statusCode: 400);
            }

            var productsUnpaginated = context.Products.AsNoTracking().Include(p => p.Category).Where(p => p.HasStock && p.Category.Active);

            if (orderBy != "name" && orderBy != "price")
            {
                return Results.Problem(title: "Order only by price or name", statusCode: 400);
            }

            if (orderBy == "name")
            {
                productsUnpaginated = productsUnpaginated.OrderBy(p => p.Name);
            }
            else
            {
                productsUnpaginated = productsUnpaginated.OrderBy(p => p.Price);
            }

            var productsPaginated = productsUnpaginated.Skip((page - 1) * rows).Take(rows);

            var products = productsPaginated.ToList();

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
