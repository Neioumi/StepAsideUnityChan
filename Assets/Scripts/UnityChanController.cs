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

	// Use this for initialization
	void Start () {
		// オブジェクトにアタッチされているAnimatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		// 走るアニメーションを開始
		// UnityChanLocomotionsの設定では、下記引数が0.8以上の時に走る
		this.myAnimator.SetFloat ("Speed", 1);

		// Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		// Unityちゃんに前方向の力を加える
		this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);
	}
}
