using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherKey : MonoBehaviour
{

    public int type = 1;
    public float speed = 1f;
    public ParticleSystem goodHitPS;
    public ParticleSystem diePS;
    public bool readyToScore = false;

    void FixedUpdate()
    {
        //transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);
        transform.Translate(Vector3.back * Time.fixedDeltaTime * speed);
    }

    private void Update()
    {
        if (transform.position.y < -1f)
        {
            this.Die();
        }
    }

    public void BadHit()
    {

    }

    public void GoodHit()
    {
        speed = 0;
        ParticleSystem ps = Instantiate(goodHitPS, transform);
        ps.transform.parent = null;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
		transform.position = new Vector3(-999f, -999f, -999f);
    }

    public void Die()
    {
        speed = 0;
        //ParticleSystem ps = Instantiate(diePS, transform);
        //ps.transform.parent = null;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        transform.position = new Vector3(-999f, -999f, -999f);
    }
}
