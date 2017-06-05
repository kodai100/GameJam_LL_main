﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
//using System.Diagnostics;

public class CombineProcess : MonoBehaviour{

	[SerializeField]
	private bool isDebug;

	[SerializeField]
	private float m_RequireAmount;

	float m_Amount;
	int mId  = 1;
	Material playerMaterial;

    Shoot shootScript;

	#region MonoBehaviourFuncs
	void Awake(){
		m_Amount = 0f;
		playerMaterial  = GetComponent<Renderer>().material;
        shootScript 	= GetComponent<Shoot>();
	}

	#if UNITY_EDITOR
	void Update(){
		// [Debug]Jキーを押すと進化する
		if(Input.GetKeyDown(KeyCode.J)){
			Combine (m_RequireAmount);
		}
	}
	#endif

	void OnEnable(){
		StaticManager.playerScale = transform.localScale.x;
		StaticManager.amount = m_Amount;
		StaticManager.requireAmount = m_RequireAmount;
	}
	#endregion MonoBehaviourFuncs

	public void Combine(float amount){
		m_Amount += amount;
		StaticManager.amount = m_Amount;
		SeManager.Instance.Play ("PlayerEat2");

		if (isDebug)
		{
			Debug.Log("Eat " + m_Amount + "/" + m_RequireAmount + " slime");
		}

		if (m_Amount < m_RequireAmount) {
			return;
		}

		GameObject next;
		try{
			next = transform.parent.parent.Find ((mId + 1).ToString ()).gameObject;
		}
		catch(NullReferenceException){
			if (isDebug) {
				Debug.Log("Can not find evolved object.");
			}
			return;
		}

		// カメラを引く
		float scaleDiff 	= next.gameObject.transform.localScale.x - transform.localScale.x;
		GameObject camera	= GameObject.FindGameObjectWithTag("MainCamera");
		Vector3 dirZoomOut 	= Vector3.Normalize(camera.transform.position - transform.position);
		camera.transform.localPosition -= dirZoomOut * scaleDiff;

		next.SetActive (true);
		next.GetComponentInChildren<Rigidbody> ().transform.position = transform.position;
		next.GetComponentInChildren<CombineProcess> ().SetId (mId + 1);

		gameObject.transform.parent.gameObject.SetActive (false);
		gameObject.SetActive (false);

	}

	void OnCollisionEnter(Collision other) {
		if (StaticManager.isWin)
			return;

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

					Combine(other.transform.localScale.x);

					if (isDebug) {
						Debug.Log("Killed. total : " + StaticManager.enemyCount);
					}
                }
                else
                {
					// GAMEOVER
					if (isDebug) {
						Debug.Log("GAMEOVER");
					}

					SeManager.Instance.Play ("EnemyEat");
					transform.root.gameObject.SetActive(false);
					MainManager.Instance.Lose();
                }
            }
        }

		

	}

	public void SetId(int id){ mId = id;}

}
