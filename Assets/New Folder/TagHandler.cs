using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagHandler : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Body"))
        {
            GameObject CollidedOwnerObject = collision.gameObject.GetComponent<GetOwner>().MostParent;
            if (CollidedOwnerObject.GetComponent<PlayerController1>().It != true)
            {
                GameEvents.OnTag?.Invoke(CollidedOwnerObject.GetComponent<PlayerController1>());
                //GameEvents.OnTag?.Invoke(GetComponentInParent<PlayerController1>());
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if (collision.gameObject.CompareTag("Player"))
    //    //{
    //    //    if(!collision.gameObject.GetComponent<PlayerController1>().It)
    //    //    {
    //    //        Debug.Log("Tag");
    //    //        GameEvents.OnTag?.Invoke(collision.gameObject.GetComponent<PlayerController1>());
    //    //    }
    //    //}


    //    //if (collision.gameObject.GetComponent<PlayerController1>().It != true)
    //    //{
    //    //    Debug.Log("Tag");
    //    //    GameEvents.OnTag?.Invoke(collision.gameObject.GetComponent<PlayerController1>());
    //    //}

    //}
}
