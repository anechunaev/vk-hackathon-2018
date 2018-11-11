using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBase : MonoBehaviour {

    public int type = 1;
    public Material[] materials;
    public float speed = 1f;
    public ParticleSystem goodHitPS;
    public ParticleSystem diePS;
    public bool readyToScore = false;

	void Start () {
        gameObject.GetComponent<MeshRenderer>().material = materials[type];
	}
	
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    public void BadHit ()
    {

    }

    public void GoodHit ()
    {
        speed = 0;
        //ParticleSystem ps = Instantiate(goodHitPS, transform);
        //ps.transform.parent = null;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    public void Die ()
    {
        speed = 0;
        //ParticleSystem ps = Instantiate(diePS, transform);
        //ps.transform.parent = null;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
