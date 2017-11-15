namespace ProNet
{
    public interface IRank : IRecommended
    {
        double Rank { get; set; }
    }
}