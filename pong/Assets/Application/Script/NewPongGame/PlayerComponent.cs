using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour {

	//gObjectを中心にオブジェクトが円運動をする 
	public GameObject gObject;
    //円運動の速度
	private float speed;
	public string axisName;
    public string skillButtonName;
    public int colorId;

    private AreaComponent areacomponent;
    private PlayerComponent enemy;
    //オブジェクトの角度(Degree)
    //Player1は90、Player2は270(=-90)からスタートする
    public float controllerAngle;
    //本来の現在地との角度差分(controllerAngle - 現在地の値)
    private float angleDifference;
    //オブジェクトの大きさ(角度)
    private float sizeAngle = 5;

    private int skillNumber;
    //行動不能時間、これが0より大きい間は操作不能
    private float stun = 0;
    //加速スキルの残り時間、これが0になると効果解除
    private float accelTime = 0;

	float P1SkillPoint;
	float P2SkillPoint;

	// Use this for initialization
	void Start () {
        speed = 2.0f; //Mathf.PI * 2 にすると大体1秒で1周できる
        areacomponent = GameObject.Find("Field_Darts").GetComponent<AreaComponent>();

        if(colorId == 1) {
            skillNumber = CacheData.Entity.P1Skill;
            enemy = GameObject.Find("Player2").GetComponent<PlayerComponent>();
        } else {
            skillNumber = CacheData.Entity.P2Skill;
            enemy = GameObject.Find("Player1").GetComponent<PlayerComponent>();
        }
    }

	// Update is called once per frame
	void Update () {

	//キャッシュからスキルポイントを呼び出し格納
	P1SkillPoint = CacheData.Entity.P1SkillPoint;
	P2SkillPoint = CacheData.Entity.P2SkillPoint;

        if(stun <= 0) { 
            //スタンしていないので操作できる
		    float Key = Input.GetAxis (axisName);

		    if (Key == 1) {
                controllerAngle -= speed * 60 * Time.deltaTime;

                //ワープが中途半端ならリセット
                if(angleDifference > 0) {
                    controllerAngle -= angleDifference;
                    angleDifference = 0;
                }
            
                if (controllerAngle < 0) {
                    controllerAngle += 360;
                }

                int areaColor = areacomponent.AngleToAreaColor(controllerAngle - sizeAngle, colorId);
                if (areaColor == colorId) {
                    if(angleDifference == 0) {
                        transform.RotateAround(gObject.transform.position, Vector3.down, speed * 60 * Time.deltaTime);
                    } else {
                        transform.RotateAround(gObject.transform.position, Vector3.down, -angleDifference + speed * 60 * Time.deltaTime);
                        angleDifference = 0;
                    }
                } else {
                    angleDifference -= speed * 60 * Time.deltaTime;
                }

		    }else if(Key == -1){
			    controllerAngle += speed * 60 * Time.deltaTime;

                //ワープが中途半端ならリセット
                if (angleDifference < 0) {
                    controllerAngle -= angleDifference;
                    angleDifference = 0;
                }

                if (controllerAngle >= 360) {
                    controllerAngle -= 360;
                }

                int areaColor = areacomponent.AngleToAreaColor(controllerAngle + sizeAngle, colorId);
                if (areaColor == colorId) {
                    if (angleDifference == 0) {
                        transform.RotateAround(gObject.transform.position, Vector3.up, speed * 60 * Time.deltaTime);
                    } else {
                        transform.RotateAround(gObject.transform.position, Vector3.up, angleDifference + speed * 60 * Time.deltaTime);
                        angleDifference = 0;
                    }
                } else {
                    angleDifference += speed * 60 * Time.deltaTime;
                }
            }

	        if (Input.GetButtonDown(skillButtonName)) {
					
				//プレイヤー1がスキルを発動した時　かつスキルポイントが1000の時
				if(colorId == 1 && P1SkillPoint >= 1000){
	                //スキルが発動する
					//スキルが発動するとスキルポイントは0になる
	                if(skillNumber == 0) {
                        //スタンスキル
                        enemy.Stun();
                        areacomponent.ShotDarts(0, colorId);
	                } else if(skillNumber == 1) {
                        //エリア強奪スキル
                        areacomponent.ShotDarts(1, colorId);
	                } else if(skillNumber == 2) {
	                    StartAccel();
                    }
                    CacheData.Entity.P1SkillPoint = 0;
                }
				//プレイヤー1がスキルを発動した時　かつスキルポイントが1000の時
				if(colorId == 2 && P2SkillPoint >= 1000){
					//スキルが発動する
					//スキルが発動するとスキルポイントは0になる
					if(skillNumber == 0) {
                        //スタンスキル
                        enemy.Stun();
                        areacomponent.ShotDarts(0, colorId);
					} else if(skillNumber == 1) {
                        //エリア強奪スキル
                        areacomponent.ShotDarts(2, colorId);
					} else if(skillNumber == 2) {
						StartAccel();
                    }
                    CacheData.Entity.P2SkillPoint = 0;
                }
	        }
		///////////
	        } else {
	            //スタン解除までのカウントが進む
	            stun -= Time.deltaTime;
	        }
			
			//加速判定
	        if(accelTime > 0) {
	            accelTime -= Time.deltaTime;
	            if(accelTime <= 0) {
	                FinishAccel();
	            }
	        }
	    }

    private void OnCollisionEnter(Collision collision) {
        NewBallComponent ball = collision.gameObject.GetComponent<NewBallComponent>();
        if (ball != null) {
            if(ball.colorId != colorId) {
                ball.Accell(1.1f);
            } else {
                ball.Accell(1f);
            }

            ball.ChangeColor(colorId);
            ball.PlaySound();
        }
    }

    public void Stun() {
        stun = 3.0f;        //スタン効果時間、数値仮置き
    }

    public void StartAccel() {
        speed = 4f;         //効果中の移動速度、数値仮置き
        accelTime = 10f;    //加速スキル効果時間、数値仮置き
        Transform t = transform.GetChild(0);
        t.gameObject.SetActive(true);
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void FinishAccel() {
        speed = 2f;
        Transform t = transform.GetChild(0);
        t.gameObject.SetActive(false);
    }
}
