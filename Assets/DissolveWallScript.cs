using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveWallScript : MonoBehaviour
{
    private float dissolveSeconds = 0.75f;
    [SerializeField]
    private Material dissolveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DissolveWall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DissolveWall(float time = 0)
    {
        dissolveMaterial.SetFloat("_Dissolvedness", time);
        float oneSixtieth = Time.deltaTime;
        yield return new WaitForSeconds(oneSixtieth);
        time += oneSixtieth / dissolveSeconds;
        if (time <= 1)
        {
            StartCoroutine(DissolveWall(time));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
