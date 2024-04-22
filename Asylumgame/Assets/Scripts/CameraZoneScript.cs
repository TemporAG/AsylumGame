using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoneScript : MonoBehaviour
{


    AiScript aiScript;
    //public GameObject CamZone;

    // Start is called before the first frame update
    void Start()
    {
        aiScript = FindObjectOfType<AiScript>();
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

    private void OnTriggerStay(Collider ccollision)
    {
        if(ccollision.gameObject.tag == "Player")
        {
            Debug.Log("a");
            aiScript.canSeePlayer = true;
        }
    }
}
