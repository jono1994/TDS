using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCamRotation : MonoBehaviour
{
    public Transform TargetRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = TargetRot.transform.rotation;
    }
}
