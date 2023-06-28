
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
    public bool GameStarted;
    private void OnEnable()
    {
        GameEvents.OnChooseIT += ChooseIT;
        GameEvents.OnTag += Tag;
    }
    private void OnDisable()
    {
        GameEvents.OnChooseIT -= ChooseIT;
        GameEvents.OnTag -= Tag;
    }



    private void ChooseIT()
    {
        if (IsServer)
        {
            if (!GameStarted)
            {
                GameStarted = true;
                Debug.Log("Choose It");
                Players.Clear();
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Players.Add(player.GetComponent<PlayerController1>());
                    player.GetComponent<PlayerController1>().It = false;
                    //player.GetComponent<PlayerController1>().undies.GetComponent<Renderer>().material = player.GetComponent<PlayerController1>().Blue;
                }

                PlayerNum = Random.Range(0, Players.Count);
                PlayerID = Players[PlayerNum].GetComponent<NetworkObject>().OwnerClientId;
                ItPlayer = Players[PlayerNum];

                ItPlayer.SetIT(true);

                //ItPlayer.undies.GetComponent<Renderer>().material = ItPlayer.Red;
                GameEvents.OnEnableHands?.Invoke();
            }
        }
           
    }
    private void Tag(PlayerController1 TaggedPlayer)
    {
        if (GameStarted)
        {
            Debug.Log("Tag");
            if (ItPlayer != null)
            {
                ItPlayer.SetIT(false);

                ItPlayer = TaggedPlayer;
                ItPlayer.SetIT(true);

                GameEvents.OnEnableHands?.Invoke();
            }
            //Debug.Log($"Tag {TaggedPlayer.GetComponent<NetworkObject>().OwnerClientId}");
            //Players.Clear();
            //foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            //{
            //    Players.Add(player.GetComponent<PlayerController1>());
            //    player.GetComponent<PlayerController1>().It = false;
            //    //player.GetComponent<PlayerController1>().undies.GetComponent<Renderer>().material = player.GetComponent<PlayerController1>().Blue;
            //}

            //ItPlayer = TaggedPlayer;
            //Debug.Log(TaggedPlayer);

            ////TaggedPlayer.undies.GetComponent<Renderer>().material = TaggedPlayer.Red;
            //PlayerID = TaggedPlayer.GetComponent<NetworkObject>().OwnerClientId;

            //ItPlayer.SetIT(PlayerID);
            //GameEvents.OnEnableHands?.Invoke();
        }
    }
}
