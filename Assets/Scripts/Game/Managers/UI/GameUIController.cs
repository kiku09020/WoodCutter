using Template.Utils.UI.UIManager;
using UnityEngine;

namespace Game.UI
{
	/// <summary> ゲーム内のUIの操作クラス </summary>
	public class GameUIController : MonoBehaviour
	{
		[SerializeField] GameManager gameManager;

		void Awake()
		{
			gameManager.OnGameOvered += () =>
			{
				UIManager.ShowUIGroup<GameOverUIGroup>();
			};
		}
	}
}
