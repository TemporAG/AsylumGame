using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoneScript : MonoBehaviour
{

    public GameObject playerReference;

    AiScript aiScript;
    //public GameObject CamZone;

    // Start is called before the first frame update
    void Start()
    {
        //playerReference = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider ccollision)
    {
        if (ccollision.gameObject.tag == "Player")
        {
            Debug.Log("abcbcbcbcbc");
            aiScript.canSeePlayer = true;
            //Instantiate(CamZone, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider ccollision)
    {
        if (ccollision.gameObject.tag == "Player" == false)
        {
            aiScript.canSeePlayer = false;
        }

    }
}
