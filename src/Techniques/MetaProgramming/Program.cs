using System.Linq.Expressions;
using QueryPredicates = System.Collections.Generic.Dictionary<string, System.Func<string, System.Func<Vector3d, bool>>>;

var queryParameterMap = new QueryPredicates();

//var createXCondition = CreateCondition(v => v.x);
//var createYCondition = CreateCondition(v => v.y);
//var createZCondition = CreateCondition(v => v.z);

Func<int, int, bool> GetComparison(char c)
{
    return c switch
    {
        'e' => (a, b) => a == b,
        'l' => (a, b) => a < b,
        'g' => (a, b) => a > b,
    };
}

var properties = typeof(Vector3d).GetProperties();

var vectorParam = Expression.Parameter(typeof(Vector3d)); //  CreateCondition(v => v.x) : v

foreach (var propertyInfo in properties)
{
    var propertyAccess = Expression.Property(vectorParam, propertyInfo); // CreateCondition(v => v.x) : v.x, v.y, v.z
    var propertyValue  = Expression.Lambda<Func<Vector3d, int>>(propertyAccess, vectorParam).Compile(); // CreateCondition(v => v.x) : (v => v.x)

    queryParameterMap[propertyInfo.Name.ToLower()] = str =>
    {
        var comp   = GetComparison(str[0]);
        var number = int.Parse(str[1..]);

        return vector => comp(propertyValue(vector), number);
    };
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(queryParameterMap);
builder.Services.AddSingleton<Graph>();

var app = builder.Build();

app.MapGet("/", (HttpRequest req, QueryPredicates queryPredicates, Graph g) =>
{
    IEnumerable<Vector3d> query = g;

    foreach (var (key, values) in req.Query)
    {
        string? val = values;

        if (!string.IsNullOrEmpty(val) && queryPredicates.TryGetValue(key, out var predicate))
        {
            query = query.Where(predicate(val));
        }
    }

    return query;
});

app.Run();