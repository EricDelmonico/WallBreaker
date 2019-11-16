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

    private List<CubeScript> walls;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        teleportPos = new Vector3(0, 0, teleportDis);

        // Store all the walls found in the scene
        walls = new List<CubeScript>();
        for (int i = 0; i < 10; i++)
        {
            GameObject wall = GameObject.Find("Wall" + i.ToString());
            if (wall != null)
            {
                walls.Add(wall.GetComponent<CubeScript>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly adjusts position
        position.z += (speed * Time.deltaTime);
        gameObject.transform.position = position;

        if(gameObject.transform.position.z > teleportPos.z)
            Teleport();
    }

    /// <summary>
    /// Teleports the player to the beginning of the chunck
    /// </summary>
    void Teleport()
    {
        position.z -= teleportDis;
        gameObject.transform.position = position;
    }
}
