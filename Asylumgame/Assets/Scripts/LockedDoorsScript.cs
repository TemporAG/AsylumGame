using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorsScript : MonoBehaviour
{

    ItemPickupThrow itemPickupThrow;
    // Start is called before the first frame update
    void Start()
    {
        itemPickupThrow = FindObjectOfType<ItemPickupThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
