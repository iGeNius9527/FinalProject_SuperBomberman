using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour {
    private float lifeSpan = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (lifeSpan < 0.0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifeSpan -= Time.deltaTime;
        }
	}
}
