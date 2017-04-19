using UnityEngine;
using System.Collections;

public class TimedObjectDestructor: MonoBehaviour
{
		float timeOut = 5.0f;
		public bool detachChildren = false;
	
		void Awake ()
		{
				Invoke ("DestroyNow", timeOut);
		}
	
		void DestroyNow ()
		{
				if (detachChildren) {
						this.transform.DetachChildren ();
				}
				DestroyObject (gameObject);
		}
}
