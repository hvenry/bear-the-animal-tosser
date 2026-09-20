using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp : MonoBehaviour
{
   public Transform holdSpot;
   public Transform holdSpot2;
   public Transform holdSpot3;
   public Transform holdSpot4;
   private Transform[] spots = new Transform[4];
   public GameObject animalPrefab;
  
   public LayerMask pickUpMask;
   public GameObject destroyEffect;
   public Vector3 Direction { get; set; }
   private GameObject[] holdings = new GameObject[4];
   private AudioSource audioSource;
   public AudioClip throwingAudioClip;
   public AudioClip pickUpAudioClip;
   public AudioClip errorAudio;
       

   void Start()
   {
       spots = new Transform[] { holdSpot, holdSpot2, holdSpot3, holdSpot4 };
       audioSource = GetComponent<AudioSource>();
   }
   void Update()
   {
       // for (int i = 0; i < holdings.Length; i++)
       // {
       //     GameObject item = holdings[i];
       //     if (item)
       //     {
       //         item.transform.position = spots[i].position;
       //     }
       //
       
       if (Input.GetKeyDown(KeyCode.Mouse1))
       {
           Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position , 2f, pickUpMask);
           
           if (pickUpItem)
           {
               bool allowed = true;
               for (int i = 0; i < holdings.Length; i++)
               {
                   if (holdings[i] is not null && 
                       holdings[i].GetComponent<MovementSM>().weight < pickUpItem.GetComponent<MovementSM>().weight)
                       allowed = false;

               }

               if (allowed)
               {
                   for (int i = 0; i < holdings.Length; i++)
                   {
                       GameObject item = holdings[i];
                       if (item)
                           continue;
                       GameObject temp = pickUpItem.gameObject;
                       temp.transform.position = spots[i].position;
                       temp.transform.parent = transform;
                       MovementSM script = temp.GetComponent<MovementSM>();
                       script.ChangeState(script.heldState);
                       temp.GetComponent<Rigidbody2D>().simulated = false;
                       holdings[i] = temp;
                       audioSource.PlayOneShot(pickUpAudioClip);
                       break;


                   }
               }
               else
               {
                   //play sound 
                   audioSource.PlayOneShot(errorAudio);
               }
           }
          
       }
       
       if (Input.GetKeyDown(KeyCode.Mouse0))
       {
           if (holdings[0])
           {
               GameObject first = holdings[0];
               MovementSM script = first.GetComponent<MovementSM>();
               script.ChangeState(script.thrownState);
               first.GetComponent<Rigidbody2D>().simulated = true;
               
               audioSource.PlayOneShot(throwingAudioClip);
               holdings[0] = holdings[1];
               
               if (holdings[0] is not null)
               {
                   holdings[0].transform.position = spots[0].position;
               }
               
               holdings[1] = holdings[2];
               
               if (holdings[1] is not null)
               {
                   holdings[1].transform.position = spots[1].position;
               }
               
               holdings[2] = holdings[3];
               
               if (holdings[2] is not null)
               {
                   holdings[2].transform.position = spots[2].position;
               }
               holdings[3] = null;
               
           }
       }
   }


   // deprecated
   IEnumerator ThrowItem(GameObject item)
   {
       Vector3 startPoint = item.transform.position;
       Vector3 endPoint = transform.position + Direction * 2;
       item.transform.parent = null;
       for (int i = 0; i < 25; i++)
       {
           item.transform.position = Vector3.Lerp(startPoint, endPoint, i * .04f);
           yield return null;
       }
       if (item.GetComponent<Rigidbody2D>())
           item.GetComponent<Rigidbody2D>().simulated = true;
       Instantiate(destroyEffect, item.transform.position, Quaternion.identity);
       Destroy(item);
   }


}
