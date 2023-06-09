using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static NetworkVariable<int> P1DMG;
    public static NetworkVariable<int> P2DMG;

    public int p1DMG;  
    public int p2DMG;

    private void OnEnable()
    {
        GameEvents.OnGetDamage += GetDamage;
    }
    private void OnDisable()
    {
        GameEvents.OnGetDamage -= GetDamage;
    }

    private void Update()
    {
        GetDamage(p1DMG);
        p1DMG= P1DMG.Value;
        p2DMG= P2DMG.Value;
    }
    private void GetDamage(int DmgDealt)
    {
        P1DMG = new NetworkVariable<int>(WeaponDataController.P1PrimaryDMG);
        P2DMG = new NetworkVariable<int>(WeaponDataController.P2PrimaryDMG);
    }

}
