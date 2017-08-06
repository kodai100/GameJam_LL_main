using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    #region public
    public float speed = 0.1f;
    public GameObject arm;
    public float maxArmDistance = 10f;
    [HideInInspector] public bool isShooting;
    #endregion public

    #region private
    Camera playerCamera;
	bool forward = false;

    Material playerMaterial;

    CombineProcess combineProcess;
    Color nextPlayerColor;
    #endregion private

    #region MonoBehaviourFuncs
    void Start() {
        combineProcess = gameObject.GetComponent<CombineProcess>();
        playerMaterial = GetComponentInChildren<Renderer>().material;
		playerCamera   = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    
    void Update() {

        if (Input.GetMouseButtonDown(0)) {

			// 舌を伸ばしていなければ
            if (!isShooting) {

				// カメラの中心空間に対して舌を伸ばす
				Vector3 dir = playerCamera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f)).direction;
				float distance = maxArmDistance;
				arm.transform.LookAt (transform.position + dir * distance);
				StartCoroutine(shootAnimation (dir, distance));
            }

        }
    }

    #endregion MonoBehaviourFuncs

	IEnumerator shootAnimation(Vector3 dir, float distance) {

        SeManager.Instance.Play("Tongue");

		Vector3 defaultLocalScale = arm.transform.localScale;
		float currentLength 	= 0f;
		isShooting 	= true;
        forward 	= true;
        
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

        yield break;
    }
 
	// Armを伸ばしていた場合、引っ込める
	public void returnArm(){
		forward = false;
	}

}
