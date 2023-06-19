using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIController : NetworkBehaviour
{
    public GameObject MainPanel;
    public GameObject MainMenu;
    public GameObject LoadOutScreen;
    public LoadOutDropDowns LoudOut;

    public GameObject Camera;
   public void StartGame(string input)
    {
        Destroy(Camera);
        Debug.Log("Starting...");
        NetworkManager.Singleton.StartHost();
        MainPanel.SetActive(false);
        //SceneManager.LoadScene(input);
        StartServerRpc();
    }

    public void JoinGame()
    {
        Destroy(Camera);
        Debug.Log("Joining...");
        MainPanel.SetActive(false);
        NetworkManager.Singleton.StartClient();
    }

    [ServerRpc]

    private void StartServerRpc()
    {
        GameEvents.OnStartGame?.Invoke();
        StartClientRpc();
    }

    [ClientRpc]
    private void StartClientRpc()
    {
        GameEvents.OnStartGame?.Invoke();
    }
    public void LoadOutMenu()
    {
        MainMenu.SetActive(false);
        LoadOutScreen.SetActive(true);
    }

    public void Back()
    {
        GameEvents.OnSelectLoadout?.Invoke();
        MainMenu.SetActive(true);
        LoadOutScreen.SetActive(false);
    }
}
