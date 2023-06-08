using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class LoadOutDropDowns : NetworkBehaviour
{
    public TextMeshProUGUI Output;
    public void HandleInputData(int val)
    {
        if (OwnerClientId == 0)
        {
            if (val == 1)
            {
                WeaponDataController.P1PrimaryDMG = new NetworkVariable<int>(10);
                WeaponDataController.P1PrimarySTN = false;
            }
            if (val == 2)
            {
                WeaponDataController.P1PrimaryDMG = new NetworkVariable<int>(0);
                WeaponDataController.P1PrimarySTN = true;
            }

        }
        else
        {
            if (val == 1)
            {
                WeaponDataController.P2PrimaryDMG = new NetworkVariable<int>(10);
                WeaponDataController.P2PrimarySTN = false;
            }
            if (val == 2)
            {
                WeaponDataController.P2PrimaryDMG = new NetworkVariable<int>(0);
                WeaponDataController.P2PrimarySTN = true;
            }
        }
    }
}
