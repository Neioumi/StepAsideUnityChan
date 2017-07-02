using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	// 動きを減速させる係数
	private float coefficient = 0.95f;
	// ゲーム終了の判定
	private bool isEnd = false;
	// ゲーム終了時のテキスト
	private GameObject stateText;
	// スコア表示のテキスト
	private GameObject scoreText;
	// 得点
	private int score = 0;
	// 左右ボタン押下の判定
	private bool isLButtonDown = false;
	private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {
		// オブジェクトにアタッチされているAnimatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		// 走るアニメーションを開始
		// UnityChanLocomotionsの設定では、下記引数が0.8以上の時に走る
		this.myAnimator.SetFloat("Speed", 1);

		// Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();

		// シーン中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");

		// シーン中のscoreTextオブジェクトを取得
		this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {
		// ゲーム終了ならUnityちゃんの動きを減衰する
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}
		// Unityちゃんに前方向の力を加える
		this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

		// Unityちゃんを矢印キーorボタンで左右に移動させる
		if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			this.myRigidbody.AddForce(-this.turnForce, 0, 0);
		} else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange) {
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

	// 自分のColliderが他のColliderと接触した時に呼ばれる（どちらかがTriggerモードの必要あり）
	void OnTriggerEnter(Collider other) {
		// 障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficCone") {
			this.isEnd = true;
			this.stateText.GetComponent<Text>().text = "GAME OVER";
		}
		
		// ゴールに到達した場合
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			this.stateText.GetComponent<Text>().text = "CLEAR!!";
		}

		// コインに衝突した場合
		if (other.gameObject.tag == "CoinTag") {
			// スコアを加算
			this.score += 10;
			// スコアを表示
			this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
			// パーティクルを再生
			GetComponent<ParticleSystem>().Play();
			// 接触したコインのオブジェクトを破棄
			Destroy(other.gameObject);
		}

	}

	// ジャンプボタンを押下時
	public void GetMyJumpButtonDown() {
		if (this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool("Jump", true);
			this.myRigidbody.AddForce(this.transform.up * this.upForce);
		}
	}

	// 左ボタン
	public void GetMyLeftButtonDown() {
		this.isLButtonDown = true;
	}
	public void GetMyLeftButtonUp() {
		this.isLButtonDown = false;
	}

	// 右ボタン
	public void GetMyRigheButtonDown() {
		this.isRButtonDown = true;
	}
	public void GetMyRigheButtonUp() {
		this.isRButtonDown = false;
	}
}