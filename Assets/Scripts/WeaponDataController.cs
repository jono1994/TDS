using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WeaponDataController : NetworkBehaviour
{
    public static NetworkVariable<int> P1PrimaryDMG;
    public static bool P1PrimarySTN;

    public static NetworkVariable<int> P2PrimaryDMG;
    public static bool P2PrimarySTN;


}
