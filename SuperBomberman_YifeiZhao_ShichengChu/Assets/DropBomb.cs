using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour {

    public int bombNum = 3;
    public GameObject bomb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop();
        }
	}
    
    public void Drop()
    {
        if (bombNum >= 0)
        {
            Vector3 t = new Vector3(0.0f,2.0f,0);
            Instantiate(bomb, transform.position+t, transform.rotation);
            bombNum--;
        }
    }

    public void Reload()
    {
        bombNum++;
    }
}
