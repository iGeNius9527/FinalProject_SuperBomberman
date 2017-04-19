using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float radius = 20.0F;
    public float power = 10.0F;
    public GameObject bomb;
    public GameObject flame;
    int timer;
    float step;
    int sign;
    float v;
    GameObject origin;
    // Use this for initialization
    void Start () {
        timer = 0;
        step = 0.001f;
        sign = 1;
        v = 0;
        origin = GameObject.Find("BombBall");
    }
	
	// Update is called once per frame
	void Update () {
        if (timer < 300)
        {
            GameObject bomb = GameObject.Find("BombBall");
            float x = origin.transform.localScale.x;
            x = x * (1.0f + Mathf.Sin(v));

            float y = origin.transform.localScale.y;
            y = y * (1.0f + Mathf.Sin(v));

            float z = origin.transform.localScale.z;
            z = z * (1.0f + Mathf.Sin(v));
            bomb.transform.localScale = new Vector3(x,y,z);
        }
        else if (timer == 300)
        {
            Explode();
        }

        timer++;
        if (v > 0.015f)
        {
            sign = -1;
        }
        else if (v < -0.015f)
        {
            sign = 1;
        }
        v += step*sign;


	}
    void Explode()
    {


        Vector3 explosionPos = bomb.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            BreakableCube brkCube = hit.GetComponent<BreakableCube>();
            if(brkCube != null)
            {
                brkCube.TakeDamage();
            }
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Impulse);
            }
        }

        Instantiate(flame, bomb.transform.position, bomb.transform.rotation);
        Destroy(bomb);

    }
}
