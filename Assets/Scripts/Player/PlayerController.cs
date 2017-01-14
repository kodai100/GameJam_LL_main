﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float mJumpPower = 100f;
	[SerializeField] float mMoveSpeed = 6f;

	float mKeyH, mKeyW;
	bool mKeyJump;
	bool mIsGround = true;	// 地面に接地しているか

	// Use this for initialization
	void Start () {
		
	}

	// 入力を受け付ける
	void Update() {
		mKeyH       = Input.GetAxis("Horizontal");
		mKeyW       = Input.GetAxis("Vertical");
		mKeyJump    = Input.GetButton("Jump");
	}

	void FixedUpdate(){

		Move (mKeyH, mKeyW);

		// Debug
		if (mKeyJump) {
			Combine (0.5f);
			return;
		}

		// ジャンプ
		if (mIsGround && mKeyJump) {
			GetComponent<Rigidbody> ().AddForce (new Vector3(0f, mJumpPower, 0f), ForceMode.Impulse);
		}
			
	}

	void Move(float h, float w){
		
		// ワールド座標時の移動量に置き換え
		Vector3 worldMove = new Vector3(h, 0.0f, w);

		// カメラの向きに合わせて移動を決定
		Vector3 movement = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation * worldMove;
		movement.y = 0.0f;

		movement = movement.normalized * mMoveSpeed * Time.deltaTime;
		GetComponent<Rigidbody>().MovePosition(transform.position + movement);
	}

	public void Combine(float enemyScale){

		transform.localScale -= new Vector3 (enemyScale, enemyScale, enemyScale);

		// 大きさに合わせてカメラを引く
		Transform target 	= transform.GetChild(0).transform;
		Vector3 angle 		= (target.position - GameObject.FindGameObjectWithTag ("MainCamera").transform.position).normalized;
		target.position -= angle * enemyScale;

		// 移動力やジャンプ力が減る(TODO)


	}

	void OnCollisionEnter(Collision collision) {mIsGround = true; }
	void OnCollisionExit(Collision collision)  {mIsGround = false;}

}
