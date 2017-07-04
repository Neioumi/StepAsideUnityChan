using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDestroyer : MonoBehaviour {
	private GameObject unitychan;
	private float unitychanPos;

	// デバッグ用のテキスト
	private GameObject debugText;

	// カメラから映らなくなるぐらいの距離
	private float margin = 4.0f;

	// Use this for initialization
	void Start () {
		// Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		// シーン中のdebugTextオブジェクトを取得
		debugText = GameObject.Find("DebugText");
		

	}
	
	// Update is called once per frame
	void Update () {
		// Unityちゃんの位置
		unitychanPos = unitychan.transform.position.z;
		// Unityちゃんの位置を画面内にテキストで表示
		debugText.GetComponent<Text>().text = "unitychanPos" + unitychanPos;
		// 各オブジェクトの位置が、Unityちゃんの後ろ、カメラに映らない位置になったら破棄
		if (this.transform.position.z < unitychanPos - margin) {
			Destroy(this.gameObject);
		}
	}

	// レンダラーがカメラから見えなくなった時に呼び出されるので、
	// コインは消えない（親を作ってレンダラー追加すれば消せる）
	// void OnBecameInvisible() {
	// 	Debug.Log("Destroy " + this.gameObject);
	// 	Destroy(this.gameObject);
	// }
}
