using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PausePanel;
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
    }
}
