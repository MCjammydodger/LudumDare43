using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPlatform : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.transform.parent = null;
        }
    }
}
