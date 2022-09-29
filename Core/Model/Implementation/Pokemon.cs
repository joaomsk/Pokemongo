using Domain.Collection;

namespace Domain.Model.Implementation;

[BsonCollection("pokemon")]
public class Pokemon : Document
{
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public double Height { get; set; }
    public IList<string> Moves { get; set; } = null!;
    public IList<string> Types { get; set; } = null!;
}