
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public List<PlayerController1> Players = new List<PlayerController1>();
    private ulong PlayerID;
    private PlayerController1 ItPlayer;
    public int PlayerNum;
    private void OnEnable()
    {
        GameEvents.OnChooseIT += ChooseIT;
        GameEvents.OnTag += Tag;
    }
    private void OnDisable()
    {
        GameEvents.OnChooseIT += ChooseIT;
        GameEvents.OnTag -= Tag;
    }

    

    private void ChooseIT()
    {
        if (IsServer)
        {
            Players.Clear();
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                Players.Add(player.GetComponent<PlayerController1>());
                player.GetComponent<PlayerController1>().It = false;
            }

            PlayerNum = Random.Range(0, Players.Count);
            PlayerID = Players[PlayerNum].GetComponent<NetworkObject>().OwnerClientId;
            ItPlayer = Players[PlayerNum];

            ItPlayer.SetIT(PlayerID);
            GameEvents.OnEnableHands?.Invoke();
        }
    }
    private void Tag(PlayerController1 TaggedPlayer)
    {
        Players.Clear();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Players.Add(player.GetComponent<PlayerController1>());
            player.GetComponent<PlayerController1>().It = false;
        }

        ItPlayer = TaggedPlayer;
        Debug.Log(TaggedPlayer);

        ItPlayer.SetIT(PlayerID);
        GameEvents.OnEnableHands?.Invoke();
    }
}
