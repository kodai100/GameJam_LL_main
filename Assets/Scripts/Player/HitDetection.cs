using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class HitDetection : MonoBehaviour {


    #region SerializeField variables
    [SerializeField] GameObject meshes;
    #endregion

    #region private variables
    PlayerParametter mPlayerParam;
    #endregion

    #region priavte methods

    void Start() {
        mPlayerParam = GetComponent<PlayerParametter>();
    }

    void OnCollisionEnter(Collision other) {

        if (StaticManager.isWin) return;
        
        // 敵に当たったら
        if (other.gameObject.tag == "Enemy") {

            // 吸収
            if (mPlayerParam.checkWin(other.gameObject)) {
                Combine(other.gameObject);
                Debug.Log("Killed. total : " + mPlayerParam.checkEnemyCount());
            }
            else {
                SeManager.Instance.Play("EnemyEat");
                transform.root.gameObject.SetActive(false);
                MainManager.Instance.Lose();
            }
        }

    }

    // 敵を食べたときのプロセス
    void Combine(GameObject enemy) {

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        meshes.GetComponentInChildren<Renderer>().material.SetColor("_Color", enemy.GetComponent<Renderer>().material.GetColor("_Color"));

        Destroy(enemy);

        SeManager.Instance.Play("PlayerEat2");

        // 進化できるときにifの中に入る
        if (mPlayerParam.eatEnemy(enemy.transform.localScale.x)) {

            meshes.transform.Find(mPlayerParam.checkMeshIDName()).gameObject.SetActive(false);

            // パラメータを更新し、それに合わせてPlayerの性質を変化
            mPlayerParam.evolved();

            try {
                meshes.transform.Find(mPlayerParam.checkMeshIDName()).gameObject.SetActive(true);
            }
            catch (NullReferenceException) {
                Debug.Log("Can not find evolved object.");
                return;
            }

            GetComponent<SphereCollider>().radius = mPlayerParam.checkRadius();
        }
        
        /*
        GameObject next;
        try {
            next = transform.parent.parent.Find((mId + 1).ToString()).gameObject;
        }
        catch (NullReferenceException) {
            Debug.Log("Can not find evolved object.");
            return;
        }

        // カメラを引く
        float scaleDiff = next.gameObject.transform.localScale.x - transform.localScale.x;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 dirZoomOut = Vector3.Normalize(camera.transform.position - transform.position);
        camera.transform.localPosition -= dirZoomOut * scaleDiff;

        next.SetActive(true);
        next.GetComponentInChildren<Rigidbody>().transform.position = transform.position;
        //next.GetComponentInChildren<CombineProcess>().SetId(mId + 1);

        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
        */

    }
    #endregion

}
