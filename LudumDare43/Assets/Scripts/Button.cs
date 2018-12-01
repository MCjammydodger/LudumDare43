using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour {

    public UnityEvent OnButtonPressed;

    private void OnCollisionEnter(Collision collision)
    {
        if(OnButtonPressed!=null)
        {
            OnButtonPressed.Invoke();
        }
    }
}
