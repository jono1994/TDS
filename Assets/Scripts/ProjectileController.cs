using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody MyRB;
    public float Speed;
    public int Dmg;
    public bool Stn;
    public NetworkVariable<GameObject> Owner;

    public ParticleSystem STNParticle;
    private void OnEnable()
    {
        GameEvents.OnGetDamage += GetDamage;
        GameEvents.OnGetSTN += GetSTN;
    }
    private void OnDisable()
    {
        GameEvents.OnGetDamage -= GetDamage;
        GameEvents.OnGetSTN -= GetSTN;
    }

    // Start is called before the first frame update
    void Start()
    {
        MyRB= GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        MyRB.velocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage(Dmg);
            GameEvents.OnTakeDamage?.Invoke(Dmg, collision.gameObject,Owner.Value.name);
            Destroy(gameObject);
        }
    }
    private void GetDamage(int DMG)
    {
        if (Owner.Value.name == "Player1")
        {
          //  DMG = GameManager.P1DMG.Value;
            Dmg=DMG;
            Debug.Log(DMG);
        }
        if(Owner.Value.name == "Player2")
        {
          //  DMG = GameManager.P2DMG.Value;
            Dmg = DMG;
            Debug.Log(DMG);
        }
    }

    private void GetSTN(bool STN)
    {
        Stn = STN;
        Debug.Log(Stn);
        if (Stn)
        {
            STNParticle.Play(true);
        }
    }
}
