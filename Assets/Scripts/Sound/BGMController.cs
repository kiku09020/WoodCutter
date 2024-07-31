namespace Game.Sound
{
	public class BGMController : AudioController<BGMController>
	{
		protected override void PlayAudio(AudioData audioData)
		{
			source.clip = audioData.Clip;
			source.Play();
		}
	}
}
