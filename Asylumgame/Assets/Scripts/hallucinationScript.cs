using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hallucinationScript : MonoBehaviour
{

    public GameObject hallucination;
    public PlayerScript psanity;


    void Start()
    {
        hallucination.SetActive(false);
    }

    private void Disappear()
    {
        hallucination.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider scollision)
    {
        if (scollision.gameObject.tag == "Player" && psanity.currentSanity < 35 )
        {
            hallucination.SetActive(true);
            Invoke("Disappear", 1f);
            Debug.Log("aaa");
        }
    }
}
