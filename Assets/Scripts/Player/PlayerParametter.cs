// プレイヤーのパラメータを持つだけのクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParametter : MonoBehaviour {

    // weightの値はscaleが変わったときのそのパラメータへの影響力を表す
    #region  SerializeField variables
    [SerializeField] float initJumpPower                  = 10f;
    [SerializeField, Range(0.5f, 10.0f)] float jumpWeight = 1.0f;

    [SerializeField] float initMoveSpeed                  = 6f;
    [SerializeField, Range(0.5f, 10.0f)] float moveWeight = 1.0f;

    [SerializeField] float initMaxArmDistance                       = 8f;
    [SerializeField, Range(0.5f, 10.0f)] float maxArmDistanceWeight = 5.0f;

    [SerializeField] int initRequiredAmount         = 5;
    #endregion


    #region private variables
    float mJumpPower;
    float mMoveSpeed;
    float mMaxArmDistance;
    float mRequiredAmount;
    float mAmount;
    int mEnemyCount;
    int mMeshID;
    #endregion


    #region private methods
    void Start() {

        mMeshID     = 1;
        mEnemyCount = 0;

        this.transformation(mMeshID);
    }
    #endregion


    #region public methods
    public float checkJumpPower()       { return mJumpPower; }
    public float checkMoveSpeed()       { return mMoveSpeed; }
    public float checkMaxArmDistance()  { return mMaxArmDistance; }
    public int checkMeshID()            { return mMeshID; }
    public int checkEnemyCount()        { return mEnemyCount; }


    public string strRatioOfAmount() {
        return mAmount.ToString("0.0") + "/" + mRequiredAmount.ToString("0.0");
    }


    // scaleをみて比較
    public bool checkWin(GameObject enemy) {
        float meshScale = transform.Find(ConstantParam.PLAYER_PARENT_MESH_NAME).transform.Find(mMeshID.ToString()).localScale.x;
        return (enemy.transform.localScale.x <= meshScale);
    }


    // 進化できるならtrueを返す
    public bool eatEnemy(float enemyScale) {
        mAmount += enemyScale;
        mEnemyCount++;

        Debug.Log("Eat " + mAmount + "/" + mRequiredAmount + " slime");

        return (mRequiredAmount <= mAmount);
    }


    // falseが返ると指定されたmeshIDがないことを意味する
    public void transformation(int meshID) {

        // 指定されたメッシュIDが存在するか確認
        Transform meshesTrans = transform.Find(ConstantParam.PLAYER_PARENT_MESH_NAME).transform;
        if (meshesTrans.Find(meshID.ToString()) == null) {
            Debug.LogError("Can not find " + meshID.ToString() + " mesh object.");
            return;
        }
        
        // メッシュの交換
        Color meshColor = meshesTrans.Find(mMeshID.ToString()).gameObject.GetComponent<Renderer>().material.GetColor("_Color");
        meshesTrans.Find(mMeshID.ToString()).gameObject.SetActive(false);
        meshesTrans.Find(meshID.ToString()).gameObject.SetActive(true);
        meshesTrans.Find(meshID.ToString()).gameObject.GetComponent<Renderer>().material.SetColor("_Color", meshColor);
        mMeshID = meshID;
        
        mAmount = 0;        // 今まで食べた敵の量は初期化される

        // メッシュのscaleに合わせて各パラメータを変化
        float meshScale = meshesTrans.Find(mMeshID.ToString()).localScale.x;
        float baseScale = meshScale - 1.0f;
        GetComponent<SphereCollider>().radius   = meshScale / 2.0f;
        mRequiredAmount                         = initRequiredAmount * meshScale;
        mJumpPower                              = initJumpPower - (baseScale * jumpWeight);
        mMoveSpeed                              = initMoveSpeed - (baseScale * moveWeight);
        mMaxArmDistance                         = initMaxArmDistance + (baseScale * maxArmDistanceWeight);

    }
    #endregion

}
