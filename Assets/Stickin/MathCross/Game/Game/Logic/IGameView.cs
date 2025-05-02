
namespace stickin.mathcross
{
    public interface IGameView
    {
        void Init(int difficultNumber, Board board, Pocket pocket, RewardResourceModule rewardResourceModule, string difficultTitle);
    }
}