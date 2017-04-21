using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float radius = 0.5F;
    public float power = 5.0F;

    
    public GameObject flame;
    float timer;
    float colliderTimer;
    float step;
    int sign;
    float v;

    // Use this for initialization
    void Start () {
        timer = 3.5f;
        step = 0.001f;
        sign = 1;
        v = 0;
        colliderTimer = 0.5f;
        this.gameObject.GetComponent<Collider>().enabled = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (timer > 0.0f)
        {
            
            float x = 1.0f;
            x = x * (1.0f + Mathf.Sin(v));

            float y = 1.0f;
            y = y * (1.0f + Mathf.Sin(v));

            float z = 1.0f;
            z = z * (1.0f + Mathf.Sin(v));
            transform.localScale = new Vector3(x,y,z);
            timer -= Time.deltaTime;
        }
        else if (timer < 0.0f)
        {
            Explode();
        }
        
        if (v > 0.015f)
        {
            sign = -1;
        }
        else if (v < -0.015f)
        {
            sign = 1;
        }
        v += step*sign;

        if (colliderTimer > 0)
        {
            colliderTimer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.GetComponent<Collider>().enabled = true;
        }

	}
    void Explode()
    {

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            BreakableCube brkCube = hit.GetComponent<BreakableCube>();
            Enemy e = hit.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage();
            }
            Boss b = hit.GetComponent<Boss>();
            if (b != null)
            {
                b.TakeDamage();
            }
            if (brkCube != null)
            {
                brkCube.TakeDamage();
            }
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Impulse);
            }
        }

        Instantiate(flame, transform.position, transform.rotation);
        Destroy(this.gameObject);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<DropBomb>().Reload();
    }
}
