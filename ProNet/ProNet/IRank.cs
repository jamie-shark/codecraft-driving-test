namespace ProNet
{
    public interface IRank : IRecommended
    {
        string GetId();
        double Rank { get; set; }
    }
}