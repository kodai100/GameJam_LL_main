using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControl : MonoBehaviour {

	Transform mBodyTransform;
	Shoot mShoot;

	void Awake(){
		mBodyTransform = transform.parent.GetComponentInChildren<Rigidbody> ().transform;
		//mShoot = transform.parent.GetComponentInChildren<Shoot> ();
	}

	// Bodyを参照して場所を修正
	void LateUpdate(){
		transform.position = mBodyTransform.position;
	}

}
