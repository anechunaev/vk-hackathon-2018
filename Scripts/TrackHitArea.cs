using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackHitArea : MonoBehaviour {
    private void OnTriggerExit(Collider other)
    {
        KeyBase key = other.GetComponent<KeyBase>();

        if (key != null)
        {
            key.Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        KeyBase key = other.GetComponent<KeyBase>();

        if (key != null)
        {
            key.readyToScore = true;
        }
    }
}
