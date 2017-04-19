using UnityEngine;
using System.Collections;

public class activatedRigidBody : MonoBehaviour
{
	
		void OnEnable ()
		{
				foreach (Rigidbody RigidB in this.transform.GetComponentsInChildren(typeof(Rigidbody))) {

						BoxCollider Box = RigidB.GetComponent <BoxCollider> ();
						RigidB.name = RigidB.GetInstanceID ().ToString ();
						RigidB.Sleep ();
						Box.size = Box.size / 1.5f;
						float weight = Box.size.x + Box.size.y + Box.size.z;
						if (weight == 0) {
								Destroy (RigidB);
						} else
								RigidB.GetComponent<Rigidbody>().mass = weight * 50.0f;
						 
				}	 
		}
}