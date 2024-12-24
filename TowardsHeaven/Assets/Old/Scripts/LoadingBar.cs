using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
    public Slider slider;
    public GameObject roomTemplates;
    
    private void Update()
    {
        slider.value = -roomTemplates.GetComponent<RoomTemplates>().waitTime;

    }

    
}
