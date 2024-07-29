using System.Collections;
using System.Collections.Generic;
using Game.UI;
using Template.DesignPatterns.State;
using UnityEngine;

namespace Game.State
{
	/// <summary> ゲーム状態操作クラス </summary>
	public class GameStateController : MonoBehaviour
	{
		/* Fields */
		[Header("State UIGroups")]
		[SerializeField] GameStateBase[] states;

		StateMachineBase<GameStateBase> stateMachine;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		public void Initialize()
		{
			stateMachine = new StateMachineBase<GameStateBase>(true,
				states
			);

			stateMachine.InitializeStates();
		}

		//-------------------------------------------------------------------
		/* Methods */
		public void TransitionState<T>() where T : GameStateBase
		{
			stateMachine.TransitionState<T>();
		}

	}
}
