using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmHitEvent : MonoBehaviour {

	#region private variables
	List<GameObject> mCatchLists = new List<GameObject>();
	#endregion


	void Update(){
		
		foreach (GameObject enemy in mCatchLists) {
			enemy.transform.position = transform.position;
		}
	}
		
	void OnTriggerStay(Collider other) {

		// 敵を捕まえてプレイヤーのところに持ってくる処理
		if (other.gameObject.tag == "Enemy") {

			mCatchLists.Add (other.gameObject);
			other.gameObject.GetComponent<Collider> ().enabled = false;
		}

		// 障害物に当たったら舌を引っ込める処理に遷移
		else if (other.gameObject.tag != "Player") {

			gameObject.transform.parent.GetComponent<Shoot> ().returnArm ();
		}
			
	}

	// 敵をDestroyするときこれを呼ばなければいけない(該当する敵オブジェクトがなくてもエラーはでない)
	void DeleteEnemyFromLists(GameObject destroyed){

		foreach (GameObject enemy in mCatchLists) {
			if (destroyed == enemy) {
				mCatchLists.Remove (enemy);
				break;
			}
		}
	}
	#endregion

}
