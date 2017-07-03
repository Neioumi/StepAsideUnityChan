using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDestroyer : MonoBehaviour {
	private GameObject unitychan;
	private float unitychanPos;

	// デバッグ用のテキスト
	private GameObject debugText;

	// Use this for initialization
	void Start () {
		// Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		// Unityちゃんの位置
		// this.unitychanPos = unitychan.transform.position.z;
		// シーン中のdebugTextオブジェクトを取得
		debugText = GameObject.Find("DebugText");
		

	}
	
	// Update is called once per frame
	void Update () {
		// Unityちゃんの位置
		unitychanPos = unitychan.transform.position.z;
		// Unityちゃんの位置を画面内にテキストで表示
		debugText.GetComponent<Text>().text = "unitychanPos" + unitychanPos;
	}

	// レンダラーがカメラから見えなくなった時に呼び出されるので、
	// コインは消えない（親を作ってレンダラー追加すれば消せる）
	// void OnBecameInvisible() {
	// 	Debug.Log("Destroy " + this.gameObject);
	// 	Destroy(this.gameObject);
	// }
}
