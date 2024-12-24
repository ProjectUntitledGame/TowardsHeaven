using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = transform.position;
        int side = 0;
        if (position.x < -1)
        {
            side = 0;
        }
        else if (position.x > 1)
        {
            side = 1;
        }
        else if (position.y > 1)
        {
            side = 2;
        }
        else if (position.y < -1)
        {
            side = 3;
        }
        GetComponentInParent<RoomGenerator>().Triggered(side);
    }
}
