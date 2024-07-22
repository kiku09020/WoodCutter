using Template.Utils.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
	/// <summary> リトライ操作クラス </summary>
	public class RetryController : MonoBehaviour
	{
		public async void Retry()
		{
			await SceneManagerUtils.ReloadSceneAsync();
		}
	}
}
