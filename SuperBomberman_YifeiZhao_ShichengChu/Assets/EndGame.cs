using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour {
    public GameObject player;
    public GameObject boss;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UnityChanControlScriptWithRgidBody p = player.GetComponent<UnityChanControlScriptWithRgidBody>();
        Enemy b = boss.GetComponent<Enemy>();
        bool ifEnd = false;
        if (p.health<=0 || b.Health<= 0)
        {
            ifEnd = true;
        }
        if (ifEnd)
        {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene("BasicScene");
            //Debug.Log("p: " + p.health);
            //Debug.Log("boss: " + b.Health);
        }
	}
}
