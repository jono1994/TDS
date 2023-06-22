using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Security;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class Relay : MonoBehaviour
{
    public TextMeshProUGUI JoinCodeToEnter;

    private void OnEnable()
    {
        GameEvents.OnStartRelay += CreateRelay;
        GameEvents.OnjoinRelay+= JoinRelay;
    }
    private void OnDisable()
    {
        GameEvents.OnStartRelay -= CreateRelay;
        GameEvents.OnjoinRelay -= JoinRelay;
    }
    private async void Start()
    {
       await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in" + AuthenticationService.Instance.PlayerId);
        };
         
       await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private async void CreateRelay()
    {
        try
        {
           Allocation allocation = await RelayService.Instance.CreateAllocationAsync(7);


           string JoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log(JoinCode);
            JoinCodeToEnter.text= JoinCode;


            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
        }catch(RelayServiceException e)
        {
            Debug.Log(e);
        } 
    }

    public async void JoinRelay(string JoinCode)
    {
        try
        {
            Debug.Log("Joining Relay with " + JoinCode);
             JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(JoinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
       
    }
}
