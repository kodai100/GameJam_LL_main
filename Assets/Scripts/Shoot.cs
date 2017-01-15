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
    Vector3 dir;
    float distance;
    float currentLength = 0f;
    Material playerMaterial;

    CombineProcess combineProcess;
    Color nextPlayerColor;
    #endregion private

    #region MonoBehaviourFuncs
    void Start() {
        combineProcess = gameObject.GetComponent<CombineProcess>();
        playerMaterial = GetComponent<Renderer>().material;
		playerCamera   = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    
    void Update() {

        arm.transform.localPosition = transform.localPosition;

        if (Input.GetMouseButtonDown(0)) {

            if (!isShooting) {

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
                        
                    } else {
                        dir = Vector3.Normalize(hit.point - transform.position);
                        distance = Vector3.Distance(hit.point, transform.position);

                        if (distance < maxArmDistance) {
                            arm.transform.LookAt(hit.point);
                            StartCoroutine(shootAnimation());
                        }
                        
                    }

                } else {
                    // dir = Vector3.Normalize(playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                    // distance = Vector3.Distance(playerCamera.ScreenToWorldPoint(Input.mousePosition), transform.position);
                    // arm.transform.LookAt(playerCamera.ScreenToWorldPoint(Input.mousePosition));
                    // StartCoroutine(shootAnimation());
                }
            }

        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + currentLength * dir);
        Gizmos.DrawWireSphere(transform.position, maxArmDistance);
    }

    void OnGUI() {
        if (isDebug) {
            GUILayout.Label("Defeated Enemies : " + StaticManager.enemyCount);
        }

    }
    #endregion MonoBehaviourFuncs

    IEnumerator shootAnimation() {
        isShooting = true;
        currentLength = 0f;
        bool forward = true;
        
        while (currentLength < distance && forward) {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.localPosition = transform.localPosition + currentLength * dir * 0.5f;
            currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
            yield return null;
        }

        forward = false;

        while (currentLength > 0 && !forward) {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.localPosition = transform.localPosition + currentLength * dir * 0.5f;
            currentLength -= speed * 0.3f;
            yield return null;
        }

        
        currentLength = 0f;
        arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);

        isShooting = false;

        yield break;
    }

    IEnumerator shootAnimationAndDestroy(GameObject enemy) {
        isShooting = true;
        currentLength = 0f;
        bool forward = true;
		bool canEat = enemy.transform.localScale.x < StaticManager.playerScale;

        
        while (currentLength < distance && forward) {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.localPosition = transform.localPosition + currentLength * dir * 0.5f;
            currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
            yield return null;
        }

        forward = false;

        while(currentLength > 0 && !forward) {
			if (canEat)
			{
				enemy.transform.position = transform.position + currentLength * dir;
			}

            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.localPosition = transform.localPosition + currentLength * dir * 0.5f;
            currentLength -= speed * 0.3f;
            yield return null;
        }

        currentLength = 0f;
        arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);

        isShooting = false;

		if (canEat)
		{
			playerMaterial.SetColor("_Color", nextPlayerColor);

			Destroy(enemy);

			StaticManager.enemyCount++;

			combineProcess.Combine();

			Debug.Log("Killed. total : " + StaticManager.enemyCount);
		}

        yield break;
    }

    
}
