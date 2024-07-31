using System.Collections;
using System.Collections.Generic;
using Template.DesignPatterns.Singleton;
using UnityEngine;

// FIXME: 複数シーン対応

namespace Game.Sound
{
	[RequireComponent(typeof(AudioSource))]
	public abstract class AudioController<T> : MonoBehaviour where T : AudioController<T>
	{
		/* Fields */
		[SerializeField] protected AudioDataList audioDataList;

		protected AudioSource source;

		static List<AudioController<T>> audioControllers = new List<AudioController<T>>();

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			// AudioDataListがnullの場合は例外を投げる
			if (audioDataList == null)
			{
				throw new System.NullReferenceException("AudioDataList is null.");
			}

			// Listに追加
			if (audioControllers.Contains(this))
			{
				Destroy(gameObject);
				return;
			}
			audioControllers.Add(this);

			// AudioSourceの取得
			source = GetComponent<AudioSource>();
		}

		void OnDestroy()
		{
			audioControllers.Remove(this);
		}

		//-------------------------------------------------------------------
		/* Playing */
		/// <summary> 指定した名前の音声を再生する </summary>
		public T PlayAudio(string audioName, bool isResetParams = true)
		{
			// 指定された名前のDataを取得して再生
			if (audioDataList.DataDict.TryGetValue(audioName, out var audioData))
			{
				PlayAudio(audioData, isResetParams);
				return this as T;
			}

			throw new System.ArgumentException("AudioData not found.");
		}

		/// <summary> 音声再生処理 </summary>
		protected void PlayAudio(AudioData audioData, bool isResetParams = true)
		{
			// パラメータリセット
			if (isResetParams)
			{
				ResetSourceParameters();
			}

			if (audioData.IsNullOrEmpty)
			{
				throw new System.ArgumentException("AudioData is null or empty.");
			}

			// 再生処理
			PlayAudio(audioData);
		}

		/// <summary> 再生処理抽象メソッド </summary>
		protected abstract void PlayAudio(AudioData audioData);

		//------------------------------------------------------------

		public void PauseAudio()
		{
			source.Pause();
		}

		public void StopAudio()
		{
			source.Stop();
		}

		public void UnpauseAudio()
		{
			source.UnPause();
		}

		public void MuteAudio()
		{
			source.mute = true;
		}

		public void UnmuteAudio()
		{
			source.mute = false;
		}

		//------------------------------------------------------------
		/*----- Parameters -----*/

		/// <summary> パラメータリセット </summary>
		public virtual void ResetSourceParameters()
		{
			source.volume = 1;
			source.pitch = 1;
			source.loop = false;
		}

		public T SetAudioVolume(float volume)
		{
			source.volume = volume;
			return this as T;
		}

		public T SetAudioPitch(float pitch)
		{
			source.pitch = pitch;
			return this as T;
		}

		//------------------------------------------------------------

		/*----- Static Methods -----*/

		static void AllCommon(System.Action<AudioController<T>> action)
		{
			foreach (var controller in audioControllers)
			{
				action?.Invoke(controller);
			}
		}

		public static void PauseAllAudio()
		{
			AllCommon(controller => controller.PauseAudio());
		}

		public static void StopAllAudio()
		{
			AllCommon(controller => controller.StopAudio());
		}

		public static void UnpauseAllAudio()
		{
			AllCommon(controller => controller.UnpauseAudio());
		}

		public static void MuteAllAudio()
		{
			AllCommon(controller => controller.MuteAudio());
		}

		public static void UnmuteAllAudio()
		{
			AllCommon(controller => controller.UnmuteAudio());
		}
	}
}
