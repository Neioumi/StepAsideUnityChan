using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		// 回転開始の角度
		this.transform.Rotate(0, Random.Range(0, 360), 0);
	}
	
	// Update is called once per frame
	void Update () {
		// 回転
		this.transform.Rotate(0, 3, 0); // y軸を中心に回転
	}
}
