using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDetection : MonoBehaviour
{
    public string Tag = "";

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.FindGameObjectsWithTag(Tag).Length > 2)
            {
                // Debug.Log("Debug logs Pen is full");
            }

        // EventManager.onSafe(2);

    }
}
