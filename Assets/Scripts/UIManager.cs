using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Variables
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject gameMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        ToMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayGame()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    void GameOver()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    void ToMainMenu()
    {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

}
