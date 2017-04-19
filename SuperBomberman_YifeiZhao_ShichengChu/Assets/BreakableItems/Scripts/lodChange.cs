using UnityEngine;
using System.Collections;

public class lodChange : MonoBehaviour {
	public GameObject fragmentObjects;
	void Start()

	{
		fragmentObjects.SetActive(false);
	}
	
	void OnTriggerEnter(Collider other)
	{
	 	if (other.tag.Contains ("Collider")){
			fragmentObjects.SetActive(true);
			this.gameObject.SetActive(false);
		}
	}
 
}
