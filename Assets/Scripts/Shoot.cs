using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    #region public
    public Camera playerCamera;
    public float speed = 0.1f;
    public GameObject arm;
    public float maxArmDistance = 10f;
    public bool isDebug = false;
    #endregion public

    #region private
    Vector3 dir;
    float distance;
    float currentLength = 0f;
    bool isShooting;
    #endregion private

    #region MonoBehaviourFuncs
    void Start() {

    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {

            if (!isShooting) {

                RaycastHit hit;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    
                    if (hit.collider.gameObject.tag == "Enemy") {
                        Debug.Log("Enemy Hit");

                        GameObject enemy = hit.collider.gameObject;

                        dir = Vector3.Normalize(enemy.transform.position - transform.position);
                        distance = Vector3.Distance(enemy.transform.position, transform.position);

                        if(distance < maxArmDistance) {
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
            arm.transform.position = transform.position + currentLength * dir * 0.5f;
            currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
            yield return null;
        }

        forward = false;

        while (currentLength > 0 && !forward) {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.position = transform.position + currentLength * dir * 0.5f;
            currentLength -= speed * 0.3f;
            yield return null;
        }

        currentLength = 0f;
        isShooting = false;

        yield break;
    }

    IEnumerator shootAnimationAndDestroy(GameObject enemy) {
        isShooting = true;
        currentLength = 0f;
        bool forward = true;

        while(currentLength < distance && forward) {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.position = transform.position + currentLength * dir * 0.5f;
            currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
            yield return null;
        }

        forward = false;

        while(currentLength > 0 && !forward) {
            enemy.transform.position = transform.position + currentLength * dir;
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.position = transform.position + currentLength * dir * 0.5f;
            currentLength -= speed * 0.3f;
            yield return null;
        }

        currentLength = 0f;
        isShooting = false;

        Destroy(enemy);

        StaticManager.enemyCount++;
        Debug.Log("Killed. total : " + StaticManager.enemyCount);

        yield break;
    }

    
}
