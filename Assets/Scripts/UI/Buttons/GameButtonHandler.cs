using UnityEngine;

namespace Game.UI.Buttons
{
	public class GameButtonHandler : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] GameManager gameManager;
		[SerializeField] RetryController retryController;

		[Header("Buttons")]
		[SerializeField] RetryButton[] retryButtons;
		[SerializeField] PauseButton pauseButton;
		[SerializeField] UnpauseButton unpauseButton;

		void Awake()
		{
			pauseButton.OnClick += () => gameManager.PauseGame(true);
			unpauseButton.OnClick += () => gameManager.PauseGame(false);

			foreach (var button in retryButtons)
			{
				button.OnClick += () => retryController.Retry();
			}
		}
	}
}
