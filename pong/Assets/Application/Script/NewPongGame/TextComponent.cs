using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class TextComponent : MonoBehaviour {
	Text _P1Area;
	Text _P2Area;
	int area1;
	int area2;


	// Use this for initialization
	void Start () {
		_P1Area = GameObject.Find("P1AreaText").GetComponent<Text>();
		_P2Area = GameObject.Find("P2AreaText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		area1 = CacheData.Entity.P1Area;
		area2 = CacheData.Entity.P2Area;

		_P1Area.text = ((int)area1).ToString ();
		_P2Area.text = ((int)area2).ToString ();
		
	}
}
