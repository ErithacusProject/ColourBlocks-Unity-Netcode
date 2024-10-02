using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour
{
    [SerializeField] Button nextButton;
    [SerializeField] Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(() => { NextLevelClientRpc(); });
        quitButton.onClick.AddListener(() => { QuitGame(); });
    }

    [ClientRpc]
    void NextLevelClientRpc()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
