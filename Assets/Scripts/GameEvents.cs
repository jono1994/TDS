using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void OnGetDamageDelegate(float DmgDealt);
    public static OnGetDamageDelegate OnGetDamage;

    public delegate void OnTakeDamageDelegate (float DmgTaken, GameObject DamagedThingy);
    public static OnTakeDamageDelegate OnTakeDamage;

    public delegate void OnSelectLoadoutDelegate();
    public static OnSelectLoadoutDelegate OnSelectLoadout;

    public delegate void OnStartGameDelegate();
    public static OnStartGameDelegate OnStartGame;
}
