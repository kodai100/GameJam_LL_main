using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public Camera playerCamera;
    public float speed = 0.1f;

    public GameObject arm;

    Vector3 dir;
    float distance;
    float currentLength = 0f;
    bool isShooting;
    
    void Start() {

    }
    
    void Update() {
        if (Input.GetMouseButtonDown(0)) {

            if (!isShooting) {
                isShooting = true;

                RaycastHit hit;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    
                    if (hit.collider.gameObject.tag == "Enemy") {
                        Debug.Log("Enemy Hit");

                        GameObject enemy = hit.collider.gameObject;

                        dir = Vector3.Normalize(enemy.transform.position - transform.position);
                        distance = Vector3.Distance(enemy.transform.position, transform.position);

                        arm.transform.LookAt(enemy.transform.position);

                        StartCoroutine(shootAnimationAndDestroy(enemy));
                    } else {
                        dir = Vector3.Normalize(hit.point - transform.position);
                        distance = Vector3.Distance(hit.point, transform.position);
                        arm.transform.LookAt(hit.point);
                        StartCoroutine(shootAnimation());
                    }

                } else {
                    dir = Vector3.Normalize(playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                    distance = Vector3.Distance(playerCamera.ScreenToWorldPoint(Input.mousePosition), transform.position);
                    arm.transform.LookAt(playerCamera.ScreenToWorldPoint(Input.mousePosition));
                    StartCoroutine(shootAnimation());
                }
            }

        }
    }

    IEnumerator shootAnimation() {
        currentLength = 0f;
        bool forward = true;

        while (currentLength < 2f && forward) {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y, currentLength);
            arm.transform.position = transform.position + currentLength * dir * 0.5f;
            currentLength += Mathf.Max(speed * (1 - currentLength / 2f), 0.1f);
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

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + currentLength * dir);
    }
}
