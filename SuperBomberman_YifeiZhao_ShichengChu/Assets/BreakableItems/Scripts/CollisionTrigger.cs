using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class CollisionTrigger : MonoBehaviour
{
	
		public	GameObject particleSmoke;
		public  GameObject particleExplosion;
		public 	AudioClip ExplosionSound;
		private Vector3 lastSoundPos;
		private bool Coll = false;
		private int isExplosion = 0;
		private float rbSpeed = 0;
		void Start ()
		{
				float weight;
				if (ExplosionSound)
						this.GetComponent<AudioSource>().clip = ExplosionSound;
		
				foreach (Rigidbody RigidB in GameObject.FindObjectsOfType(typeof(Rigidbody))) {
	 
						BoxCollider Box = RigidB.GetComponent <BoxCollider> ();
						RigidB.name = RigidB.GetInstanceID ().ToString ();
						RigidB.Sleep ();
				
						Box.size = Box.size / 1.5f;
						weight = Box.size.x + Box.size.y + Box.size.z;
						if (weight == 0) {
								Destroy (RigidB);
						} else
								RigidB.GetComponent<Rigidbody>().mass = weight * 10.0f;

				}	
				InvokeRepeating ("checkRigidBodySpeed", 0.5f, 1.0f);
		}
 
		void checkRigidBodySpeed ()
		{
				foreach (Rigidbody RigidB in GameObject.FindObjectsOfType(typeof(Rigidbody))) {
			
						rbSpeed = Vector3.Distance (new Vector3 (0, 0, 0), RigidB.velocity);
						if (rbSpeed < 0.2f)
								RigidB.Sleep ();
				}
		}

		void OnCollisionStay (Collision other)
		{ 
				if (other.collider.tag.ToString () == "Explosion") 
						this.GetComponent<Animation>().Play ();
				isExplosion = 10;
		}

		void OnCollisionExit (Collision other)
		{ 
				Coll = false;
				isExplosion = 0;
		} 
	
	
		void  OnCollisionEnter (Collision other)
		{ 

				if (other.collider.tag.ToString () == "Explosion") {
						Dest (other.collider.gameObject.name);
						this.GetComponent<Animation>().Play ();
						if (this.ExplosionSound) {
								float soundDist = Vector3.Distance (this.gameObject.GetComponent<Rigidbody>().position, lastSoundPos);
								if (!this.gameObject.GetComponent<AudioSource>().isPlaying || soundDist > 10.0) {
										this.GetComponent<AudioSource>().Play ();
										lastSoundPos = this.gameObject.GetComponent<Rigidbody>().position;
										StartCoroutine (Wait ());
								}
						}
			 
						if (particleExplosion && !Coll && isExplosion == 0) {
								GameObject Explosion = (GameObject)Instantiate (particleExplosion, other.contacts [0].point, Quaternion.identity);
								this.isExplosion++;
						}
						/*	if (other.collider.gameObject.GetComponent <TimedObjectDestructor> () == null)
								other.collider.gameObject.AddComponent <TimedObjectDestructor> ();*/
						Coll = true;
				}
		}

		IEnumerator Wait ()
		{
				yield return new WaitForSeconds (this.GetComponent<AudioSource>().clip.length / 10.0f);
		}

		void Dest (string Obj)
		{
		
				Rigidbody RigidObj = GameObject.Find (Obj).GetComponent <Rigidbody> ();
				if (RigidObj && Coll == true) {
						RigidObj.isKinematic = false;
						RigidObj.WakeUp (); 
						OnRigidBody (RigidObj);
				}
		}

		void OnRigidBody (Rigidbody RB)
		{
				if (Coll == true) {
						if (particleSmoke && RB.name.IndexOf ("particle") == -1) {
								GameObject parent = (GameObject)Instantiate (particleSmoke, new Vector3 (0, 0, 0), Quaternion.identity);
								RB.name = RB.name + "particle";
								parent.transform.parent = RB.transform;
								parent.transform.localPosition = Vector3.zero;
								parent.transform.localRotation = Quaternion.EulerRotation (0, 0, 0);
						}
				}
		}
}
