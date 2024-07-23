using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager
{
	/* Fields */

	//-------------------------------------------------------------------
	/* Properties */

	//-------------------------------------------------------------------
	/* Events */

	//-------------------------------------------------------------------
	/* Methods */
	public void SetupApp()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}
}
