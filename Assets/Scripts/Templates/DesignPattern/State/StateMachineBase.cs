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
				throw new System.Exception("State‚ª“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
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
		/// <summary> “o˜^‚³‚ê‚½ó‘ÔƒŠƒXƒg‚Ì‰Šú‰» </summary>
		public void InitializeStates()
		{
			// ó‘Ô‚Ì‘JˆÚƒCƒxƒ“ƒg‚ğ“o˜^‚·‚é
			foreach (var state in stateDictionary.Values)
			{
				state.OnStateTransition -= TransitionState;
				state.OnStateTransition += TransitionState;
			}

			// Œ»İ‚Ìó‘Ô‚É‘JˆÚ‚·‚é
			CurrentState.OnEnter();
		}

		bool IsExistState(T state)
		{
			return stateDictionary.ContainsKey(state.GetType());
		}

		/// <summary> ó‘Ô‚ğƒŠƒXƒg‚É’Ç‰Á‚·‚é </summary>
		public virtual void AddState(T state)
		{
			// ƒŠƒXƒg‚É‘¶İ‚µ‚È‚¢ê‡‚Ì‚İA’Ç‰Á‚·‚é
			if (!IsExistState(state))
			{
				stateDictionary.Add(state.GetType(), state);
			}
			else
			{
				throw new System.Exception("Šù‚É“o˜^‚³‚ê‚Ä‚¢‚éState‚Å‚·");
			}
		}

		//------------------------------------------------------------
		/*----- Transition -----*/
		/// <summary> w’è‚µ‚½ó‘Ô‚É‘JˆÚ‚·‚é </summary>
		public void TransitionState(System.Type type)
		{
			// Œ^‚ªT‚ÌƒTƒuƒNƒ‰ƒX‚Ìê‡
			if (type.IsSubclassOf(typeof(T)))
			{
				var state = stateDictionary[type];

				// ƒŠƒXƒg‚É‘¶İ‚·‚éê‡
				if (state != null)
				{
					CurrentState.OnExit();
					CurrentState = state;
					CurrentState.OnEnter();
				}

				// ƒŠƒXƒg‚É‘¶İ‚µ‚È‚¢ê‡
				else
				{
					throw new System.Exception("‘¶İ‚µ‚È‚¢State‚Å‚·");
				}
			}

			// Œ^‚ªT‚ÌƒTƒuƒNƒ‰ƒX‚Å‚È‚¢ê‡
			else
			{
				throw new System.Exception("•s³‚ÈŒ^‚Å‚·");
			}
		}

		/// <summary> w’è‚µ‚½ó‘Ô‚É‘JˆÚ‚·‚é </summary>
		public void TransitionState<State>() where State : T
		{
			TransitionState(typeof(State));
		}

		/// <summary> ‰Šúó‘Ô‚É‘JˆÚ‚·‚é </summary>
		public void TransitionToFirstState()
		{
			TransitionState(firstState.GetType());
		}

		/// <summary> Œ»İ‚Ìó‘Ô‚ÌŸ‚É“o˜^‚³‚ê‚½ó‘Ô‚É‘JˆÚ‚·‚é </summary>
		public void TransitionToNextState()
		{
			var currentStateIndex = States.IndexOf(CurrentState);

			if (currentStateIndex < States.Count - 1)
			{
				TransitionState(States[currentStateIndex + 1].GetType());
			}
			else
			{
				throw new System.Exception("Ÿ‚ÌState‚ª‘¶İ‚µ‚Ü‚¹‚ñ");
			}
		}

		/// <summary> Œ»İ‚Ìó‘Ô‚Ì‘O‚É“o˜^‚³‚ê‚½ó‘Ô‚É‘JˆÚ‚·‚é </summary>
		public void TransitionToPreviousState()
		{
			var currentStateIndex = States.IndexOf(CurrentState);

			if (currentStateIndex > 0)
			{
				TransitionState(States[currentStateIndex - 1].GetType());
			}
			else
			{
				throw new System.Exception("‘O‚ÌState‚ª‘¶İ‚µ‚Ü‚¹‚ñ");
			}
		}
	}
}
