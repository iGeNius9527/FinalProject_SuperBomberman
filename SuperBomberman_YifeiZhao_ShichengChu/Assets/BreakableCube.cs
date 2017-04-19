using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCube : MonoBehaviour {

    public GameObject breakableCube;
    public GameObject remains;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage()
    {
        
        Instantiate(remains, breakableCube.transform.position, breakableCube.transform.rotation);
        Destroy(breakableCube);
    }
}
