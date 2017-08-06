using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmHitEvent : MonoBehaviour {

	#region private variables
	List<GameObject> mCatchLists = new List<GameObject>();
    PlayerParametter mPlayerParam;
    #endregion

    #region private methods
    void Start() {
        mPlayerParam = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParametter>();
    }

    void Update(){
		
		foreach (GameObject enemy in mCatchLists) {

            if (enemy == null) {
                mCatchLists.Remove(enemy);
                break;
            }
            else {
                enemy.transform.position = transform.position;
            }
		}
	}
		
	void OnTriggerStay(Collider other) {

        if (other.gameObject.tag == "Enemy") {

            // 小さい敵を捕まえてプレイヤーのところに持ってくる前処理
            if (mPlayerParam.checkWin(other.gameObject)) {

                mCatchLists.Add(other.gameObject);
            }
            else {
                SeManager.Instance.Play("Reflect");
                gameObject.transform.parent.GetComponent<Shoot>().returnArm();
            }
		}

		// 障害物に当たったら舌を引っ込める処理に遷移
		else if (other.gameObject.tag != "Player") {

			gameObject.transform.parent.GetComponent<Shoot> ().returnArm ();
		}
			
	}
    #endregion
    
}
