using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる
using UnityEngine.SceneManagement;

public class ScoreComponent: MonoBehaviour {

	Slider _P1Guage;
	Slider _P2Guage;
	Slider _P1SkillGuage;
	Slider _P2SkillGuage;
    [SerializeField]
    Image _Fill1;

	float _hp1;
	float _hp2;

	int area1;
	int area2;

    float gameTime;

	float skillpoint1;
	float skillpoint2;

    int r = 0;
    int g = 100;
    int b = 0;

	//スキル増減周りのシステム
	void Skillsystem(){
		skillpoint1 = CacheData.Entity.P1SkillPoint;
		skillpoint2 = CacheData.Entity.P2SkillPoint;

		if (skillpoint1 < 1000) {
			skillpoint1 += 1 * 60 * Time.deltaTime;
		}

		if (skillpoint2 < 1000) {
			skillpoint2 += 1 * 60 * Time.deltaTime;
		}

		CacheData.Entity.P1SkillPoint = skillpoint1;
		CacheData.Entity.P2SkillPoint = skillpoint2;

		_P1SkillGuage.value = skillpoint1;
		_P2SkillGuage.value = skillpoint2;


	}

	//エリアやスコア周りのシステムと勝敗判定
	void Areasystem(){

		_hp1 = CacheData.Entity.P1Score;
		_hp2 = CacheData.Entity.P2Score;

		area1 = CacheData.Entity.P1Area;
		area2 = CacheData.Entity.P2Area;

        gameTime = CacheData.Entity.Time;

		//プレイヤー1のエリアがプレイヤー2より多い場合
		if (area1 > area2) {
			_hp1 += 2 * 60 * Time.deltaTime;
			CacheData.Entity.P1Score = _hp1;
			_P1Guage.value = _hp1;
		}
		//プレイヤー2のエリアがプレイヤー1より多い場合 
		if (area1 < area2) {
			_hp2 += 2 * 60 * Time.deltaTime;
			CacheData.Entity.P2Score = _hp2;
			_P2Guage.value = _hp2;
		}

		//プレイヤー2のエリアが0になった場合
		if (area2 == 0) {
			CacheData.Entity.P1Result = true;
			CacheData.Entity.P2Result = false;
			SceneManager.LoadScene ("ResultScene");
		}

		//プレイヤー1のエリアが0になった場合
		if (area1 == 0) {
			CacheData.Entity.P1Result = false;
			CacheData.Entity.P2Result = true;
			SceneManager.LoadScene ("ResultScene");
		}

		//プレイヤー1のスコアゲージがMAXになったら
		if (_hp1 >= 5000) {
			CacheData.Entity.P1Result = true;
			CacheData.Entity.P2Result = false;
			SceneManager.LoadScene ("ResultScene");
		}

		//プレイヤー2のスコアゲージがMAXになったら
		if (_hp2 >= 5000) {
			CacheData.Entity.P1Result = false;
			CacheData.Entity.P2Result = true;
			SceneManager.LoadScene ("ResultScene");
		}

        //時間切れになったら
        if(gameTime > 99) {
            //スコアゲージの長い方が勝利
            if (_hp1 > _hp2) {
                CacheData.Entity.P1Result = true;
                CacheData.Entity.P2Result = false;
                SceneManager.LoadScene("ResultScene");
            } else if (_hp1 < _hp2) {
                CacheData.Entity.P1Result = false;
                CacheData.Entity.P2Result = true;
                SceneManager.LoadScene("ResultScene");
            }
            //万が一スコアゲージが完全に一致した場合は結果画面に進まない(実質延長戦)
        }

        // HPゲージに値を設定
        _P1Guage.value = _hp1;
		_P2Guage.value = _hp2;
	}


	void Start () {
		// スライダーを取得する
		_P1Guage = GameObject.Find("P1Guage").GetComponent<Slider>();
		_P2Guage = GameObject.Find("P2Guage").GetComponent<Slider>();
		_P1SkillGuage = GameObject.Find("P1SkillGuage").GetComponent<Slider>();
		_P2SkillGuage = GameObject.Find("P2SkillGuage").GetComponent<Slider>();
	}
		
	void Update () {
		area1 = CacheData.Entity.P1Area;
		area2 = CacheData.Entity.P2Area;
		Skillsystem ();
		Areasystem ();

	}
}
