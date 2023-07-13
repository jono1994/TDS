using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    private bool hold;
    public KeyCode grabKey;
    public bool canGrab;

    void Update()
    {
        if (canGrab)
        {
            if (Input.GetKey(grabKey))
            {
                hold = true;
                Debug.Log("Grabbing");
            }
            else
            {
                hold = false;
                Destroy(GetComponent<FixedJoint>());
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Grabbed" + col.gameObject.name);
        if (hold && col.transform.tag == "Grabbable")
        {
            
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                fj.connectedBody = rb;
            }
            else
            {
                FixedJoint fj = transform.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            }
        }
    }
}
