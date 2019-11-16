using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
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

}
