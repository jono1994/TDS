using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody MyRB;
    public float Speed;
    public NetworkVariable<int> Dmg = new NetworkVariable<int>();
    public bool Stn;

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
            GameEvents.OnTakeDamage?.Invoke(Dmg, collision.gameObject);
            Destroy(gameObject);
        }
    }
    private void GetDamage(NetworkVariable<int> DMG)
    {
        Dmg = DMG;
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
