using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hallucinationScript : MonoBehaviour
{

    public GameObject hallucination;

    // Start is called before the first frame update
    void Start()
    {
        hallucination.SetActive(true);
    }

    private void Disappear()
    {
        hallucination.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider vcollision)
    {
        if (vcollision.gameObject.tag == "View")
        {
            Invoke("Disappear", 1);
        }

    }
}
