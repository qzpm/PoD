using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeComponent : MonoBehaviour {
	private float counttime = 99;

	void Start () {
		//初期値60を表示
		//float型からint型へCastし、String型に変換して表示
		GetComponent<Text>().text = ((int)counttime).ToString();
	}

	void Update (){
		//1秒に1ずつ減らしていく
		counttime -= Time.deltaTime;
		CacheData.Entity.Time += Time.deltaTime;
		//マイナスは表示しない
		if (counttime < 0) counttime = 0;
		GetComponent<Text> ().text = ((int)counttime).ToString ();
	}
}