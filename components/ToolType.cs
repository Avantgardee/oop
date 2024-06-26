
namespace components;

public class ToolType
{
    public AbstractFactory Factory { get; set; }
    public AbstractDrawStrategy Strategy { get; set; }

    public int CountOfPoints { get; set; }
    public ToolType(AbstractFactory factory, AbstractDrawStrategy strategy, int countOfPoints)
    {
        Factory = factory;
        Strategy = strategy;
        CountOfPoints = countOfPoints;
    }
}