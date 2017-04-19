using UnityEngine;
using System.Collections;

[System.Serializable]
public class cameraDistance
{
		public LayerMask mask = -1;
		public float distance = 0f;

}
public class cameraLayerDistance : MonoBehaviour
{
		float[] distances = new float[32];
		public cameraDistance[] layerDistance;

		void Start ()
		{
				/*	myBloom = this.transform.GetComponent ("BloomAndLensFlares") as BloomAndLensFlares;

				if (Settings.GFXQuality == true) {
						myBloom.enabled = false;
				} else 
						myBloom.enabled = true; */
				//this.camera.layerCullSpherical = true;
				for (int i=0; i<layerDistance.Length; i++) {
						for (int layer = 0; layer < 32; layer++) {
								if ((layerDistance [i].mask.value & 1 << layer) != 0) {
										distances [layer] = layerDistance [i].distance;
#if DEBUG
										Debug.Log ("Select Layer :" + layer);
#endif
								}
						}
				}
				GetComponent<Camera>().layerCullDistances = distances;
		}

		/*
	void Update ()
	{
		
		for (int i=0; i<layerDistance.Length; i++) {
			for (int layer = 0; layer < 32; layer++) {
				if ((layerDistance [i].mask.value & 1 << layer) != 0) {
					distances [layer] = layerDistance [i].distance;
					#if DEBUG
					//Debug.Log ("Select Layer :" + layer);
					#endif
				}
			}
		}
		camera.layerCullDistances = distances;
	}*/
}
