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

    public bool It;

    public BoxCollider LeftHand, RightHand;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<RigidbodySynchronizable>();
        controller = GetComponent<CharacterController>();

        It = false;
        Cursor.lockState = CursorLockMode.Locked;
        //LeftHand.enabled = false;
        //RightHand.enabled = false;
    }
    private void EnableHands()
    {
        if (It)
        {
          //  LeftHand.enabled = true;
          //  RightHand.enabled = true;
        }
    }

    public void SetIT(ulong PlayerID)
    {
        It = false;
        SetItServerRpc(PlayerID);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetItServerRpc(ulong PlayerID)
    { 
         SetItClientRpc(PlayerID);
    }

    [ClientRpc]
    private void SetItClientRpc(ulong PlayerID)
    {
        Debug.Log("Client Id = " + PlayerID);
        Debug.Log("Owner Id = " + OwnerClientId);
        if (PlayerID == OwnerClientId)
        {
            Debug.Log(PlayerID + "= true");
            It = true;
        }
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameEvents.OnChooseIT?.Invoke();
            }
        }

        
    }
}

