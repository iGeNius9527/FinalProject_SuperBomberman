using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Animator anim;
	private float inputH;
	private float inputV;
	public Rigidbody rbody;
	private bool run;
	private bool jump;
	public float forwardSpeed = 2.0f;
	public float backwardSpeed = 1.0f;
	public float rotateSpeed = 2.0f;
	public float jumpPower = 3.0f; 
	public float animSpeed = 1.5f;
	public float runspeed = 10f;
	public float walkspeed = 3f;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
		run = false;
		jump = false;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "eyes") {
			other.transform.parent.GetComponent<Enemy> ().checkSight();
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {
			anim.Play ("WAIT01",-1,0f);
		}
		if (Input.GetKeyDown ("2")) {
			anim.Play ("WAIT02",-1,0f);
		}
		if (Input.GetKeyDown ("3")) {
			anim.Play ("WAIT03",-1,0f);
		}
		if (Input.GetKeyDown ("4")) {
			anim.Play ("WAIT04",-1,0f);
		}
		if (Input.GetKeyDown ("5")) {
			anim.Play ("WAIT05",-1,0f);
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			anim.Play ("JUMP01", -1, 0f);
		}
		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");
		anim.speed = animSpeed;	

		anim.SetFloat ("inputH",inputH);
		anim.SetFloat ("inputZ",inputV);
		anim.SetBool ("run", run);
		anim.SetBool ("jump", jump);

		velocity = new Vector3(0, 0, inputV);
		velocity = transform.TransformDirection(velocity);
		if (inputV> 0.1) {
			velocity *= forwardSpeed;	
		} else if (inputV < -0.1) {
			velocity *= backwardSpeed;
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			run = true;
			forwardSpeed = runspeed;
		} else {
			run = false;
			forwardSpeed = walkspeed;
		}
		if (Input.GetKey (KeyCode.Space)) {
			jump = true;
			//rbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
		} else {
			jump = false;
		}

		transform.localPosition += velocity * Time.fixedDeltaTime;
		anim.SetFloat ("speed", velocity.magnitude);

		transform.Rotate(0, inputH * rotateSpeed, 0);

	}
}
