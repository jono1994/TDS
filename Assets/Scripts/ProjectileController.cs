using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody MyRB;
    public float Speed;
    public float Dmg;
    private void OnEnable()
    {
        GameEvents.OnGetDamage += GetDamage;
    }
    private void OnDisable()
    {
        GameEvents.OnGetDamage -= GetDamage;
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
    private void GetDamage(float DMG)
    {
        Dmg = DMG;
    }
}
