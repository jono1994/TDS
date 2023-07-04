using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.ComponentModel;

public class PlayerController1 : NetworkBehaviour
{
    private CharacterController controller;
    [SerializeField] private float Speed = 6f;
    [SerializeField] private float TurnSpeed = 90f;
    [SerializeField] private Animator Anim;
    [SerializeField] private List<Transform> SpawnPoints;
    [SerializeField] private Transform SpawnedPoint;

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
        EnableHands(false);
    }

    private void OnEnable()
    {
        GameEvents.OnEnableHands += EnableHands;
    }
    private void OnDisable()
    {
        GameEvents.OnEnableHands -= EnableHands;
    }

    public override void OnNetworkSpawn()
    {
       if (IsOwner) UpdatePositionServerRpc();
    }
    public void EnableHands(bool IsIt)
    {
        EnableHandsServerRPC(IsIt);
    }
    [ServerRpc(RequireOwnership = false)]
    private void EnableHandsServerRPC(bool IsIt)
    {
        EnableHandsClientRPC(IsIt);
    }
    [ClientRpc]
    private void EnableHandsClientRPC(bool IsIt)
    {
        if (IsOwner)
        {
            It = IsIt;
            if (It)
            {
                LeftHand.enabled = true;
                RightHand.enabled = true;
                MyItUi.text = "You're It";
                Anim.speed = 1.5f;
            }
            if (!It)
            {
                LeftHand.enabled = false;
                RightHand.enabled = false;
                MyItUi.text = " ";
                Anim.speed = 1f;
            }
        }
        
    }
    public void SetIT(bool isIt)
    {
        //if (!IsOwner) return;
        //It = false;
        //undies.GetComponent<Renderer>().material = Blue;
        //Debug.Log(undies.GetComponent<Renderer>().material);
        //MyItUi.text = (" ");
        SetItServerRpc(isIt);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetItServerRpc(bool isIt)
    { 
         SetItClientRpc(isIt);
    }

    [ClientRpc]
    private void SetItClientRpc(bool isIt)
    {
        It= isIt;
        if(It)
        {
            undies.GetComponent<Renderer>().material = Red;
            Debug.Log(undies.GetComponent<Renderer>().material);
            
        }
        else
        {
            undies.GetComponent<Renderer>().material = Blue;
            Debug.Log(undies.GetComponent<Renderer>().material);
            
        }
        //Debug.Log("Client Id = " + PlayerID);
        //Debug.Log("Owner Id = " + OwnerClientId);
        //if (PlayerID == OwnerClientId)
        //{
        //    Debug.Log(PlayerID + "= true");
        //    It = true;
        //    undies.GetComponent<Renderer>().material = Red;
        //    MyItUi.text = ("You're IT!!");
        //}
        //else
        //{
        //    It = false;
        //    Debug.Log(It);
        //    undies.GetComponent<Renderer>().material = Blue;
        //    MyItUi.text = (" ");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            if (It)
            {
                
            }
            if (!It)
            {
                
            }
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

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Anim.SetBool("Crouching", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                Anim.SetBool("Crouching", false);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GameEvents.OnChooseIT?.Invoke();
            }
        }

        
    }

    [ServerRpc]
    private void UpdatePositionServerRpc()
    {
        UpdatePositionClientRpc();
    }
    [ClientRpc]
    private void UpdatePositionClientRpc()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("spawnpoint");

        foreach (GameObject spawnPointObject in spawnPointObjects)
        {
            SpawnPoints.Add(spawnPointObject.transform);
        }

        SpawnedPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
        transform.position = SpawnedPoint.position;
        transform.rotation = SpawnedPoint.rotation;
    }
}

