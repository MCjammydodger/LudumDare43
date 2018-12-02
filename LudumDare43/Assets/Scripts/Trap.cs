using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    [SerializeField]
    private Transform trappedPos;

    private Critter trappedCritter;

    private void OnTriggerEnter(Collider other)
    {
        Critter critter = other.GetComponent<Critter>();
        if(critter != null && critter.IsBeingThrown())
        {
            Free();
            trappedCritter = critter;
            trappedCritter.transform.parent = trappedPos;
            trappedCritter.Trap();
            trappedCritter.transform.position = trappedPos.position;
            trappedCritter.transform.rotation = trappedPos.rotation;
        }
    }

    private void Free()
    {
        if(trappedCritter != null)
        {
            trappedCritter.transform.parent = null;
            trappedCritter.Free();
        }
    }

    public void DisableTrap()
    {
        Free();
        gameObject.SetActive(false);
    }
}
