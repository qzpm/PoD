using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Title(){
		SceneManager.LoadScene("TitleScene");
		CacheData.Entity.Refresh ();
	}

	public void Replay(){
		SceneManager.LoadScene("Game_Master");
        //CacheData.Entity.Refresh ();
        CacheData.Entity.P1Score = 100;
        CacheData.Entity.P2Score = 100;
        CacheData.Entity.P1Area = 5;
        CacheData.Entity.P2Area = 5;
        CacheData.Entity.P1SkillPoint = 0;
        CacheData.Entity.P2SkillPoint = 0;
        CacheData.Entity.Time = 0;
    }

	public void SkillSelect(){
		SceneManager.LoadScene("SkillSelect");
		CacheData.Entity.Refresh ();
	}
}
