using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectDistractionScript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject DistractionObject;

    float speedX; float speedY; float speedZ;
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame


    void Update()
    {
        //rb.detectCollisions = true; Debug.Log("a");

        speedX = Mathf.Abs(rb.velocity.x);
        speedY = Mathf.Abs(rb.velocity.y);
        speedZ = Mathf.Abs(rb.velocity.z);
        speed = rb.velocity.magnitude;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(speed > 0.1f) 
        {
            Instantiate(DistractionObject, transform.position, Quaternion.identity);
    }   }
}
