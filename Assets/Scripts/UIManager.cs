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
    [SerializeField]
    private Text finalScore;

    [HideInInspector]
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        ToMainMenu();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        gameMenu.SetActive(true);
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene(0);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(true);

    }

    public void ToMainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        finalScore.text = "Final Score: " + score;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
