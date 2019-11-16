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
    private Vector3 initialPos;

    private List<MakeCubes> walls;
    private int currentWall;
    private bool changeWall;

    private bool collidingWall;
    private bool previousCollide;

    private int score;

    [SerializeField]
    private GameObject[] scenery;
    private GameObject currentScenery;
    private int sceneryIndex;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text liveText;

    // Start is called before the first frame update
    void Start()
    {
        collidingWall = false;

        initialPos = transform.position;

        position = gameObject.transform.position;
        teleportPos = new Vector3(transform.position.x, transform.position.y, teleportDis);

        // Store all the walls found in the scene
        walls = new List<MakeCubes>();
        for (int i = 0; i < 10; i++)
        {
            GameObject wall = GameObject.Find("Wall" + i.ToString());
            if (wall != null)
            {
                walls.Add(wall.GetComponent<MakeCubes>());
            }
        }
        currentWall = -1;
        changeWall = true;

        score = 0;
        sceneryIndex = 0;
        if (scenery != null)
        {
            currentScenery = Instantiate(scenery[sceneryIndex], Vector3.zero, Quaternion.identity);
            currentScenery.transform.GetChild(1);
            sceneryIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly adjusts position
        position.z += (speed * Time.deltaTime);
        gameObject.transform.position = position;

        if(walls.Count > 0)
        {
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

            previousCollide = collidingWall;
        }
        
        if(scoreText != null && liveText != null)
        {
            scoreText.text = "Score: " + score;
            liveText.text = "Lives: " + lives;
        }
        

        if(teleportDis == 384)
        {
            Debug.Log("Will teleport");
            Vector3 distance = transform.position - teleportPos;
            if (distance.magnitude < 0.5f && distance.magnitude > -0.5f)
            {
                Debug.Log("teleporting");
                Teleport();
            }
        }
        
    }

    /// <summary>
    /// Teleports the player to the beginning of the chunk
    /// </summary>
    void Teleport()
    {
        if(teleportDis == 384)
        {
            position.z -= 484;
            gameObject.transform.position = position;
        }
        else
        {
            position.z -= teleportDis - (speed * Time.deltaTime);
            gameObject.transform.position = position;
        }
        
    }

    void ResetScene()
    {
        score++;
        if (score % 2 == 0 && scenery != null)
        {
            Destroy(currentScenery);
            currentScenery = Instantiate(scenery[sceneryIndex], Vector3.zero, Quaternion.identity);
            currentScenery.transform.GetChild(1);
            // if all the scenery has been cycled 
            // through, loop back to the beginning
            if (sceneryIndex + 1 >= scenery.Length)
            {
                sceneryIndex = -1;
            }
            sceneryIndex++;
        }

        Teleport();
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].ResetWall();
        }
        changeWall = false;
        currentWall = 0;
        walls[0].DisplayPattern();
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
