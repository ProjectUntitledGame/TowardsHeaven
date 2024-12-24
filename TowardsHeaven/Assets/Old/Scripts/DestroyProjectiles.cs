﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectiles : MonoBehaviour
{
    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().healthvalue--;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Rooms"))
        {
            
            Destroy(gameObject);

        }
    }
}
