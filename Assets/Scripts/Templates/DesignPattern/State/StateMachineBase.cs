using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.DesignPatterns.State
{
	public class StateMachineBase<T> where T : IState
	{
		/* Fields */
		T firstState;

		Dictionary<System.Type, T> stateDictionary;

		List<T> states;
		List<T> States
		{
			get
			{
				if (stateDictionary == null) return null;

				if (states == null || states.Count != stateDictionary.Count)
				{
					states = new List<T>(stateDictionary.Values);
				}
				return states;
			}
		}

		bool isDebug = false;

		//-------------------------------------------------------------------
		/* Properties */
		public T CurrentState { get; private set; }

		//-------------------------------------------------------------------
		/* Constructors */
		public StateMachineBase(bool isDebug = false, params T[] states)
		{
			if (states.Length == 0)
			{
				throw new System.Exception("Stateが登録されていません");
			}

			stateDictionary = new Dictionary<System.Type, T>();
			foreach (var state in states)
			{
				stateDictionary.Add(state.GetType(), state);
			}

			firstState = states[0];
			CurrentState = firstState;
			if (isDebug)
			{
				this.isDebug = isDebug;
				Debug.Log($"firstState: {firstState}");
			}
		}

		//-------------------------------------------------------------------
		/* Events */
		public void OnUpdate()
		{
			CurrentState.OnUpdate();
		}

		public void OnFixedUpdate()
		{
			CurrentState.OnFixedUpdate();
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> 登録された状態リストの初期化 </summary>
		public void InitializeStates()
		{
			// 状態の遷移イベントを登録する
			foreach (var state in stateDictionary.Values)
			{
				state.OnStateTransition -= TransitionState;
				state.OnStateTransition += TransitionState;
			}

			// 現在の状態に遷移する
			CurrentState.OnEnter();
		}

		bool IsExistState(T state)
		{
			return stateDictionary.ContainsKey(state.GetType());
		}

		/// <summary> 状態をリストに追加する </summary>
		public virtual void AddState(T state)
		{
			// リストに存在しない場合のみ、追加する
			if (!IsExistState(state))
			{
				stateDictionary.Add(state.GetType(), state);
			}
			else
			{
				throw new System.Exception("既に登録されているStateです");
			}
		}

		//------------------------------------------------------------
		/*----- Transition -----*/
		/// <summary> 指定した状態に遷移する </summary>
		public void TransitionState(System.Type type)
		{
			// 型がTのサブクラスの場合
			if (type.IsSubclassOf(typeof(T)))
			{
				var state = stateDictionary[type];

				// リストに存在する場合
				if (state != null)
				{
					CurrentState.OnExit();
					CurrentState = state;
					CurrentState.OnEnter();
				}

				// リストに存在しない場合
				else
				{
					throw new System.Exception("存在しないStateです");
				}
			}

			// 型がTのサブクラスでない場合
			else
			{
				throw new System.Exception("不正な型です");
			}
		}

		/// <summary> 指定した状態に遷移する </summary>
		public void TransitionState<State>() where State : T
		{
			TransitionState(typeof(State));
		}

		/// <summary> 初期状態に遷移する </summary>
		public void TransitionToFirstState()
		{
			TransitionState(firstState.GetType());
		}

		/// <summary> 現在の状態の次に登録された状態に遷移する </summary>
		public void TransitionToNextState()
		{
			var currentStateIndex = States.IndexOf(CurrentState);

			if (currentStateIndex < States.Count - 1)
			{
				TransitionState(States[currentStateIndex + 1].GetType());
			}
			else
			{
				throw new System.Exception("次のStateが存在しません");
			}
		}

		/// <summary> 現在の状態の前に登録された状態に遷移する </summary>
		public void TransitionToPreviousState()
		{
			var currentStateIndex = States.IndexOf(CurrentState);

			if (currentStateIndex > 0)
			{
				TransitionState(States[currentStateIndex - 1].GetType());
			}
			else
			{
				throw new System.Exception("前のStateが存在しません");
			}
		}
	}
}
