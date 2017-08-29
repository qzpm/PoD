using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaComponent : MonoBehaviour {
    public GameObject skillDarts;

    int[] wallColorId = new int[10];

    private Material[] groundMaterials = new Material[3];
    private Material[] wallMaterials = new Material[3];

    // Use this for initialization
    void Start () {
		for(int i = 0; i < 5; i++) {
            wallColorId[i] = 1;
        }

        for (int i = 5; i < 10; i++) {
            wallColorId[i] = 2;
        }
        
        for (int i = 0; i < 3; i++) {
            groundMaterials[i] = Resources.Load("Materials/UV_" + i) as Material;
            wallMaterials[i] = Resources.Load("Materials/Wall_" + i) as Material;
        }

        for (int i = 0; i < 10; i++) {
            Transform wall = transform.Find("FieldWithLine_split10 (" + i + ")/Wall");
            WallComponent wcomp = wall.GetComponent<WallComponent>();
            wcomp.Init(this, i);
            wcomp.ChangeColor(groundMaterials[wallColorId[i]], wallMaterials[wallColorId[i]]);
        }

        CacheData.Entity.P1Area = 5;
        CacheData.Entity.P2Area = 5;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeWallColor(int wallId, int ballId) {
        int newWallColor = 0;
        int oldWallColor = wallColorId[wallId];

        if (ballId != 0) {
            if (oldWallColor != ballId) {
                newWallColor = ballId;
            }
        }
        wallColorId[wallId] = newWallColor;
        
        //ここで色の変化をキャッシュに反映させる
        if (oldWallColor != newWallColor) {
            if (oldWallColor == 1) {
                CacheData.Entity.P1Area -= 1;
            } else if (oldWallColor == 2) {
                CacheData.Entity.P2Area -= 1;
            }

            if (newWallColor == 1) {
                CacheData.Entity.P1Area += 1;
            } else if (newWallColor == 2) {
                CacheData.Entity.P2Area += 1;
            }
        }
        //(スキルゲージへの反映)

        Transform wall = transform.Find("FieldWithLine_split10 (" + wallId + ")/Wall");
        WallComponent wcomp = wall.GetComponent<WallComponent>();
        wcomp.ChangeColor(groundMaterials[newWallColor], wallMaterials[newWallColor]);
    }

    public void HitWall(int wallId, NewBallComponent ball) {
		int oldBallId = ball.colorId; 
        // 白い壁に当たった場合以外ボールの色変化
        if (wallColorId[wallId] != 0) {
            ball.ChangeColor(wallColorId[wallId]);
        }

        ChangeWallColor(wallId, oldBallId);
    }

    public void UpdateWall(int wallId, int playerId) {
        if(wallColorId[wallId] != playerId)
        {
            ChangeWallColor(wallId, playerId);
        }
    }

    public int AngleToAreaColor(float angle, int playerId) {
        if (angle < 0) {
            angle += 360;
        } else  if (angle >= 360) {
            angle -= 360;
        }

        int number = Mathf.FloorToInt(angle / 36);

        if(wallColorId[number] == 0) {
            ChangeWallColor(number, playerId);
        }

        return wallColorId[number];
    }

    public void ShotDarts(int dartsType, int playerColor) {
        //エネミー番号を取得
        int enemyColor = 0;
        if(playerColor == 1) {
            enemyColor = 2;
        } else {
            enemyColor = 1;
        }

        GameObject darts = Instantiate(skillDarts);
        DartsArrowComponent dartsComponent = darts.GetComponent<DartsArrowComponent>();
        if (dartsType == 0) {
            //スタン効果のダーツを射出
            Vector3 enemyPos = GameObject.Find("Player" + enemyColor).transform.position;
            dartsComponent.Init(enemyPos + Vector3.up * 30, 0);

        } else {
            //エリア強奪効果のダーツを射出
            int stealIndex = FindStealArea(enemyColor, playerColor);
            float angle = (stealIndex * 36 + 18) * Mathf.Deg2Rad;
            Vector3 dartsPos = new Vector3(Mathf.Sin(angle), 6, Mathf.Cos(angle)) * 5;
            dartsComponent.Init(dartsPos, playerColor);
            Debug.Log("[index, position] = " + stealIndex + ", " + dartsPos);
        }
    }

    public int FindStealArea(int stolenColor, int newColor) {
        //まず奪えるエリアの候補をリストアップ
        List<int> candidate = new List<int>();
        for(int i = 0; i< 10; i++) { 
            if(wallColorId[i] == stolenColor) {
                candidate.Add(i);
            }
        }

        int stolenIndex = Random.Range(0, candidate.Count);
        return candidate[stolenIndex];
        //ChangeWallColor(candidate[stolenIndex], newColor);
    }
}

