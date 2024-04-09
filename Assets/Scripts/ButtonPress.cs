using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour
{
    public UnityEvent onPressed;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("button pressed");
        onPressed.Invoke();
    }

}
