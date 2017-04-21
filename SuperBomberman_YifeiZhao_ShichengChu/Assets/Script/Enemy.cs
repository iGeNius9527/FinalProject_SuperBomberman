using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public GameObject player;
    public NavMeshAgent nav;
    public int Health = 4;
    private string state = "idle";
    private bool alive = true;
    public Transform eyes;
    public AudioSource roar;
    public Animator anim;
    private float wait = 0f;
    private bool highAlert = false;
    private float alertness = 20f;
    private float deathTime = 3f;

    // Use this for initialization
    void Start() {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        nav.speed = 1.2f;
        anim.speed = 1.2f;
    }

    //check the player
    public void checkSight() {
        if (alive) {
            RaycastHit rayHit;
            if (Physics.Linecast(eyes.position, player.transform.position, out rayHit)) {
                print("hit " + rayHit.collider.gameObject.name);
                if (rayHit.collider.gameObject.name == "Player") {
                    if (state != "kill") {
                        state = "chase";
                        nav.speed = 3.0f;
                        anim.speed = 3.0f;
                        roar.pitch = 1.2f;
                        roar.Play();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        anim.SetFloat("velocity", nav.velocity.magnitude);
        anim.SetBool("alive", alive);
        if (alive)
        {
            if (state == "idle")
            {
                Vector3 RandomPos = Random.insideUnitCircle * alertness;
                NavMeshHit NavHit;
                NavMesh.SamplePosition(transform.position + RandomPos, out NavHit, 20f, NavMesh.AllAreas);
                nav.SetDestination(NavHit.position);
                state = "walk";
                if (highAlert)
                {
                    NavMesh.SamplePosition(player.transform.position + RandomPos, out NavHit, 20f, NavMesh.AllAreas);
                    alertness += 5f;
                    if (alertness > 20f)
                    {
                        highAlert = false;
                        alertness = 20f;
                        nav.speed = 1.2f;
                        anim.speed = 1.2f;
                    }
                }
            }
            if (state == "walk")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                }
            }
            if (state == "search")
            {
                if (wait > 5f)
                {
                    wait -= Time.deltaTime;
                    transform.Rotate(0f, 120f * Time.deltaTime, 0f);
                }
                else
                {
                    state = "idle";
                }
            }
            if (state == "chase")
            {
                nav.destination = player.transform.position;

                //lose sight of player
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > 10f)
                {
                    state = "hunt";
                }
                if (distance < 2f)
                {
                    player.GetComponent<UnityChanControlScriptWithRgidBody>().TakeDamage();
                }
            }
            if (state == "hunt")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                    highAlert = true;
                    alertness = 5f;
                    checkSight();
                }
            }
        }
        else {
            if (deathTime > 0f)
            {
                deathTime -= Time.deltaTime;
            }
            else {
                Destroy(this.gameObject);
            }
        }
     
		//nav.SetDestination (player.transform.position);
	}
    public void TakeDamage()
    {
        Health--;
        if(Health<=0)alive = false;
        
    }
}
