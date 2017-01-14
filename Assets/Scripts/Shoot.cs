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

            // レイの交差判定
            // レイが当たったオブジェクトを取得し、自分のポジションから角度を算出、その方向に射出
            if (!isShooting) {
                isShooting = true;

                RaycastHit hit;
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    Debug.Log("Hit");

                    GameObject enemy = hit.collider.gameObject;

                    dir = Vector3.Normalize(enemy.transform.position - transform.position);
                    distance = Vector3.Distance(enemy.transform.position, transform.position);

                    StartCoroutine(shootAnimation(enemy));

                }
            }

        }
    }

    IEnumerator shootAnimation(GameObject enemy) {
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
