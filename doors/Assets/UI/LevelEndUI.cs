using System.Collections;
using System.Collections.Generic;
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
        nextButton.onClick.AddListener(() => { NextLevel(); });
        quitButton.onClick.AddListener(() => { QuitGame(); });
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
