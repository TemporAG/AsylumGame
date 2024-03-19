using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickupThrow : MonoBehaviour
{
    public Camera fpsCam;
    //public ItemScript ItemPick;

    public int range = 100;

    public float PickupRate = 10.0f;
    private float nextTimeToPickup = 0f;

    Vector3 origin;
    Vector3 direction;

    public LayerMask layerMask;
    RaycastHit hit;

    //private AIscript aIscript;


    private void Start()
    {

    }
    void Update()
    {
        origin = transform.position;
        direction = fpsCam.ScreenPointToRay(Input.mousePosition).direction;
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextTimeToPickup)
        {
            nextTimeToPickup = Time.time + 2f / PickupRate;
            Shoot(); 
        }
    }

    void Shoot()
    {
        Debug.DrawRay(fpsCam.ScreenPointToRay(Input.mousePosition).origin, fpsCam.ScreenPointToRay(Input.mousePosition).direction * range, Color.yellow, 10);
        if (Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("Consumable"))
            {
                //do something
            }
            else if (hit.transform.CompareTag("Holdable"))
            {
                //full pickup

                //ItemPick = hit.collider.GetComponent;
                //hit.Bottle.transform.parent = .Hand...;
                // 
            }
        }
    }

    
}

