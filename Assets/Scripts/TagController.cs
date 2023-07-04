using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagController : MonoBehaviour
{
    public bool It;

    public BoxCollider LeftHand, RightHand;

    private void OnEnable()
    {
        GameEvents.OnEnableHands += EnableHands;
    }
    private void OnDisable()
    {
        GameEvents.OnEnableHands -= EnableHands;
    }

    private void EnableHands(bool IsIt)
    {
        It = IsIt;
        if (It)
        {
            LeftHand.enabled = true;
            RightHand.enabled = true;
        }
        if (!It)
        {
            LeftHand.enabled = false;
            RightHand.enabled = false;
        }
    }

    void Start()
    {
        It = false;
        LeftHand.enabled = false;
        RightHand.enabled = false;
    }


}
