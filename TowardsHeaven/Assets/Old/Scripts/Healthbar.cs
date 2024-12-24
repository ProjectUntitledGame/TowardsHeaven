using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject Character;
    

    // Update is called once per frame
    void Update()
    {
        healthBar.value = Character.GetComponent<Health>().healthvalue;

    }
}
