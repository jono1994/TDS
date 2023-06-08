using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static NetworkVariable<int> P1DMG;
    public static NetworkVariable<int> P2DMG;

    private void OnEnable()
    {
        GameEvents.OnGetDamage += GetDamage;
    }
    private void OnDisable()
    {
        GameEvents.OnGetDamage -= GetDamage;
    }

    private void GetDamage(int DmgDealt)
    {
        P1DMG = new NetworkVariable<int>(WeaponDataController.P1PrimaryDMG);
        P2DMG = new NetworkVariable<int>(WeaponDataController.P2PrimaryDMG);
    }

}
