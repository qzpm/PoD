using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBallComponent : MonoBehaviour {

    // 0 = 中立、1 = Player1、2 = Player2
    public int colorId = 0;
    Color[] ballColor = { Color.white, Color.red, Color.green };

	// Use this for initialization
	void Start () {
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.AddForce(Vector3.right * 5, ForceMode.VelocityChange);
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void PlaySound() { 
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void ChangeColor(int newColorId)
    {
        colorId = newColorId;
        //ここでボールの色変え
        GetComponent<Renderer>().material.color = ballColor[colorId];
    }

    public float GetVelocity() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        return rigidbody.velocity.magnitude;
    }

    public void Accell(float bouncy) {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Debug.Log(rigidbody.velocity);
        Vector3 newVelocity = rigidbody.velocity;
        if (rigidbody.velocity.magnitude < 10f)
        {
            newVelocity = newVelocity * 10 / rigidbody.velocity.magnitude;
        } else if(rigidbody.velocity.magnitude > 35f) {
            newVelocity = newVelocity * 35 / rigidbody.velocity.magnitude;
        }
        newVelocity *= bouncy;
        rigidbody.velocity = newVelocity;
    }
}


