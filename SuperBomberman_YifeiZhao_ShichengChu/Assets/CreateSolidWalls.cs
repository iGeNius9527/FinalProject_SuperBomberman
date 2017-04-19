using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSolidWalls : MonoBehaviour {

    private float offset;
	// Use this for initialization
	void Start () {
        offset = 20.0f;
        GameObject origin = GameObject.Find("OriginalCube");
        GameObject rawCube = origin;
        GameObject columnCube = origin;
        for (int i = 0; i < 7; i++)
        {
            
            if (i != 0)
            {
                rawCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                rawCube.transform.position = new Vector3(origin.transform.position.x, origin.transform.position.y, origin.transform.position.z-i*offset);
                rawCube.transform.localScale = origin.transform.localScale;
                rawCube.name = "Cube" + i + "_" + 0;
                Renderer rend = rawCube.GetComponent<Renderer>();
                rend.enabled = true;
                Renderer originalRend = origin.GetComponent<Renderer>();
                originalRend.enabled = true;
                rend.material = originalRend.material;
            }
            
            for (int j = 1; j < 7; j++)
            {
                columnCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                columnCube.transform.position = new Vector3(origin.transform.position.x + j*offset, origin.transform.position.y, origin.transform.position.z - i * offset);
                columnCube.transform.localScale = origin.transform.localScale;
                columnCube.name = "Cube" + i + "_" + j;
                Renderer rend = columnCube.GetComponent<Renderer>();
                rend.enabled = true;
                Renderer originalRend = origin.GetComponent<Renderer>();
                originalRend.enabled = true;
                rend.material = originalRend.material;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
