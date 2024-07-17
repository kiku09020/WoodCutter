using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 幹の生成 </summary>
public class StemGenerator : MonoBehaviour
{
	/* Fields */
	[SerializeField] Stem stemPrefab;
	[SerializeField] float startPosY = -4;
	[SerializeField] int startStemCount = 5;

	float currentTreePosY;

	//-------------------------------------------------------------------
	/* Properties */

	//-------------------------------------------------------------------
	/* Events */
	void Awake()
	{
		currentTreePosY = startPosY;

		// 初期生成
		for (int i = 0; i < startStemCount; i++)
		{
			GenerateStem();
		}
	}

	void FixedUpdate()
	{

	}

	//-------------------------------------------------------------------
	/* Methods */
	public Stem GenerateStem()
	{
		// 生成
		var stem = Instantiate(stemPrefab, new Vector3(0, currentTreePosY, 0), Quaternion.identity, transform);

		// 木の高さを更新
		currentTreePosY += stem.transform.localScale.y;

		return stem;
	}

}
