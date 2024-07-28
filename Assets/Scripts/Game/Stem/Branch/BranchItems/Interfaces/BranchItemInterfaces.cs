namespace Game.Tree.Branch.Item
{
	public interface IGettableItem
	{
		/// <summary> 取得時の処理 </summary>
		void GetItem();
	}

	/// <summary> スコア取得可能なアイテム </summary>
	public interface IScoreItem : IGettableItem
	{
		/// <summary> 取得時のスコア </summary>
		int Score { get; }
	}

	/// <summary> ゲームオーバーになるアイテム </summary>
	public interface IDeadlyItem : IGettableItem
	{

	}
}
