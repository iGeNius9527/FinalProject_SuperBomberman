using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainsScript : MonoBehaviour {
    public int lifeSpan;
    private int timer;
    public GameObject remains;
    // Use this for initialization
    void Start () {

     lifeSpan = 200;
     timer = 0;
}
	
	// Update is called once per frame
	void Update () {
        timer++;
        if (timer > lifeSpan)
        {
            Destroy(remains);
        }
	}
}
