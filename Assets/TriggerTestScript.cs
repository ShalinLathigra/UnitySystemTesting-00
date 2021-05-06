using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTestScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger: " + other.name);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exited Trigger: " + other.name);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Inside Trigger: " + other.name);
    }
}
