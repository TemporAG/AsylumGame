using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hallucinationScript : MonoBehaviour
{

    public GameObject hallucination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(hallucination, transform.position, Quaternion.identity);
    }
}