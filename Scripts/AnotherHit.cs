using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherHit : MonoBehaviour {
    private void OnTriggerExit(Collider other)
    {
        AnotherKey key = other.GetComponent<AnotherKey>();

        if (key != null)
        {
            key.Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AnotherKey key = other.GetComponent<AnotherKey>();

        if (key != null)
        {
            key.readyToScore = true;
        }
    }
}
