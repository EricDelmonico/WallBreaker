using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    public float speed;
    public float teleportDis;

    private Vector3 position;
    private Vector3 teleportPos;

    private List<MakeCubes> walls;
    private int currentWall;
    private bool changeWall;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        currentWall = -1;
        changeWall = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly adjusts position
        position.z += (speed * Time.deltaTime);
        gameObject.transform.position = position;

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
    }

    /// <summary>
    /// Teleports the player to the beginning of the chunck
    /// </summary>
    void Teleport()
    {
        position.z -= teleportDis;
        gameObject.transform.position = position;
    }

    void ResetScene()
    {
        // score++

        Teleport();
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].ResetWall();
        }
        changeWall = true;
        currentWall = 0;
    }
}
