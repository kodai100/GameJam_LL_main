using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraControll : MonoBehaviour {

    #region private variables
    float mDefaultCamY;
    float mDefaultCamZ;
    GameObject mPivot;
    GameObject mMainCamera;
    #endregion

    #region private methods
    void Awake() {
        mPivot      = transform.GetChild(0).gameObject;
        mMainCamera = mPivot.transform.GetChild(0).gameObject;

        mDefaultCamY = mPivot.transform.localPosition.y;
        mDefaultCamZ = mMainCamera.transform.localPosition.z;
    }
    #endregion

    #region public methods
    public void moveCameraFromDefault(float y, float z) {

        mPivot.transform.localPosition = new Vector3(0f, mDefaultCamY + y, 0f);
        mMainCamera.transform.localPosition = new Vector3(0f, 0f, mDefaultCamZ + z);
    }
    #endregion

}

