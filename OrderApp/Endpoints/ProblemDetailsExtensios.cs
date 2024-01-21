namespace OrderApp.Endpoints;

public static class ProblemDetailsExtensios
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
            .GroupBy(c => c.Key)
            .ToDictionary(c => c.Key, g => g.Select(x => x.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> errors)
    {
        var dictionary = new Dictionary<string, string[]>
        {
            { "Error", errors.Select(e => e.Description).ToArray() }
        };

        return dictionary;
    }
}
