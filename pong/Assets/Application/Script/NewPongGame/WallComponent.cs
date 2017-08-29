using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallComponent : MonoBehaviour {

    private AreaComponent areacomponent;
    private int areaId;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(AreaComponent area, int number) {
        areacomponent = area;
        areaId = number;
    }

    public void ChangeColor(Material groundMaterial,Material wallMaterial) {
        MeshRenderer ground = transform.parent.Find("Ground").GetComponent<MeshRenderer>();
        ground.material = groundMaterial;
        MeshRenderer line = transform.parent.Find("Line").GetComponent<MeshRenderer>();
        line.material = wallMaterial;
        MeshRenderer wall = transform.GetComponent<MeshRenderer>();
        wall.material = wallMaterial;
    }

    private void OnCollisionEnter(Collision collision) {
        NewBallComponent ball = collision.gameObject.GetComponent<NewBallComponent>();
        if(ball != null) {
            areacomponent.HitWall(areaId, ball);
            ball.Accell(0.95f);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    public void UpdateWall(int playerColor) {
        areacomponent.UpdateWall(areaId, playerColor);
    }
}
