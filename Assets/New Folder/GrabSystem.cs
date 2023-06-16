using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSystem : MonoBehaviour
{
    public Transform RightArm, LeftArm;
    public Transform RotTarget;

    public Quaternion RightArmStart, LeftArmStart;

    public Animator Anim;


    private void Start()
    {
        RightArmStart = RightArm.rotation;
        LeftArmStart = LeftArm.rotation;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Anim.SetBool("LeftArm", true);
        }
        else
        {
            Anim.SetBool("LeftArm", false);
        }
    }

}
