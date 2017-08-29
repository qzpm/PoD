using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartsArrowComponent : MonoBehaviour {
    Rigidbody rigid;
    bool stick = false;

    public AudioSource stunSound;
    public AudioSource stealSound;

    /* ダーツ効果
     * -1 = なにもなし
     * 0 = PlayerCmponentのStunを実行
     * 1 = AreaComponentのChangeWallColorを実行、プレイヤー1の色で上書き
     * 2 = AreaComponentのChangeWallColorを実行、プレイヤー2の色で上書き
     */
    int effect = -1;
    
    //ダーツが刺さり続ける時間
    float stickTime = 3f;
    float sticking = 0;
    
	void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.AddForce(Vector3.down * 50, ForceMode.VelocityChange);
    }

    public void Init(Vector3 shotPosition, int type) {
        effect = type;
        transform.position = shotPosition;
    }

    void Update () {
        if (stick) {
            sticking += Time.deltaTime;
            if(sticking > stickTime) {
                Eject();
            }
        } else { 
            if(transform.position.z < -20) {
                Destroy(this.gameObject);
            }
        }
	}

    private void OnCollisionEnter(Collision collision) {
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = true;
        stick = true;
        if(effect == 0) {
            PlayerComponent enemy = collision.gameObject.GetComponent<PlayerComponent>();
            if(enemy != null) {
                transform.position = new Vector3(transform.position.x, 5, transform.position.z);
                enemy.Stun();
                stunSound.Play();
            }
        } else if(effect != -1) {
            Transform wallObject = collision.gameObject.transform.parent.Find("Wall");
            if(wallObject != null) {
                WallComponent wall = wallObject.GetComponent<WallComponent>();
                if(wall != null) {
                    wall.UpdateWall(effect);
                    stealSound.Play();
                }
            }
        }
        effect = -1;
    }

    void Eject() {
        stick = false;
        rigid.isKinematic = false;
        rigid.AddTorque(Vector3.left * 5, ForceMode.VelocityChange);
        rigid.AddForce(Vector3.back * 20, ForceMode.VelocityChange);
    }
}
