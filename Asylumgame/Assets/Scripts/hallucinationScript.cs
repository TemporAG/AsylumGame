using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hallucinationScript : MonoBehaviour
{

    public GameObject hallucination;
    public PlayerScript psanity;
    public GameObject jumpsound;


    void Start()
    {
        hallucination.SetActive(false);
        jumpsound.SetActive(false);
    }

    private void Disappear()
    {
        hallucination.SetActive(false);
        jumpsound.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider scollision)
    {
        if (scollision.gameObject.tag == "Player" && psanity.currentSanity < 20 )
        {
            hallucination.SetActive(true);
            jumpsound.SetActive(true);
            Invoke("Disappear", 1f);
            Debug.Log("aaa");
        }
    }
}
