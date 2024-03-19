using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{
    public Slider sslider;


    public void MaxStamia(float stamina)
    {
        sslider.maxValue = stamina;
        sslider.value = stamina;
    }

    public void SetStamia(float stamina)
    {
        // sslider.value = stamina;
    }
}
