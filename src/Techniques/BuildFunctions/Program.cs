var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Graph>();

var app = builder.Build();

app.MapGet("/", (int? x, int? y, int? z, Graph g) =>
{
    IEnumerable<Vector3d> query = g;

    if (x != null)
    {
        query = query.Where(v => v.x == x);
    }

    if (y != null)
    {
        query = query.Where(v => v.y == y);
    }

    if (z != null)
    {
        query = query.Where(v => v.z == z);
    }

    return query;
});

app.Run();
