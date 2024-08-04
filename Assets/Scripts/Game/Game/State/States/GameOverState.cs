using Cysharp.Threading.Tasks;
using DG.Tweening;
using Template.Utils.UI.UIManager;
using UnityEngine;

namespace Game.State
{
	public class GameOverState : GameStateBase
	{
		[Header("HitStop")]
		[SerializeField] float hitStopDuration = 1f;

		[Header("Camera")]
		[SerializeField] Player.PlayerObject player;
		[SerializeField] float cameraDuration = 1f;
		[SerializeField] float cameraZoomSize = 1f;
		[SerializeField] Ease cameraEase;

		public override async void OnEnter()
		{
			base.OnEnter();

			ControllCamera();
			await HitStop();
		}

		async UniTask HitStop()
		{
			// ヒットストップ
			Time.timeScale = 0;
			await UniTask.Delay(System.TimeSpan.FromSeconds(hitStopDuration), true);
			Time.timeScale = 1;
		}

		void ControllCamera()
		{
			var camera = Camera.main;


			var zoomTween = camera.DOOrthoSize(camera.orthographicSize - cameraZoomSize, cameraDuration)
				.SetEase(cameraEase);

			var targetPos = new Vector3(player.transform.position.x / 2, player.transform.position.y / 2, camera.transform.position.z);
			var moveTween = camera.transform.DOMove(targetPos, cameraDuration)
				.SetEase(cameraEase);

			DOTween.Sequence().
				Append(zoomTween)
				.Join(moveTween)
				.SetUpdate(true);
		}
	}
}
