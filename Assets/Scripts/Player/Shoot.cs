using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    #region public
    public float speed = 0.1f;
    public GameObject arm;
    public float maxArmDistance = 10f;
    [HideInInspector] public bool isShooting;
    public bool isDebug = false;
    #endregion public

    #region private
    Camera playerCamera;

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

				/*
				// カメラから画面の中心の空間に対してRayを飛ばす
                RaycastHit hit;
                Ray ray = playerCamera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));

                if (Physics.Raycast(ray, out hit)) {
                    
                    if (hit.collider.gameObject.tag == "Enemy") {
                        Debug.Log("Enemy Hit");

                        GameObject enemy = hit.collider.gameObject;

                        dir = Vector3.Normalize(enemy.transform.position - transform.position);
                        distance = Vector3.Distance(enemy.transform.position, transform.position);

                        if(distance < maxArmDistance) {
                            nextPlayerColor = enemy.GetComponent<Renderer>().material.GetColor("_Color");
                            arm.transform.LookAt(enemy.transform.position);
                            StartCoroutine(shootAnimationAndDestroy(enemy));
                        }
                        
					} else if(hit.collider.gameObject.tag != "Player"){
                        dir = Vector3.Normalize(hit.point - transform.position);
                        distance = Vector3.Distance(hit.point, transform.position);

                        if (distance < maxArmDistance) {
                            arm.transform.LookAt(hit.point);
                            StartCoroutine(shootAnimation());
                        }
                        
                    }

                } 
				else { // Rayがヒットしない場合でも空間に対して舌を伸ばすように処理
                    dir = Vector3.Normalize(playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                    distance = Vector3.Distance(playerCamera.ScreenToWorldPoint(Input.mousePosition), transform.position);
                    arm.transform.LookAt(playerCamera.ScreenToWorldPoint(Input.mousePosition));
                    StartCoroutine(shootAnimation());
                }
				*/
            }

        }
    }

	/*
    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + currentLength * dir);
        Gizmos.DrawWireSphere(transform.position, maxArmDistance);
    }

    void OnGUI() {
        if (isDebug) {
            GUILayout.Label("Defeated Enemies : " + StaticManager.enemyCount);
        }

    }
	*/

    #endregion MonoBehaviourFuncs

	IEnumerator shootAnimation(Vector3 dir, float distance) {

        SeManager.Instance.Play("Tongue");

		Vector3 defaultLocalScale = arm.transform.localScale;
        isShooting 		= true;
        float currentLength 	= 0f;
        bool forward 	= true;
        
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
    
}
