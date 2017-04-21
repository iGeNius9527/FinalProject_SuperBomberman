using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsScript : MonoBehaviour {
    public float lifeSpan;
   
    // Use this for initialization
    void Start () {

     lifeSpan = 1.0f;
    
}
	
	// Update is called once per frame
	void Update () {
        
        if (lifeSpan < 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifeSpan -= Time.deltaTime;
        }
	}
}
