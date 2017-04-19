using UnityEngine;
using System.Collections;

public class objectOcclusionCulling : MonoBehaviour
{
	

		public float shadowDist = 10.0f;
		private Component[] comps;
		private float dist = 0;
		private int cnt = 0;
		public Camera mainCamera;
		void Awake ()
		{
				comps = this.GetComponentsInChildren<MeshRenderer> ();
		}
 
		void LateUpdate ()
		{
				StartCoroutine (checkVisible ());
		}
		IEnumerator checkVisible ()
		{
				cnt = 0;
				while (cnt<comps.Length) {
						dist = Vector3.Distance (comps [cnt].transform.position, mainCamera.transform.position);
						if (comps [cnt].GetComponent<Renderer>().IsVisibleFrom (mainCamera)) {
								comps [cnt].gameObject.SetActive (true);
						} else {
								comps [cnt].gameObject.SetActive (false);
								//	Debug.Log (comps [cnt].name);
						}
						cnt++;
				}
		 
				yield return null;
		}
}
