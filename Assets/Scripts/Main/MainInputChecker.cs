// Mainシーン中の他オブジェクトが管理していない入力を処理

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInputChecker : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {

        // ESCキーで終了
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("End");
            Application.Quit();
        }
    }
}
