using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : NetworkBehaviour
{
    public int DmgTaken;
    public GameObject DamagedThingy;
    public string damager;
    private void OnEnable()
    {
        GameEvents.OnTakeDamage += DealWith;
    }
    private void DealWith(int DmgPassed, GameObject DamagedThingyPassed,string Damager)
    {
            DmgTaken = DmgPassed;
            DamagedThingy = DamagedThingyPassed;
            damager = Damager;
            TakeDamageServerRpc();
    }
    private void TakeDamage()
    {
        if(DamagedThingy == gameObject)
        {
            Debug.Log(DmgTaken);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void TakeDamageServerRpc()
    {
        TakeDamageClientRpc();
    }
    [ClientRpc]
    private void TakeDamageClientRpc()
    {
        //if(!IsOwner)
        TakeDamage();
    }
}
