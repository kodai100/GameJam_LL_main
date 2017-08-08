﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class HitDetection : MonoBehaviour {

    #region SerializedFiled variables
    [SerializeField] MyCameraControll myCameraControll;
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
                MainManager.Instance.Lose();
                transform.root.gameObject.SetActive(false);
            }
        }

    }

    // 敵を食べたときのプロセス
    void Combine(GameObject enemy) {

        // メッシュの色を食べた敵の色と同じにする処理
        transform.Find(ConstantParam.PLAYER_PARENT_MESH_NAME)
            .Find(mPlayerParam.checkMeshID().ToString())
            .gameObject.GetComponent<Renderer>().material.SetColor
            ("_Color", enemy.GetComponent<Renderer>().material.GetColor("_Color"));

        Destroy(enemy);

        SeManager.Instance.Play("PlayerEat2");

        // 進化できるときにifの中に入る
        if (mPlayerParam.eatEnemy(enemy.transform.localScale.x)) {

            // パラメータを更新
            mPlayerParam.transformation(mPlayerParam.checkMeshID() + 1);

            // カメラが引く処理
            float meshScale = transform.Find(ConstantParam.PLAYER_PARENT_MESH_NAME).Find(mPlayerParam.checkMeshID().ToString()).localScale.x;
            myCameraControll.moveCameraFromDefault(meshScale * 0.5f, -meshScale);

        }

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        
    }
    #endregion

}
