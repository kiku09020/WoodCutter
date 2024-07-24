using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
	/// <summary> 解像度に合わせて、カメラサイズを調整する </summary>
	[ExecuteAlways, RequireComponent(typeof(Camera))]
	public class CameraSizeFitter : MonoBehaviour
	{
		/* Fields */
		[SerializeField] Vector2 referenceResolution = new Vector2(1080, 1920);
		[SerializeField] float referenceCameraSize = 5f;

		Camera resizedCamera;

		float prevOrthographicSize;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			resizedCamera = GetComponent<Camera>();
		}

		void Update()
		{
			ResizeCamera();
		}

		void OnEnable()
		{
			ResizeCamera();
		}

		void OnDisable()
		{
			if (resizedCamera == null) return;

			resizedCamera.orthographicSize = referenceCameraSize;
		}

		//-------------------------------------------------------------------
		/* Methods */
		void ResizeCamera()
		{
			if (resizedCamera == null) return;
			if (prevOrthographicSize == resizedCamera.orthographicSize) return;
			if (UnityEngine.Device.SystemInfo.deviceType != DeviceType.Handheld) return;

			var currentResolution = new Vector2(Screen.width, Screen.height);
			var ratio = currentResolution.x / currentResolution.y;
			var referenceRatio = referenceResolution.x / referenceResolution.y;

			if (ratio >= referenceRatio)
			{
				resizedCamera.orthographicSize = referenceCameraSize;
			}
			else
			{
				var cameraSize = referenceCameraSize * referenceRatio / ratio;
				resizedCamera.orthographicSize = cameraSize;
			}

			prevOrthographicSize = resizedCamera.orthographicSize;
		}
	}
}
