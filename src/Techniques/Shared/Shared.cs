public record Vector3d(int x, int y, int z) { }

public class Graph : List<Vector3d> 
{
    public Graph()
    {
        for (int i = 0; i < 1000; i++)
        {
            this.Add(new Vector3d(
                Random.Shared.Next(100),
                Random.Shared.Next(100),
                Random.Shared.Next(100)
            ));
        }
    }
}