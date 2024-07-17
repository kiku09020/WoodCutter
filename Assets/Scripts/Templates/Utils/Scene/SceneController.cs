using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Template.Utils.SceneManagement
{
	/// <summary> シーン管理汎用クラス </summary>
	public class SceneManagerUtils
	{
		//------------------------------------------------------------
		/* Properties */
		/// <summary> 現在のシーン番号 </summary>
		public static int ActiveSceneIndex => ActiveScene.buildIndex;

		static Scene activeScene;
		static Scene ActiveScene
		{
			get
			{
				if (activeScene == null || activeScene.buildIndex < 0)
				{
					activeScene = SceneManager.GetActiveScene();
				}
				return activeScene;
			}
		}

		static int nextSceneIndex => ActiveSceneIndex + 1;
		static int prevSceneIndex => ActiveSceneIndex - 1;

		//------------------------------------------------------------
		/* Events */
		/// <summary> シーン変更時のイベントを追加する </summary>
		/// <param name="action">型引数：(現在のシーン、次のシーン)</param>
		public static void AddSceneChangedEvent(UnityAction<Scene, Scene> action)
		{ SceneManager.activeSceneChanged += action; }

		/// <summary> シーン変更時のイベントを削除する </summary>
		public static void RemoveSceneChangedEvent(UnityAction<Scene, Scene> action)
		{ SceneManager.activeSceneChanged -= action; }

		//------------------------------------------------------------
		/* Checking Methods */
		// カスタムエラーハンドリング
		static System.Exception CustomException()
		{
			return new System.Exception("シーンが読み込めませんでした");
		}

		// シーン番号判定
		static bool IsLoadable(int index)
		{
			if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
			{
				return true;
			}

			throw CustomException();
		}

		// シーン名判定
		static bool IsLoadable(string name)
		{
			for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
			{
				var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
				var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

				if (name == sceneName)
				{
					return true;
				}
			}

			throw CustomException();
		}

		//------------------------------------------------------------
		/* Loading Methods */
		/// <summary> シーン番号を指定してシーンを読み込む </summary>
		public static void LoadScene(int index, LoadSceneMode mode = LoadSceneMode.Single)
		{
			if (IsLoadable(index))
			{
				SceneManager.LoadScene(index, mode);
			}
		}

		/// <summary> シーン名を指定してシーンを読み込む </summary>
		public static void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single)
		{
			if (IsLoadable(name))
			{
				SceneManager.LoadScene(name, mode);
			}
		}

		/// <summary> 現在のシーンを再読み込みする </summary>
		public static void ReloadScene(LoadSceneMode loadSceneMode = LoadSceneMode.Single) => LoadScene(ActiveSceneIndex, loadSceneMode);

		/// <summary> 次のシーンを読み込む </summary>
		public static void LoadNextScene(LoadSceneMode loadSceneMode = LoadSceneMode.Single) => LoadScene(nextSceneIndex, loadSceneMode);

		/// <summary> 前のシーンを読み込む </summary>
		public static void LoadPrevScene(LoadSceneMode loadSceneMode = LoadSceneMode.Single) => LoadScene(prevSceneIndex, loadSceneMode);

		//------------------------------------------------------------
		/* Async Loading Methods */
		/// <summary> シーン番号を指定して非同期でシーンを読み込む </summary>
		public static AsyncOperation LoadSceneAsync(int index, LoadSceneMode mode = LoadSceneMode.Single)
		{
			if (IsLoadable(index))
			{
				return SceneManager.LoadSceneAsync(index, mode);
			}

			return null;
		}

		/// <summary> シーン名を指定して非同期でシーンを読み込む </summary>
		public static AsyncOperation LoadSceneAsync(string name, LoadSceneMode mode = LoadSceneMode.Single)
		{
			if (IsLoadable(name))
			{
				return SceneManager.LoadSceneAsync(name, mode);
			}

			return null;
		}

		/// <summary> 現在のシーンを非同期で再読み込みする </summary>
		public static AsyncOperation ReloadSceneAsync(LoadSceneMode loadSceneMode = LoadSceneMode.Single) => LoadSceneAsync(ActiveSceneIndex, loadSceneMode);

		/// <summary> 次のシーンを非同期で読み込む </summary>
		public static AsyncOperation LoadNextSceneAsync(LoadSceneMode loadSceneMode = LoadSceneMode.Single) => LoadSceneAsync(nextSceneIndex, loadSceneMode);

		/// <summary> 前のシーンを非同期で読み込む </summary>
		public static AsyncOperation LoadPrevSceneAsync(LoadSceneMode loadSceneMode = LoadSceneMode.Single) => LoadSceneAsync(prevSceneIndex, loadSceneMode);

		//------------------------------------------------------------

		public static void LoadAndUnloadAdditiveScene(int prevSceneIndex, int nextSceneIndex)
		{
			LoadScene(nextSceneIndex, LoadSceneMode.Additive);
			SceneManager.UnloadSceneAsync(prevSceneIndex);
		}

		public static void LoadAndUnloadAdditiveScene(string prevSceneName, string nextSceneName)
		{
			LoadScene(nextSceneName, LoadSceneMode.Additive);
			SceneManager.UnloadSceneAsync(prevSceneName);
		}

		//------------------------------------------------------------

		public static void SetActiveObjectsInCurrentScene(int index, bool activate)
		{
			// 現在のシーンのインデックスを取得
			var activeSceneIndex = ActiveSceneIndex;

			// 指定のシーンをアクティブにする
			var currentScene = SceneManager.GetSceneByBuildIndex(index);
			SceneManager.SetActiveScene(currentScene);

			// シーン内のオブジェクトをアクティブにする
			var rootObjects = currentScene.GetRootGameObjects();
			foreach (var rootObject in rootObjects)
			{
				rootObject.SetActive(activate);
			}

			// 元のシーンをアクティブにする
			var activeScene = SceneManager.GetSceneByBuildIndex(activeSceneIndex);
			SceneManager.SetActiveScene(activeScene);
		}
	}
}
