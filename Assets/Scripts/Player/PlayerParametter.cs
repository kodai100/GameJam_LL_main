// プレイヤーのパラメータを持つだけのクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParametter : MonoBehaviour {

    #region  SerializeField variables
    [SerializeField] float mRequiredAmount;
    [SerializeField] float mJumpPower;
    [SerializeField] float mMoveSpeed;
    [SerializeField] float mMaxArmDistance;
    #endregion

    #region private variables
    float mAmount;
    int mEnemyCount;
    float mRadius;
    int mMeshID;
    #endregion

    #region private methods
    void Start() {
        mAmount     = 0;
        mEnemyCount = 0;
        mRadius     = GetComponent<SphereCollider>().radius;
        mMeshID     = 1;
    }
    #endregion

    #region public methods
    public float checkJumpPower()       { return mJumpPower; }
    public float checkMoveSpeed()       { return mMoveSpeed; }
    public float checkRadius()          { return mRadius; }
    public float checkMaxArmDistance()  { return mMaxArmDistance; }
    public string checkMeshIDName()     { return mMeshID.ToString(); }
    public int checkEnemyCount()        { return mEnemyCount; }

    public string strRatioOfAmount() {
        return mAmount.ToString("0.0") + "/" + mRequiredAmount.ToString("0.0");
    }

    public bool checkWin(GameObject enemy) {
        return (enemy.GetComponent<SphereCollider>().radius <= mRadius);
    }

    // 進化できるならtrueを返す
    public bool eatEnemy(float amounts) {
        mAmount += amounts;
        mEnemyCount++;

        Debug.Log("Eat " + mAmount + "/" + mRequiredAmount + " slime");

        return (mRequiredAmount <= mAmount);
    }

    public void evolved() {
        mAmount = 0;
        mMeshID++;
        mRequiredAmount *= 1.5f;
        mJumpPower      *= 0.8f;
        mMoveSpeed      *= 0.8f;
        mRadius         *= 2.0f;
        mMaxArmDistance *= 1.5f;
    }
    #endregion

}
