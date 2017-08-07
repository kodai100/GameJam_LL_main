using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region private variables
	float mJumpIntervalSec = 0.1f;
	float mKeyX, mKeyY;
	bool mKeyJump;
	float mTime    = 0f;

    Rigidbody mRigidbody;
    PlayerParametter mPlayerParam;
    SphereCollider mSphereCollider;
    #endregion

    #region public methods
	void Awake(){
		mRigidbody      = GetComponent<Rigidbody> ();
        mPlayerParam    = GetComponent<PlayerParametter>();
        mSphereCollider = GetComponent<SphereCollider>();

    }

	// 入力を受け付ける
	void Update() {
		mKeyX       = Input.GetAxis("Horizontal");
		mKeyY       = Input.GetAxis("Vertical");
		mKeyJump    = Input.GetButton("Jump");
	}

	void FixedUpdate(){
			
		Move (mKeyX, mKeyY);

		// ジャンプ処理
		if (CheckGrounded ()) {

			// ジャンプ後すぐにジャンプできないようにインターバルをとる
			if (mTime < mJumpIntervalSec) {
				mTime += Time.deltaTime;
			}
			else if(mKeyJump){
				
				// ジャンプする前にかかっている速度を0に戻す
				mRigidbody.velocity = Vector3.zero;

				string seName = (mSphereCollider.radius < 1.5f) ? "Jump" : "JumpBig";
				SeManager.Instance.Play (seName);

				mRigidbody.AddForce (new Vector3 (0f, mPlayerParam.checkJumpPower(), 0f), ForceMode.Impulse);
				mTime = 0f;
			}
		}

		StaticManager.playerPos = transform.position;
	}
    #endregion

    #region private method
	// 移動を反映する
	private void Move(float x, float y){
		
		// ワールド座標時の移動量に置き換え
		Vector3 worldMove = new Vector3(x, 0.0f, y);

		// カメラの向きに合わせて移動を決定
		Vector3 movement = GameObject.FindGameObjectWithTag("MainCamera").transform.rotation * worldMove;
		movement.y = 0.0f;

		movement = movement.normalized * mPlayerParam.checkMoveSpeed() * Time.deltaTime;
		mRigidbody.MovePosition(transform.position + movement);
	}
		
	// 真下にRayを飛ばして接地してるかを判定
	private bool CheckGrounded() {

		float range = transform.GetComponent<SphereCollider>().radius * transform.localScale.x * 1.5f;		// 1.5f = offset
		Debug.DrawLine(transform.position, transform.position - (transform.up * range), Color.red);

		RaycastHit hitCollider;
		if (Physics.Linecast(transform.position, transform.position - (transform.up * range), out hitCollider)) {
			return !hitCollider.collider.isTrigger;
		}

		return false;
	}
    #endregion

}
