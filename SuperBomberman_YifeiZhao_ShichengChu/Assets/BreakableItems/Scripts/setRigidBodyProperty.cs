using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (Rigidbody))]

public class setRigidBodyProperty : MonoBehaviour {
public rigidBodyProperty myRigidBodyProperty;

	void Awake(){

			if (this.tag.Contains ("WallLayer")) {
				this.GetComponent<Rigidbody>().name =this.GetComponent<Rigidbody>().GetInstanceID ().ToString ();
				this.GetComponent<Rigidbody>().Sleep ();
			}
		}
}
