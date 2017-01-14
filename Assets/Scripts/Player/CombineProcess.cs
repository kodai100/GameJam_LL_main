using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombineProcess : MonoBehaviour{

	[SerializeField] int mRequiredNum;	// 大きくなるために必要な敵の数

	int mCnt = 0;
	int mId  = 1;

	public void Combine(){
		
		mCnt++;
		if (mCnt < mRequiredNum) {
			return;
		}

		Debug.Log ("Evolution!!");

		GameObject next;
		try{
			next = transform.parent.FindChild ((mId + 1).ToString ()).gameObject;
		}
		catch(NullReferenceException e){
			Debug.Log("Evolution limit");
			return;
		}

		// カメラを引く
		GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localPosition -= new Vector3(0f, 0f, 1f);

		next.SetActive (true);
		next.GetComponent<CombineProcess> ().SetId (mId + 1);
		gameObject.SetActive (false);

	}

	public void SetId(int id){ mId = id;}

}
