using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public float speed;
    public float teleportDis;

    [SerializeField]
    private int lives;

    private Vector3 position;
    private Vector3 teleportPos;

    private List<MakeCubes> walls;
    private int currentWall;
    private bool changeWall;

    private bool collidingWall;
    private bool previousCollide;

    private int score;

    [SerializeField]
    private GameObject[] scenery;
    private GameObject currentScenery;
    private GameObject currentDecor;
    private int sceneryIndex;
    private int prevSceneryIndex;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text liveText;

    [SerializeField]
    private GameObject fakeWall;
    private bool wallDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        wallDestroyed = false;
        collidingWall = false;

        position = gameObject.transform.position;
        teleportPos = new Vector3(0, 0, teleportDis);

        // Store all the walls found in the scene
        walls = new List<MakeCubes>();
        for (int i = 0; i < 10; i++)
        {
            GameObject wall = GameObject.Find("Wall" + i.ToString());
            if (wall != null)
            {
                walls.Add(wall.GetComponent<MakeCubes>());
            }
            if (wall == null)
            {
                walls[i - 1].lastWall = true;
                break;
            }
        }
        currentWall = -1;
        changeWall = true;
        // run this once to display the first wall's pattern
        if (changeWall && currentWall != walls.Count - 1)
        {
            changeWall = false;
            currentWall++;
            walls[currentWall].DisplayPattern();
        }

        score = 0;
        sceneryIndex = 0;
        if (scenery != null)
        {
            currentDecor = Instantiate(scenery[sceneryIndex], Vector3.zero - new Vector3(0, 0, 128), Quaternion.identity);
            currentScenery = Instantiate(scenery[sceneryIndex], Vector3.zero, Quaternion.identity);
            currentScenery.transform.GetChild(1);
            prevSceneryIndex = sceneryIndex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly adjusts position
        position.z += (speed * Time.deltaTime);
        gameObject.transform.position = position;

        wallDestroyed = walls[currentWall].Destroyed();
        if (wallDestroyed && collidingWall)
        {
            collidingWall = false;
        }

        if (changeWall && currentWall != walls.Count - 1)
        {
            changeWall = false;
            currentWall++;
            walls[currentWall].DisplayPattern();
        }

        if (walls[currentWall].wallSolved && currentWall != walls.Count - 1)
        {
            changeWall = true;
        }

        if (walls[walls.Count - 1].wallSolved)
            ResetScene();

        ChangeLives();

        if (walls[0].wallSolved)
            fakeWall.SetActive(false);

        previousCollide = collidingWall;

        scoreText.text = "Score: " + score;
        liveText.text = "Lives: " + lives;
    }

    /// <summary>
    /// Teleports the player to the beginning of the chunk
    /// </summary>
    void Teleport()
    {
        position.z -= teleportDis - (speed * Time.deltaTime);
        gameObject.transform.position = position;
        collidingWall = false;
    }

    void ResetScene()
    {
        walls[walls.Count - 1].DoLastWall(fakeWall);
        score++;
        if (score % 2 == 0 && scenery != null)
        {
            // if all the scenery has been cycled 
            // through, loop back to the beginning
            prevSceneryIndex = sceneryIndex;
            if (sceneryIndex + 1 >= scenery.Length)
            {
                sceneryIndex = -1;
            }
            sceneryIndex++;
            Destroy(currentScenery);
            currentScenery = Instantiate(scenery[sceneryIndex], Vector3.zero, Quaternion.identity);
        }
        if (score % 2 == 1)
        {
            prevSceneryIndex = sceneryIndex;
        }
        Destroy(currentDecor);
        Debug.Log(score % 2 == 0 ? "yes" : "no");
        currentDecor = Instantiate(scenery[prevSceneryIndex], Vector3.zero - new Vector3(0, 0, 128), Quaternion.identity);

        Teleport();
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].ResetWall();
        }
        changeWall = true;
        currentWall = -1;
        // run this once to display the first wall's pattern
        if (changeWall && currentWall != walls.Count - 1)
        {
            changeWall = false;
            currentWall++;
            walls[currentWall].DisplayPattern();
        }
    }

    //Collision Detection
    private void OnTriggerEnter(Collider other)
    {
         collidingWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collidingWall = false;
    }

    void ChangeLives()
    {
        if(collidingWall && !previousCollide)
        {
            if (lives <= 0)
                lives = 0;
            else
                lives--;

            walls[currentWall].Solve();
        }
    }
}
