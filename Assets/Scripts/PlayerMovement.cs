using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Constantly adjusts position
        position.z += (speed * Time.deltaTime);
        gameObject.transform.position = position;
    }
}
