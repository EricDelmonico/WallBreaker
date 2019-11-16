using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCubes : MonoBehaviour
{
    // the cube prefab
    [SerializeField]
    private GameObject cube;

    // the wall array
    private int[,] cubemap;

    // the actual wall
    private CubeScript[,] wall;

    // Start is called before the first frame update
    void Start()
    {
        int initial_size = 3;
        // start of the game is 3x3
        wall = new CubeScript[initial_size, initial_size];
        cubemap = new int[initial_size, initial_size];

        int size = wall.GetLength(0);
        float cubeDimension = cube.GetComponent<MeshRenderer>().bounds.size.x;
        float x = size % 2 == 0 ? (cubeDimension / 2f) - (cubeDimension * size / 2) : -(cubeDimension * (size - 1) / 2);
        float y = cubeDimension / 2f;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject temp = Instantiate(cube, new Vector3(x + cubeDimension * j, y + cubeDimension * i, 0), Quaternion.identity);
                temp.transform.parent = transform;
                temp.name = "i should be gone!";
                wall[i, j] = temp.GetComponent<CubeScript>();
            }
        }

        RandomWall();
        StartCoroutine(DisplayForSeconds(1.75f));
    }

    // Update is called once per frame
    void Update()
    {
        bool solved = true;
        int size = wall.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // if it is supposed to be activated and is not,
                // or if is supposed to not be activated and is
                if ((cubemap[i, j] == 1 && !wall[i, j].activated) ||
                    (cubemap[i, j] == 0 && wall[i, j].activated))
                {
                    // the puzzle was not solved
                    solved = false;
                }
            }
        }

        if (solved)
        {
            // wall dissolves here
            for (int i = transform.childCount - 1; i != -1; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            ResetWall();
        }
    }

    private void RandomWall()
    {
        int size = cubemap.GetLength(0);
        bool anything = false;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // is this a zero?
                float rng = Random.Range(0, 1f);
                if (rng > 0.75f)
                {
                    cubemap[i, j] = 1;
                    anything = true;
                }
            }
        }

        float rng1 = Random.Range(0, 1f);
        float rng2 = Random.Range(0, 1f);
        if (!anything)
        {
            cubemap[(int)(rng1 * size), (int)(rng2 * size)] = 1;
        }
    }

    private void ResetWall()
    {
        int size = cubemap.GetLength(0);
        cubemap = new int[size + 1, size + 1];
        wall = new CubeScript[size + 1, size + 1];

        RandomWall();

        size = cubemap.GetLength(0);
        float cubeDimension = cube.GetComponent<MeshRenderer>().bounds.size.x;
        float x = size % 2 == 0 ? (cubeDimension / 2f) - (cubeDimension * size / 2) : -(cubeDimension * (size - 1) / 2);
        float y = cubeDimension / 2f;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject temp = Instantiate(cube, new Vector3(x + cubeDimension * j, y + cubeDimension * i, 0), Quaternion.identity);
                temp.transform.parent = transform;
                wall[i, j] = temp.GetComponent<CubeScript>();
            }
        }

        StartCoroutine(DisplayForSeconds(1.75f));
    }

    IEnumerator DisplayForSeconds(float seconds)
    {
        // highlight pattern
        int size = cubemap.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                wall[i, j].displaying = true;
                if (cubemap[i, j] == 1)
                {
                    wall[i, j].gameObject.GetComponent<Renderer>().material = wall[i, j].clicked;
                }
            }
        }

        yield return new WaitForSeconds(seconds);

        // erase pattern
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                wall[i, j].gameObject.GetComponent<Renderer>().material = wall[i, j].notClicked;
                wall[i, j].displaying = false;
            }
        }
    }
}
