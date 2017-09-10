using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	#region public variables
	public GameObject arm;
	[SerializeField] GameObject guideArm;
	[HideInInspector] public bool isShooting;
	#endregion

	#region private variables
	Camera playerCamera;
	PlayerParametter mPlayerParam;
	float speed     = 3f;
	bool forward    = false;
	#endregion

	#region private methods
	void Start() {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		mPlayerParam = GetComponent<PlayerParametter>() ; 
	}

	void Update() {
		


		// 舌を伸ばす位置を計算
		Vector3 dir 		= playerCamera.ScreenPointToRay (new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f)).direction;
		float distance 		= mPlayerParam.checkMaxArmDistance ();
		Vector3 distPoint 	= transform.position + dir * distance;
		arm.transform.LookAt (distPoint);

		// おおよその着弾点をガイドで示す
		RaycastHit hitInfo;
		if (Physics.Raycast (transform.position, dir, out hitInfo, distance)) {
			guideArm.transform.position = hitInfo.point;
		}
		else {
			guideArm.transform.position = distPoint;
		}
		guideArm.transform.rotation = arm.transform.rotation;

		// 舌を伸ばす処理
		if (!isShooting && Input.GetMouseButtonDown (0)) {
			StartCoroutine (shootAnimation (distPoint, distance));
		}
	}

	/// <summary>
	/// 舌を伸ばすアニメーションの処理
	/// </summary>
	/// <param name="pos">舌を伸ばす先</param>
	/// <param name="distance">舌を伸ばせる距離</param>
	IEnumerator shootAnimation(Vector3 pos, float distance) {

		SeManager.Instance.Play("Tongue");

		Vector3 defaultLocalScale = arm.transform.localScale;
		float currentLength = 0f;
		isShooting  = true;
		forward     = true;
		guideArm.SetActive (false);

		// 伸びる
		while (currentLength < distance && forward) {
			arm.transform.LookAt (pos);
			arm.transform.localScale    = defaultLocalScale + Vector3.forward * currentLength;
			arm.transform.localPosition = currentLength * (pos - transform.position).normalized * 0.5f;

			currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
			yield return null;
		}

		forward = false;

		// 縮む
		while (currentLength > 0 && !forward) {
			arm.transform.LookAt (pos);
			arm.transform.localScale    = defaultLocalScale + Vector3.forward * currentLength;
			arm.transform.localPosition = currentLength * (pos - transform.position).normalized * 0.5f;

			currentLength -= speed * 0.3f;
			yield return null;
		}

		// 終了処理
		currentLength = 0f;
		arm.transform.localPosition = Vector3.zero;
		arm.transform.localScale    = defaultLocalScale;
		isShooting = false;
		guideArm.SetActive (true);

		yield break;
	}
	#endregion

	// Armを伸ばしていた場合、引っ込める
	public void returnArm(){
		forward = false;
	}

}
