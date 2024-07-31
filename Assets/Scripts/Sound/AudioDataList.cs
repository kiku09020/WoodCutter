using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// FIXME: Addressablesなどで動的に追加する場合は、AudioDataListのラッパークラスからAudioData追加する

namespace Game.Sound
{
	[CreateAssetMenu(menuName = "AudioDataList", fileName = "AudioDataList")]
	public class AudioDataList : ScriptableObject
	{
		[Header("Settings")]
		[SerializeField] bool autoSetName;

		[Header("Data")]
		[SerializeField] List<AudioData> dataList = new List<AudioData>();

		public IReadOnlyDictionary<string, AudioData> DataDict { get; private set; }

		//------------------------------------------------------------

		void OnEnable()
		{
			DataDict = dataList.ToDictionary(data => data.Name);
		}

		void OnValidate()
		{
			if (!autoSetName) return;

			foreach (var data in dataList)
			{
				data.SetName();
			}
		}
	}

	[System.Serializable]
	public class AudioData
	{
		[SerializeField] string name;
		[SerializeField] AudioClip clip;

		public string Name => name;
		public AudioClip Clip => clip;

		public bool IsNullOrEmpty => string.IsNullOrEmpty(name) || clip == null;

		public void SetName()
		{
			name = clip.name;
		}
	}
}
