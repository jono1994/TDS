using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController1 : NetworkBehaviour
{
    private CharacterController controller;
    [SerializeField] private float Speed = 6f;
    [SerializeField] private float TurnSpeed = 90f;
    [SerializeField] private Animator Anim;

    public Rigidbody rb;
    public float rotationSpeed = 100f;

    [SerializeField] public Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<RigidbodySynchronizable>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Anim.SetBool("Moving", true);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                Anim.SetBool("Moving", false);
            }
            //rotates the hips of the ragdoll
            float rotationAmount = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
            rb.MoveRotation(rb.rotation * deltaRotation);

            if (Input.GetMouseButton(0))
            {
                Anim.SetBool("LeftArm", true);
            }
            else
            {
                Anim.SetBool("LeftArm", false);
            }
            if (Input.GetMouseButton(1))
            {
                Anim.SetBool("RightArm", true);
            }
            else
            {
                Anim.SetBool("RightArm", false);
            }
        }

        
    }
}

