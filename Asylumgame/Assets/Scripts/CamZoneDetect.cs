using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamZoneDetect : MonoBehaviour
{
    int x = 4;
    public Transform areaofdetection;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine("PlayerFound");// AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
    }


    IEnumerator PlayerFound()
    {
        Debug.Log("abc");
        yield return new WaitForSeconds(x);
        Destroy(gameObject);
    }
}