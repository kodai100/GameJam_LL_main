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
    float speed = 3f;
    Camera playerCamera;
	bool forward = false;

    PlayerParametter mPlayerParam;
    #endregion

    #region private methods
    void Start() {
		playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mPlayerParam = GetComponent<PlayerParametter>() ; 
    }
    
    void Update() {

		// 舌を伸ばす位置を計算
		Vector3 dir = playerCamera.ScreenPointToRay (new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f)).direction;
		float distance = mPlayerParam.checkMaxArmDistance ();
		arm.transform.LookAt (transform.position + dir * distance);

		// 伸ばす想定の位置にガイドをつける
		Ray ray = new Ray (arm.transform.position, dir);
		Vector3 estimatedPoint;
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo, distance)) {
			estimatedPoint = hitInfo.point;
		}
		else {
			estimatedPoint = ray.GetPoint (distance);
		}
		guideArm.transform.position = estimatedPoint;
		guideArm.transform.rotation = arm.transform.rotation;

		// 舌を伸ばす処理
		if (!isShooting && Input.GetMouseButtonDown (0)) {
				StartCoroutine (shootAnimation (dir, distance));
		}
    }
    #endregion

	IEnumerator shootAnimation(Vector3 dir, float distance) {
		
        SeManager.Instance.Play("Tongue");

		Vector3 defaultLocalScale = arm.transform.localScale;
		float currentLength 	= 0f;
		isShooting 	= true;
        forward 	= true;
		guideArm.SetActive (false);

		// 伸びる
        while (currentLength < distance && forward) {
			arm.transform.localScale 	= defaultLocalScale + Vector3.forward * currentLength;
			arm.transform.localPosition = currentLength * dir * 0.5f;

            currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
            yield return null;
        }
			
        forward = false;
        
		// 縮む
		while (currentLength > 0 && !forward) {
			arm.transform.localScale 	= defaultLocalScale + Vector3.forward * currentLength;
			arm.transform.localPosition = currentLength * dir * 0.5f;

            currentLength -= speed * 0.3f;
            yield return null;
        }

        // 終了処理
        currentLength = 0f;
		arm.transform.localPosition = Vector3.zero;
		arm.transform.localScale 	= defaultLocalScale;

        isShooting = false;
		guideArm.SetActive (true);

        yield break;
    }
 
	// Armを伸ばしていた場合、引っ込める
	public void returnArm(){
		forward = false;
	}

}
