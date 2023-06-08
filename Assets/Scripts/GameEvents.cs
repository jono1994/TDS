using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameEvents : MonoBehaviour
{
    public delegate void OnGetDamageDelegate(int DmgDealt);
    public static OnGetDamageDelegate OnGetDamage;

    public delegate void OnGetSTNDelegate(bool STN);
    public static OnGetSTNDelegate OnGetSTN;

    public delegate void OnTakeDamageDelegate (int DmgTaken, GameObject DamagedThingy, string Damager);
    public static OnTakeDamageDelegate OnTakeDamage;

    public delegate void OnSelectLoadoutDelegate();
    public static OnSelectLoadoutDelegate OnSelectLoadout;

    public delegate void OnStartGameDelegate();
    public static OnStartGameDelegate OnStartGame;
}
