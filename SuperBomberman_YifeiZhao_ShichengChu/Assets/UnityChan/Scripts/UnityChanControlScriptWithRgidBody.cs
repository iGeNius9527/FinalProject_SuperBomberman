using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class UnityChanControlScriptWithRgidBody : MonoBehaviour
{
    private float deathTime = 3f;
    public float animSpeed = 1.5f;				
	public bool useCurves = true;				
	public float useCurvesHeight = 0.5f;		
    private bool alive = true;

	public float forwardSpeed = 7.0f;
	
	public float backwardSpeed = 2.0f;
	private CapsuleCollider col;
	private Rigidbody rb;

	private Vector3 velocity;
	
	private float orgColHight;
	private Vector3 orgVectColCenter;
	
	private Animator anim;							
	private AnimatorStateInfo currentBaseState;			

	private GameObject cameraObject;	
		

	static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int restState = Animator.StringToHash("Base Layer.Rest");

	void Start ()
	{
		anim = GetComponent<Animator>();
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
		orgColHight = col.height;
		orgVectColCenter = col.center;
}

	void FixedUpdate ()
	{
        if (alive)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            anim.SetFloat("Direction", h);
            anim.speed = animSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            rb.useGravity = true;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 270f, 0));
            }

            velocity = new Vector3(0, 0, Mathf.Abs(v) + Mathf.Abs(h));
            anim.SetFloat("Speed", velocity.magnitude);

            velocity = transform.TransformDirection(velocity);
            velocity *= forwardSpeed;
            transform.localPosition += velocity * Time.fixedDeltaTime;
            if (currentBaseState.nameHash == locoState)
            {
                if (useCurves)
                {
                    resetCollider();
                }
            }
            else if (currentBaseState.nameHash == idleState)
            {
                if (useCurves)
                {
                    resetCollider();
                }
                if (Input.GetButtonDown("Jump"))
                {
                    anim.SetBool("Rest", true);
                }
            }
            else if (currentBaseState.nameHash == restState)
            {
                if (!anim.IsInTransition(0))
                {
                    anim.SetBool("Rest", false);
                }
            }
        }
        else {
            anim.SetBool("alive",alive);
            if (deathTime > 0.0f)
            {
                deathTime -= Time.deltaTime;
            }else
            {
                Destroy(this.gameObject);
            }
        }
        
	}

	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width -260, 10 ,250 ,150), "Interaction");
		GUI.Label(new Rect(Screen.width -245,30,250,30),"Up/Down Arrow : Go Forwald/Go Back");
		GUI.Label(new Rect(Screen.width -245,50,250,30),"Left/Right Arrow : Turn Left/Turn Right");
		GUI.Label(new Rect(Screen.width -245,70,250,30),"Hit Space key while Running : Drop bomb");
		GUI.Label(new Rect(Screen.width -245,90,250,30),"The total number of bomb can be four.");

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "eyes") {
			other.transform.parent.GetComponent<Enemy> ().checkSight();
		}
	}


	void resetCollider()
	{
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}

    public void TakeDamage()
    {
        //alive = false;
    }
}
