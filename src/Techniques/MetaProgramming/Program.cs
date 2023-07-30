var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Graph>();

var app = builder.Build();

//'e' => x => v => v.X == x
//'l' => x => v => v.X < x
//'g' => x => v => v.X > x

var createXCondition = CreateCondition(v => v.x);
var createYCondition = CreateCondition(v => v.y);
var createZCondition = CreateCondition(v => v.z);

Func<string, Func<Vector3d, bool>> CreateCondition(Func<Vector3d, int> coordSelector)
{
    return val => val switch
    {
        ['e', .. var rest] => v => coordSelector(v) == int.Parse(rest),
        ['l', .. var rest] => v => coordSelector(v) < int.Parse(rest),
        ['g', .. var rest] => v => coordSelector(v) > int.Parse(rest),
    };
};


app.MapGet("/", (string? x, string? y, string? z, Graph g) =>
{
    IEnumerable<Vector3d> query = g;

    if (x != null)
    {
        query = query.Where(createXCondition(x));
    }

    if (y != null)
    {
        query = query.Where(createYCondition(y));
    }

    if (z != null)
    {
        query = query.Where(createZCondition(z));
    }

    return query;
});

app.Run();
