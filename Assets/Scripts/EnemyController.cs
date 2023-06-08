using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : NetworkBehaviour
{
    public NetworkVariable<int> DmgTaken = new NetworkVariable<int>();
    public GameObject DamagedThingy;
    private void OnEnable()
    {
        GameEvents.OnTakeDamage += DealWith;
    }
    private void DealWith(NetworkVariable<int> DmgPassed, GameObject DamagedThingyPassed)
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
