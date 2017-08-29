using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultComponent : MonoBehaviour {

	//Canvasにスクリプトをアタッチしていて、該当textを選択する必要あり
	public Text P1ScoreNum;
	public Text P2ScoreNum;
	public Text Time;
	public Text P1Result;
	public Text P2Result;


	// Use this for initialization
	void Start () {

		//キャッシュからP1スコアを取得し、リザルトシーンのP1ScoreNumを上書き
		float score1 = CacheData.Entity.P1Score;
		P1ScoreNum.GetComponent<Text>().text = Mathf.FloorToInt(score1).ToString();

		//キャッシュからP2スコアを取得し、リザルトシーンのP1ScoreNumを上書き
		float score2 = CacheData.Entity.P2Score;
		P2ScoreNum.GetComponent<Text>().text = Mathf.FloorToInt(score2).ToString();

		//キャッシュからタイムを取得し、リザルトシーンのタイムを上書き
		float time = CacheData.Entity.Time;
		Time.GetComponent<Text>().text = Mathf.FloorToInt(time).ToString();

		//キャッシュから勝敗を取得し、リザルトシーンのWINLOSEを上書き
		if (CacheData.Entity.P1Result == true) {
			P1Result.GetComponent<Text> ().text = "Win";
			P2Result.GetComponent<Text> ().text = "lose";
		} else {
			P1Result.GetComponent<Text> ().text = "lose";
			P2Result.GetComponent<Text> ().text = "Win";
		}
	}

	// Update is called once per frame
	void Update () {

	}
		
}
