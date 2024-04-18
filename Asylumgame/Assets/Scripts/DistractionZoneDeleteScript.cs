using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionZoneDeleteScript : MonoBehaviour
{
    int x = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Disappear");
    }


    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(x);
        Destroy(gameObject);
    }
}
