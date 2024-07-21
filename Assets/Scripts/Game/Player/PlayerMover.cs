using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Player
{
	/// <summary> プレイヤー移動クラス </summary>
	public class PlayerMover : MonoBehaviour
	{
		/* Fields */
		[Header("Parameters")]
		[SerializeField] float playerPositionX = 1.5f;

		[Header("Tween")]
		[SerializeField] float moveDuration = 0.5f;
		[SerializeField] Ease moveEase;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			var player = GetComponentInParent<PlayerObject>();
			player.OnChangeDirection += Move;
		}

		//-------------------------------------------------------------------
		/* Methods */
		void Move(Transform playerTransform, Directions direction)
		{
			var directionSign = direction == Directions.Left ? -1 : 1;
			var targetPositionX = playerPositionX * directionSign;

			playerTransform.DOMoveX(targetPositionX, moveDuration)
				.SetEase(moveEase);
		}
	}
}
