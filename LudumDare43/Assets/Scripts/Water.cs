using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().Sink();
        }
        if(other.GetComponent<Critter>() != null)
        {
            other.GetComponent<Critter>().Sink();
        }
    }
}
