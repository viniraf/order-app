using OrderApp.Domain.Order;

namespace OrderApp.Endpoints.Orders;

public class OrderPost
{
    public static string Template => "/Orders";

    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };

    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext httpContext, ApplicationDbContext context)
    {
        // TODO: Add validations if the result of db is null
        // Return properly message using Result.ValidationProblem() passing the ConvertToProblemDetails method

        var clientId = httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = httpContext.User.Claims.First(c => c.Type == "Name").Value;

        var products = new List<Product>();
        var productsFound = context.Products.Where(p => orderRequest.ProductsIds.Contains(p.Id)).ToList();

        var order = new OrderClass(clientId, clientName, products, orderRequest.DeliveryAddress);

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/orders/{order.Id}", $"Created Order Id: {order.Id}");
    }
}