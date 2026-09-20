using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PenCollision : MonoBehaviour
{
    public string TagToIgnore = "";
    
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagToIgnore)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            // Debug.Log("Collidor works!");
            int id = collision.gameObject.GetComponent<MovementSM>().id;
            EventManager.onSafe(id);
        }
    }
}

