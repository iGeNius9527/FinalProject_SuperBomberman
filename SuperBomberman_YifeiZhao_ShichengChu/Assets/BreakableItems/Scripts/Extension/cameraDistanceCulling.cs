using UnityEngine;
using System.Collections;

public class cameraDistanceCulling : MonoBehaviour
{
	

		public float range = 100.0f;
		private float dist = 0.0f;
		private bool isRigidBodySleep = true;
		private int isMeshHide = 1;
		private Component[] comps;
		public Camera mainCamera;
		void Update ()
		{
				StartCoroutine (visibleCheck ());
		}
		IEnumerator visibleCheck ()
		{
				dist = Vector3.Distance (this.transform.position, mainCamera.transform.position);
				if (dist < range) {
						if (isMeshHide == 0) {

								StartCoroutine (meshShow ());	
								isMeshHide = 1;
						
								if (isRigidBodySleep) {	
										StartCoroutine (wallLayerSleep ());
										isRigidBodySleep = false;
								}
						}

				} else if (isMeshHide == 1) {
						isMeshHide = 0;

						StartCoroutine (meshHide ());	
		
						isRigidBodySleep = true;
				}	 
				yield return null;
		}
 
		void OnDrawGizmosSelected ()
		{
				Gizmos.color = Color.white;
				Gizmos.DrawWireSphere (transform.position, range);
		}
		IEnumerator wallLayerSleep ()
		{
				foreach (Rigidbody RigidB in GameObject.FindObjectsOfType(typeof(Rigidbody))) {
						if (RigidB.gameObject.tag.Contains ("WallLayer")) {
								RigidB.name = RigidB.GetInstanceID ().ToString ();
								RigidB.Sleep ();
						}
				}
				yield return null;
		}

		IEnumerator meshHide ()
		{

				comps = this.GetComponentsInChildren<MeshRenderer> ();
				foreach (MeshRenderer comp in comps) {
						comp.GetComponent<Renderer>().enabled = false;
				}
				comps = this.GetComponentsInChildren<ParticleSystem> ();
				foreach (ParticleSystem comp in comps) {
						comp.GetComponent<ParticleSystem>().enableEmission = false;
				}
				/*
			comps = this.GetComponentsInChildren<BoxCollider>();
			foreach (BoxCollider comp in comps) {
				comp.collider.enabled=false;
			}
			comps = this.GetComponentsInChildren<MeshCollider>();
			foreach (MeshCollider comp in comps) {
				comp.collider.enabled=false;
			}
			comps = this.GetComponentsInChildren<SphereCollider>();
			foreach (SphereCollider comp in comps) {
			comp.collider.enabled=false;
			}
			comps = this.GetComponentsInChildren<CapsuleCollider>();
			foreach (CapsuleCollider comp in comps) {
			comp.collider.enabled=false;
			}
	*/
				yield return null;
		}


		IEnumerator meshShow ()
		{
				comps = this.GetComponentsInChildren<MeshRenderer> ();
				foreach (MeshRenderer comp in comps) {
						comp.GetComponent<Renderer>().enabled = true;
				}
				comps = this.GetComponentsInChildren<ParticleSystem> ();
				foreach (ParticleSystem comp in comps) {
						comp.GetComponent<ParticleSystem>().enableEmission = true;
				}
				/*
		comps = this.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider comp in comps) {
			comp.collider.enabled=true;
		}
		comps = this.GetComponentsInChildren<MeshCollider>();
		foreach (MeshCollider comp in comps) {
			comp.collider.enabled=true;
		}
		comps = this.GetComponentsInChildren<SphereCollider>();
		foreach (SphereCollider comp in comps) {
			comp.collider.enabled=false;
		}
		comps = this.GetComponentsInChildren<CapsuleCollider>();
		foreach (CapsuleCollider comp in comps) {
			comp.collider.enabled=false;
		}*/
				yield return null;
		}
}

