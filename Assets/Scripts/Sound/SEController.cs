using System.Linq;
using UnityEngine;

namespace Game.Sound
{
	public class SEController : AudioController<SEController>
	{
		protected override void PlayAudio(AudioData audioData)
		{
			source.clip = audioData.Clip;

			// ループが有効な場合、ループ再生
			if (source.loop)
			{
				source.Play();
			}

			// ループが無効な場合、OneShot再生
			else
			{
				source.PlayOneShot(audioData.Clip);
			}
		}

		//------------------------------------------------------------

		/// <summary> ランダムな音声データを取得 </summary>
		AudioData GetRandomAudioData()
		{
			return GetRandomAudioData(0, audioDataList.DataDict.Count);
		}

		/// <summary> 指定した範囲のランダムな音声データを取得 </summary>
		AudioData GetRandomAudioData(int min, int max)
		{
			var dataList = audioDataList.DataDict.Values.ToList();
			var randomIndex = Random.Range(min, max);
			return dataList[randomIndex];
		}

		/// <summary> ランダムな音声を再生する </summary>
		public void PlayRandomAudio(bool isResetParams = false)
		{
			PlayAudio(GetRandomAudioData(), isResetParams);
		}

		/// <summary> 指定した範囲のランダムな音声を再生する </summary>
		public void PlayRandomAudio(int min, int max, bool isResetParams = false)
		{
			PlayAudio(GetRandomAudioData(min, max), isResetParams);
		}

		//------------------------------------------------------------
		/// <summary> ランダムなピッチを設定する </summary>
		public SEController SetRandomPitch(float min = .5f, float max = 1.5f)
		{
			source.pitch = Random.Range(min, max);
			return this;
		}
	}
}
