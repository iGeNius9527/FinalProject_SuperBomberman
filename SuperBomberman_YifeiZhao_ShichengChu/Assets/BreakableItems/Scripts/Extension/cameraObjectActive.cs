using UnityEngine;
using System.Collections;

public class cameraObjectActive : MonoBehaviour
{
	

		public float range = 100.0f;
		private float dist = 0.0f;
		private bool isRigidBodySleep = true;
		private int isMeshHide = 1;
		private Object[] myRigidBody;
		private float rbSpeed = 0;


		void Awake ()
		{
				myRigidBody = GameObject.FindObjectsOfType (typeof(Rigidbody));
				InvokeRepeating ("visibleCheck", 0.0f, 0.5f);
		}

		void visibleCheck ()
		{
				foreach (Rigidbody RigidB in myRigidBody) { 
				 
						wallLayerSleep (RigidB);
												 
				}
		}
  
		void wallLayerSleep (Rigidbody RigidB)
		{
				if (RigidB.gameObject.tag.Contains ("WallLayer")) {
						RigidB.name = RigidB.GetInstanceID ().ToString ();
						rbSpeed = Vector3.Distance (new Vector3 (0, 0, 0), RigidB.velocity);
						if (rbSpeed < 0.1f)
								RigidB.Sleep ();
				}
		}
}

