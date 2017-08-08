using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndClick : MonoBehaviour {

    float mTimer = -1f;

    void Update() {

        if (mTimer < 0) { return; }

        mTimer += Time.deltaTime;
        if(mTimer > 0.5f) {
            Application.Quit();
        }

    }

    public void Push () {
        BgmManager.Instance.TimeToFade = 0.5f;
        BgmManager.Instance.Stop();
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.LoadLevel(null, 0.5f);
        mTimer = 0f;
    }
}
