using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBase : MonoBehaviour {

    public GameObject mesh;
    public float length = 10f;

	void Start () {
        mesh.transform.localScale = new Vector3(0.3f, length, 1f);
        mesh.transform.localPosition = new Vector3(0, length / 2, 0);
	}
	
	void Update () {
		
	}
}
