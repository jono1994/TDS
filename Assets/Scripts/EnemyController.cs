using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyController : NetworkBehaviour
{
    public float DmgTaken;
    public GameObject DamagedThingy;
    private void OnEnable()
    {
        GameEvents.OnTakeDamage += DealWith;
    }
    private void DealWith(float DmgPassed, GameObject DamagedThingyPassed)
    {
        DmgTaken = DmgPassed;
        DamagedThingy = DamagedThingyPassed;
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
        if(!IsOwner)
        TakeDamage();
    }
}
