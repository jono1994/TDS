using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public GameObject FeetEffect;
    public PlayerController1 player;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Environment")
        {
            if (player.It)
            {
                Debug.Log("Step");
                Instantiate(FeetEffect, transform.position, Quaternion.identity);
            }
            
        }
    }
}
