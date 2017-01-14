using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public Camera playerCamera;
    public float speed = 0.1f;

    private Vector3 dir;
    private float distance;
    float currentLength = 0f;

    bool isShooting;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {

            if (!isShooting) {

                RaycastHit hit;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    
                    if (hit.collider.gameObject.tag == "Enemy") {
                        Debug.Log("Enemy Hit");

                        isShooting = true;
                        GameObject enemy = hit.collider.gameObject;

                        dir = Vector3.Normalize(enemy.transform.position - transform.position);
                        distance = Vector3.Distance(enemy.transform.position, transform.position);

                        StartCoroutine(shootAnimationAndDestroy(enemy));
                    }

                } else {
                    StartCoroutine(shootAnimation(Input.mousePosition));
                }
            }

        }
    }

    IEnumerator shootAnimation(Vector3 position) {
        currentLength = 0f;
        bool forward = true;

        while (currentLength < 2f && forward) {
            currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
            yield return null;
        }

        forward = false;

        while (currentLength > 0 && !forward) {
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
            currentLength += Mathf.Max(speed * (1 - currentLength / distance), 0.1f);
            yield return null;
        }

        forward = false;

        while(currentLength > 0 && !forward) {
            currentLength -= speed * 0.3f;
            enemy.transform.position = transform.position + currentLength * dir;
            yield return null;
        }

        currentLength = 0f;
        isShooting = false;

        Destroy(enemy);

        yield break;
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + currentLength * dir);
    }
}
