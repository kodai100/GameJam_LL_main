using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float mJumpPower    	= 10f;
	[SerializeField] float mMoveSpeed 	 	= 6f;
	[SerializeField] float mJumpIntervalSec = 0.2f;

	float mKeyH, mKeyW;
	bool mKeyJump;
	float mTime    = 0f;
	Rigidbody mRigidbody;

	void Awake(){
		mRigidbody = GetComponent<Rigidbody> ();
	}

	// 入力を受け付ける
	void Update() {
		mKeyH       = Input.GetAxis("Horizontal");
		mKeyW       = Input.GetAxis("Vertical");
		mKeyJump    = Input.GetButton("Jump");
	}

	void FixedUpdate(){

		if (mTime < mJumpIntervalSec) {
			mTime += Time.deltaTime;
		}
			
		Move (mKeyH, mKeyW);

		// ジャンプ
		if ((mTime >= mJumpIntervalSec) && mKeyJump && CheckGrounded()) {
			SeManager.Instance.Play ("jump");
			GetComponent<Rigidbody> ().AddForce (new Vector3(0f, mJumpPower, 0f), ForceMode.Impulse);
			mTime = 0f;
		}

		StaticManager.playerPos = transform.position;
	}

	void Move(float h, float w){
		
		// ワールド座標時の移動量に置き換え
		Vector3 worldMove = new Vector3(h, 0.0f, w);

		// カメラの向きに合わせて移動を決定
		Vector3 movement = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation * worldMove;
		movement.y = 0.0f;

		movement = movement.normalized * mMoveSpeed * Time.deltaTime;
		//transform.parent.parent.position += movement;
		GetComponent<Rigidbody>().MovePosition(transform.position + movement);
	}
		
	// 真下にRayを飛ばして接地してるかを判定
	bool CheckGrounded() {

		float range = transform.GetComponent<SphereCollider>().radius * transform.localScale.x * 1.5f;		// 1.5f = offset
		Debug.DrawLine(transform.position, transform.position - (transform.up * range), Color.red);

		RaycastHit hitCollider;
		if (Physics.Linecast(transform.position, transform.position - (transform.up * range), out hitCollider)) {
			return !hitCollider.collider.isTrigger;
		}

		return false;
	}

}
