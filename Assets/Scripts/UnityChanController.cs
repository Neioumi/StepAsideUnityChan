using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {
	// アニメーションのためのコンポーネントを入れる
	private Animator myAnimator;
	// 移動させるためのコンポーネントを入れる
	private Rigidbody myRigidbody;
	// 前進のための力
	private float forwardForce = 800.0f;
	// 左右移動のための力
	private float turnForce = 500.0f;
	// ジャンプするための力
	private float upForce = 500.0f;
	// 左右移動できる範囲
	private float movableRange = 3.4f;

	// Use this for initialization
	void Start () {
		// オブジェクトにアタッチされているAnimatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		// 走るアニメーションを開始
		// UnityChanLocomotionsの設定では、下記引数が0.8以上の時に走る
		this.myAnimator.SetFloat("Speed", 1);

		// Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		// Unityちゃんに前方向の力を加える
		this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

		// Unityちゃんを矢印キーorボタンで左右に移動させる
		if (Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x) {
			this.myRigidbody.AddForce(-this.turnForce, 0, 0);
		} else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.movableRange) {
			this.myRigidbody.AddForce(this.turnForce, 0, 0);
		}

		// Jumpステートの場合、Jumpにfalseをセット
		if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
			this.myAnimator.SetBool("Jump", false);
		}
		// ジャンプしていない時にスペースが押されたらジャンプする
		if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool("Jump", true);
			this.myRigidbody.AddForce(this.transform.up * this.upForce);
		}
	}
}
