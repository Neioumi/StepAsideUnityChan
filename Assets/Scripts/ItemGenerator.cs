using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour {
	private GameObject unitychan;
	private float unitychanPos;

	// デバッグフラグ
	private bool debugFlag = false;
	// デバッグ用のテキスト
	private GameObject debugText;

	public GameObject carPrefab;
	public GameObject coinPrefab;
	public GameObject conePrefab;
	// アイテム生成のスタート地点
	private int startPos = -160;
	// ゴール地点
	private int goalPos = 120;
	// アイテムをだすX方向の範囲
	private float posRange = 3.4f;
	// アイテムを生成しておく距離(見える距離)
	private int viewableDistance = 45;
	// アイテムを生成する位置(初期値はスタート地点)
	private float itemPos = -160.0f;

	// Use this for initialization
	void Start () {
		// Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");

		// デバッグ時
		if (debugFlag == true) {
			// シーン中のdebugTextオブジェクトを取得
			// SetActive(false)の自身は探せないが、親のtransformから探す
			debugText = GameObject.Find("Canvas").transform.Find("DebugText").gameObject;
			// デバッグ用テキストを表示
			debugText.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Unityちゃんの位置
		unitychanPos = unitychan.transform.position.z;

		// デバッグ時はUnityちゃんの位置を画面内にテキストで表示
		if (debugFlag == true) {
			debugText.GetComponent<Text>().text = "unitychanPos" + unitychanPos + "m";
		}

		// アイテムを生成可能な範囲のとき（Unityちゃんがスタート地点の45m前から、ゴールの45m前にいるとき）
		if (unitychanPos >= startPos - viewableDistance && unitychanPos < goalPos - viewableDistance) {
			// Debug.Log(unitychanPos + "アイテム生成可能範囲");

			// Unityちゃんが次のアイテム生成位置（45m前）に来たとき
			if (unitychanPos >= itemPos - viewableDistance) {
				Debug.Log("Unityちゃんの位置" + unitychanPos + "m");
				// どのアイテムを出すかランダムに設定
				int num = Random.Range(0, 10);
				if (num <= 1) {
					// コーンをX軸方向に一直線に生成
					for (float j = -1; j <= 1; j += 0.4f) {
						GameObject cone = Instantiate(conePrefab) as GameObject;
						cone.transform.position = new Vector3(4 * j, cone.transform.position.y, itemPos);
					}
					Debug.Log("コーン生成！" + itemPos + "m地点");
				} else {
					// レーンごとにアイテムを生成
					for (int j = -1; j < 2; j++) {
						// アイテムの種類を決める
						int item = Random.Range(1, 11);
						// アイテムを置くZ座標のオフセットをランダムに設定
						int offsetZ = Random.Range(-5, 6);
						// 60%コイン配置:30%車配置:10%何もなし
						if (itemPos <= item && item <= 6) {
							// コインを生成
							GameObject coin = Instantiate(coinPrefab) as GameObject;
							coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, itemPos + offsetZ);
							Debug.Log("コイン生成！" + itemPos + "m地点");
						} else if (7 <= item && item <= 9) {
							// 車を生成
							GameObject car = Instantiate(carPrefab) as GameObject;
							car.transform.position = new Vector3(posRange * j, car.transform.position.y, itemPos + offsetZ);
							Debug.Log("車生成！" + itemPos + "m地点");
						}
					}
				}
				// 次のアイテム生成位置を15m先に設定
				itemPos += 15;
				Debug.Log("次回アイテム生成位置更新 " + itemPos + "m地点");
			}
		}
	}
}
