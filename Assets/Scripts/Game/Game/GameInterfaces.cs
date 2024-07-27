using Template.Utils;

namespace Game
{
	/// <summary> ゲームオーバー判定 </summary>
	public interface IGameOverChecker : IFoundObject
	{
		/// <summary> ゲームオーバー判定 </summary>
		bool CheckGameOvered();
	}
}
