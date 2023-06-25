using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!collision.gameObject.GetComponent<PlayerController1>().It)
            {
                GameEvents.OnTag?.Invoke(collision.gameObject.GetComponent<PlayerController1>());
            }
        }
    }
}
