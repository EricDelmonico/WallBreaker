using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    // Materials for clicked and unclicked
    [SerializeField]
    public Material clicked;
    [SerializeField]
    public Material notClicked;

    // whether this cube was clicked
    public bool activated;

    // The pattern is displaying
    public bool displaying;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Renderer>().material = notClicked;
        displaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (displaying)
            return;

        if (!activated)
        {
            GetComponent<Renderer>().material = clicked;
        }
        if (activated)
        {
            GetComponent<Renderer>().material = notClicked;
        }

        activated = !activated;
    }
}
