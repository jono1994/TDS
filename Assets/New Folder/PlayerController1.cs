using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerController1 : NetworkBehaviour
{
    private CharacterController controller;
    [SerializeField] private float Speed = 6f;
    [SerializeField] private float TurnSpeed = 90f;
    [SerializeField] private Animator Anim;

    public Rigidbody rb;
    public float rotationSpeed = 100f;

    public bool It;

    public TagHandler LeftHand, RightHand;
    public GameObject undies;
    public Material Red, Blue;

    public TextMeshProUGUI YourItText;
    public TextMeshProUGUI MyItUi;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<RigidbodySynchronizable>();
        controller = GetComponent<CharacterController>();

        It = false;
        Cursor.lockState = CursorLockMode.Locked;
        //YourItText = GameObject.Find("YourIt").GetComponent<TextMeshProUGUI>();
        MyItUi = Instantiate(YourItText, GameObject.Find("Canvas").transform);
        LeftHand.enabled = false;
        RightHand.enabled = false;
        EnableHands();
    }

    private void OnEnable()
    {
        GameEvents.OnEnableHands += EnableHands;
    }
    private void OnDisable()
    {
        GameEvents.OnEnableHands -= EnableHands;
    }
    private void EnableHands()
    {
        if (It)
        {
            LeftHand.enabled = true;
            RightHand.enabled = true;
        }
        if (!It)
        {
            LeftHand.enabled = false;
            RightHand.enabled = false;
        }
    }

    public void SetIT(ulong PlayerID)
    {
        if (!IsOwner) return;
        It = false;
        //undies.GetComponent<Renderer>().material = Blue;
        //Debug.Log(undies.GetComponent<Renderer>().material);
        //MyItUi.text = (" ");
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
            undies.GetComponent<Renderer>().material = Red;
            MyItUi.text = ("You're IT!!");
        }
        else
        {
            It = false;
            Debug.Log(It);
            undies.GetComponent<Renderer>().material = Blue;
            MyItUi.text = (" ");
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

