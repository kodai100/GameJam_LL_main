using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CombineProcess : MonoBehaviour{

	[SerializeField] int mRequiredNum;	// 大きくなるために必要な敵の数

	int mCnt = 0;
	int mId  = 1;
	Material playerMaterial;

    Shoot shootScript;

	void Awake(){
		playerMaterial  = GetComponent<Renderer>().material;
        shootScript 	= GetComponent<Shoot>();
	}

	void OnEnable(){
		StaticManager.playerScale = transform.localScale.x;
	}

	public void Combine(){
		
		mCnt++;
		if (mCnt < mRequiredNum) {
			return;
		}

		Debug.Log ("Evolution!!");

		GameObject next;
		try{
			next = transform.parent.parent.FindChild ((mId + 1).ToString ()).gameObject;
		}
		catch(NullReferenceException e){
			Debug.Log("Evolution limit");
			return;
		}

		// カメラを引く
		GameObject.FindGameObjectWithTag("MainCamera").transform.parent.transform.localPosition -= new Vector3(0f, 0f, 1f);

		next.SetActive (true);
		next.GetComponentInChildren<Rigidbody> ().transform.position = transform.position;
		next.GetComponentInChildren<CombineProcess> ().SetId (mId + 1);

		gameObject.transform.parent.gameObject.SetActive (false);
		gameObject.SetActive (false);

	}

	void OnCollisionEnter(Collision other) {

        if (!shootScript.isShooting)
        {
            // 敵に当たったら
            if (other.gameObject.tag == "Enemy")
            {


                // 吸収
                if (gameObject.transform.lossyScale.x >= other.gameObject.transform.lossyScale.x)
                {
                    playerMaterial.SetColor("_Color", other.gameObject.GetComponent<Renderer>().material.GetColor("_Color"));

                    Destroy(other.gameObject);

                    StaticManager.enemyCount++;

                    Combine();

                    Debug.Log("Killed. total : " + StaticManager.enemyCount);
                }
                else
                {   // GAMEOVER
                    Debug.Log("GAMEOVER");
                    SceneManager.LoadScene("Result");
                }
            }
        }

		

	}

	public void SetId(int id){ mId = id;}

}
