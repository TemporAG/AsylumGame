using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickupThrow : MonoBehaviour
{
    public Camera fpsCam;
    //public ItemScript ItemPick;
    public GameObject player;
    public Transform holdPos;

    public float range = 2.1f;
    public float throwForce = 500f;
    public float pickUpRange = 2.1f;

    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    public float PickupRate = 10.0f;
    private float nextTimeToPickup = 0f;

    Vector3 origin;
    Vector3 direction;

    public LayerMask layerMask;
    RaycastHit hit;
    PlayerScript playerScript;
    char key;

    bool yellowKey;
    bool blueKey;
    bool greenKey;
    bool redKey;
    bool orangeKey;
    bool purpleKey;

    private void Start()
    {
        
        yellowKey = false;
        blueKey = false;
        greenKey = false;
        redKey = false;
        orangeKey = false;
        purpleKey = false;
    }

    void Update()
    {
        switch (key)
        {

            case 'A':
                yellowKey = true;
                break;
            case 'B':
                blueKey = true;
                break;
            case 'C':
                greenKey = true;
                break;
            case 'D':
                redKey = true;
                break;
            case 'E':
                orangeKey = true;
                break;
            case 'F':
                purpleKey = true;
                break;
        }
        origin = transform.position;
        direction = fpsCam.ScreenPointToRay(Input.mousePosition).direction;
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextTimeToPickup)
        {
            nextTimeToPickup = Time.time + 2f / PickupRate;
            Shoot();
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {

                    if (hit.transform.gameObject.tag == "Holdable")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    
                    DropObject();
                }
            }
        }
        if (heldObj != null)
        {
            MoveObject();

            if (Input.GetKeyDown(KeyCode.Mouse1) && canDrop == true)
            {

                ThrowObject();
            }

        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;

            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {

        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
    }
    void MoveObject()
    {

        heldObj.transform.position = holdPos.transform.position;
    }

    void ThrowObject()
    {

        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;

    }

    void Shoot()
    {
        Debug.DrawRay(fpsCam.ScreenPointToRay(Input.mousePosition).origin, fpsCam.ScreenPointToRay(Input.mousePosition).direction * range, Color.yellow, 10);
        if (Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("Consumable"))
            {
                playerScript = FindObjectOfType<PlayerScript>();
                playerScript.currentSanity += 10f;
                Destroy(hit.collider.gameObject);
                Debug.Log("e");
            }
        }
        
        if(Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("YellowK"))
            {
                yellowKey = true; Debug.Log("y");
                Object.Destroy(hit.collider.gameObject);
            }
        }

        if (Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("BlueK"))
            {
                blueKey = true; Debug.Log("b");
                Object.Destroy(hit.collider.gameObject);
            }
        }

        if (Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("GreenK"))
            {
                greenKey = true; Debug.Log("g");
                Object.Destroy(hit.collider.gameObject);
            }
        }

        if (Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("RedK"))
            {
                redKey = true; Debug.Log("r");
                Object.Destroy(hit.collider.gameObject);
            }
        }

        if (Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("OrangeK"))
            {
                orangeKey = true; Debug.Log("o");
                Object.Destroy(hit.collider.gameObject);
            }
        }

        if (Physics.Raycast(origin, direction, out hit, range, layerMask))
        {
            if (hit.transform.CompareTag("PurpleK"))
            {
                purpleKey = true; Debug.Log("p");
                Object.Destroy(hit.collider.gameObject);
            }
        }
    }
}