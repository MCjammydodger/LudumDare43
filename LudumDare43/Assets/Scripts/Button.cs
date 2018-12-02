using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour {

    public UnityEvent OnButtonPressed;

    private float timeSinceLastPressed = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (timeSinceLastPressed > 1)
        {
            timeSinceLastPressed = 0;
            if (OnButtonPressed != null)
            {
                OnButtonPressed.Invoke();
            }
        }
    }

    private void Update()
    {
        timeSinceLastPressed += Time.deltaTime;
    }
}
