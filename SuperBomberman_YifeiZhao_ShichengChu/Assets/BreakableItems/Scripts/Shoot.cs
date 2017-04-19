using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{

		public Transform bullet;
		public float bulletSpeed = 0.3333f;
		public float  BulletRange = 20.0f;
		private  Vector3 BulletVect = new Vector3 (0, 0, 0); 
		private  float distance;
		private Transform inst;	
		void Start ()
		{
				inst = (Transform)Instantiate (bullet);
				bullet = inst;
		}

		void LateUpdate ()
		{
				if (Input.GetButtonDown ("Fire1")) {
				 
						bullet.transform.position = this.transform.position;
						BulletVect = transform.forward;
				}
				distance = Vector3.Distance (this.transform.position, bullet.transform.position); 
				if (BulletRange > distance) {
						bullet.Translate (BulletVect * bulletSpeed);
			
				} else
						bullet.transform.position = new Vector3 (bullet.transform.position.x, -2.0f, bullet.transform.position.z);
		}
}
