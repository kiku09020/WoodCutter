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
				throw new System.Exception("State���o�^����Ă��܂���");
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
		/// <summary> �o�^���ꂽ��ԃ��X�g�̏����� </summary>
		public void InitializeStates()
		{
			// ��Ԃ̑J�ڃC�x���g��o�^����
			foreach (var state in stateDictionary.Values)
			{
				state.OnStateTransition -= TransitionState;
				state.OnStateTransition += TransitionState;
			}

			// ���݂̏�ԂɑJ�ڂ���
			CurrentState.OnEnter();
		}

		bool IsExistState(T state)
		{
			return stateDictionary.ContainsKey(state.GetType());
		}

		/// <summary> ��Ԃ����X�g�ɒǉ����� </summary>
		public virtual void AddState(T state)
		{
			// ���X�g�ɑ��݂��Ȃ��ꍇ�̂݁A�ǉ�����
			if (!IsExistState(state))
			{
				stateDictionary.Add(state.GetType(), state);
			}
			else
			{
				throw new System.Exception("���ɓo�^����Ă���State�ł�");
			}
		}

		//------------------------------------------------------------
		/*----- Transition -----*/
		/// <summary> �w�肵����ԂɑJ�ڂ��� </summary>
		public void TransitionState(System.Type type)
		{
			// �^��T�̃T�u�N���X�̏ꍇ
			if (type.IsSubclassOf(typeof(T)))
			{
				var state = stateDictionary[type];

				// ���X�g�ɑ��݂���ꍇ
				if (state != null)
				{
					CurrentState.OnExit();
					CurrentState = state;
					CurrentState.OnEnter();
				}

				// ���X�g�ɑ��݂��Ȃ��ꍇ
				else
				{
					throw new System.Exception("���݂��Ȃ�State�ł�");
				}
			}

			// �^��T�̃T�u�N���X�łȂ��ꍇ
			else
			{
				throw new System.Exception("�s���Ȍ^�ł�");
			}
		}

		/// <summary> �w�肵����ԂɑJ�ڂ��� </summary>
		public void TransitionState<State>() where State : T
		{
			TransitionState(typeof(State));
		}

		/// <summary> ������ԂɑJ�ڂ��� </summary>
		public void TransitionToFirstState()
		{
			TransitionState(firstState.GetType());
		}

		/// <summary> ���݂̏�Ԃ̎��ɓo�^���ꂽ��ԂɑJ�ڂ��� </summary>
		public void TransitionToNextState()
		{
			var currentStateIndex = States.IndexOf(CurrentState);

			if (currentStateIndex < States.Count - 1)
			{
				TransitionState(States[currentStateIndex + 1].GetType());
			}
			else
			{
				throw new System.Exception("����State�����݂��܂���");
			}
		}

		/// <summary> ���݂̏�Ԃ̑O�ɓo�^���ꂽ��ԂɑJ�ڂ��� </summary>
		public void TransitionToPreviousState()
		{
			var currentStateIndex = States.IndexOf(CurrentState);

			if (currentStateIndex > 0)
			{
				TransitionState(States[currentStateIndex - 1].GetType());
			}
			else
			{
				throw new System.Exception("�O��State�����݂��܂���");
			}
		}
	}
}
