using UnityEngine;
using System.Collections;
using UnityEngine.UI;//UIコンポーネントの使用
using UnityEngine.SceneManagement;



public class SkillSelect : MonoBehaviour
{
	[SerializeField]
    Button skill1;
	[SerializeField]
    Button skill2;
	[SerializeField]
    Button skill3;
    int p1 = 1;//player1
    int p2 = 1;//player2
	bool selected1 = false;
	bool selected2 = false;
    Button[] ss = new Button[3];
    Color[] c1 = new Color[3];
    GameObject cursorP1;
    GameObject cursorP2;

    // Use this for initialization
    void Start()
    {    
        //Buttonコンポーネントの取得
        ss[0] = GameObject.Find("/Canvas/skill1").GetComponent<Button>();
        ss[1] = GameObject.Find("/Canvas/skill2").GetComponent<Button>();
        ss[2] = GameObject.Find("/Canvas/skill3").GetComponent<Button>();

        //プレイヤーカーソルの設定
        cursorP1 = GameObject.Find("/Canvas/Player1");
        cursorP2 = GameObject.Find("/Canvas/Player2");

        //最初に選択状態にしたいボタンの設定
        ss[1].Select();


        
    }


//	public void SelectSkill(int num)
//	{
//		if (num == 1) {
//			skill1.image.color = new Color (255, 0, 0);
//		} else if (num == 2) {
//			skill2.image.color = new Color (0, 0, 255);
//		} else if (num == 3) {
//			skill3.image.color = new Color (0, 255, 0);
//		}
//	}


    public void Update()
    {

        //プレイヤー１がスキル１選択時のカラー変更
        ColorBlock colorBlock1 = ss[0].colors;
        colorBlock1.highlightedColor = Color.red;
        ss[0].colors = colorBlock1;

        //プレイヤー２がスキル１選択時のカラー変更
        ColorBlock colorBlock2 = ss[0].colors;
        colorBlock2.highlightedColor = Color.green;
        ss[0].colors = colorBlock2;



        //player1のスキル選択
        ss[p1].Select();
        cursorP1.transform.position = ss[p1].gameObject.transform.position + new Vector3(0, 30, 0);
        //左向きキーを入力 
		if (Input.GetKeyDown(KeyCode.K) && selected1 == false)
         {
            p1--;
                
                if (p1 <= -1)
                    {
                         p1 = 2;
                    }
            
          }

        //右向きキーを入力
		if (Input.GetKeyDown(KeyCode.M) && selected1 == false)
        {
            p1++;

                 if (p1 >= 3)
                     {
                         p1 = 0;
                     }   

        }

        //player2のスキル選択
        ss[p2].Select();
        cursorP2.transform.position = ss[p2].gameObject.transform.position + new Vector3(0, 30, 0);
        //左向きキーを入力 
		if (Input.GetKeyDown(KeyCode.S) && selected2 == false)
        {
            p2--;

            if (p2 <= -1)
            {
                p2 = 2;
            }

        }

        //右向きキーを入力
		if (Input.GetKeyDown(KeyCode.X) && selected2 == false)
        {
            p2++;

            if (p2 >= 3)
            {
                p2 = 0;
            }

        }

        //p1とp2のカーソルが重なった時に少し上にずらす
        if(p1 == p2)
        {
            cursorP1.transform.position = ss[p1].gameObject.transform.position + new Vector3(0, 50, 0);
        }

        //プレイヤーそれぞれのスキル選択決定とそのキャッシュの保存
        //１プレイヤーがスキルを選択
    if (Input.GetButtonDown("Skill1Key") && selected1 == false)
        {
            //ボタン押した時にSE
            GetComponent<AudioSource>().Play();

//            //p1のスキルキャンセル処理
//            if (Input.GetButtonDown("Skill1Key"))
//            {
//				selected1 = false;
//                CacheData.Entity.P1Skill = -1;
//            }

                if (p1 == 0 )
            {
				skill1.image.color = new Color (255, 0, 0);
				selected1 = true;
                CacheData.Entity.P1Skill = 0;
            }

            else if (p1 == 1)
            {
				skill2.image.color = new Color (0, 255, 0);
				selected1 = true;
                CacheData.Entity.P1Skill = 1;
            }

            else if (p1 == 2)
            {
				skill3.image.color = new Color (0, 0, 255);
				selected1 = true;
                CacheData.Entity.P1Skill = 2;
            }
		}else if(Input.GetButtonDown("Skill1Key") && selected1 == true){
			selected1 = false;
			CacheData.Entity.P1Skill = -1;
		}
        //2プレイヤーがスキルを選択
	if(Input.GetButtonDown("Skill2Key") && selected2 == false)
        {

            //ボタン押した時にSE
            GetComponent<AudioSource>().Play();
            //            //p2のスキルキャンセル処理
            //			if (Input.GetButtonDown("Skill2Key"))
            //            {
            //				selected2 = false;
            //                CacheData.Entity.P2Skill = -1;
            //            }

            if (p2 == 0)
            {
				skill1.image.color = new Color (255, 0, 0);
				selected2 = true;
                CacheData.Entity.P2Skill = 0;
            }

           else if (p2 == 1)
            {
				skill2.image.color = new Color (0, 255, 0);
				selected2 = true;
                CacheData.Entity.P2Skill = 1;
            }

           else if (p2 == 2)
            {
				skill3.image.color = new Color (0, 0, 255);
				selected2 = true;
                CacheData.Entity.P2Skill = 2;
               
            }
		}else if(Input.GetButtonDown("Skill2Key") && selected2 == true){
			selected2 = false;
			CacheData.Entity.P2Skill = -1;
		}


        //p1とp2が二人とも選択した際にGame_Masterに画面遷移
        if (CacheData.Entity.P1Skill != -1 && CacheData.Entity.P2Skill != -1)
        {
            SceneManager.LoadScene("Game_Master");
        }

    }





}
