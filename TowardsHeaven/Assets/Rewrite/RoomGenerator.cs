using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DOORS MUST GO THIS ORDER IN HIERARCHY: LEFT, RIGHT, UP, DOWN

public class RoomGenerator : MonoBehaviour
{
    public RoomController roomController;
    public RoomInfo thisRoomInfo;
    public GameObject roomPrefab;

    bool roomInfoLoaded = false;

    void Awake()
    {

    }
    
    void Update()
    {
        //Delayed awake. Has to be in update because awake gets called before the roomController can tell this script what room it is, which means that everything in the class is default during Awake().
        if (thisRoomInfo.isRoom && !roomInfoLoaded)
        {
            ActualAwake();
        }
    }

    void ActualAwake()
    {
        roomInfoLoaded = true;

        Vector2 thisLocation = thisRoomInfo.position;

        if (!thisRoomInfo.left)
        {
            Destroy(transform.Find("LeftDoor").gameObject);
        }
        if (!thisRoomInfo.right)
        {
            Destroy(transform.Find("RightDoor").gameObject);
        }
        if (!thisRoomInfo.up)
        {
            Destroy(transform.Find("TopDoor").gameObject);
        }
        if (!thisRoomInfo.down)
        {
            Destroy(transform.Find("BottomDoor").gameObject);
        }
    }

    public void Triggered(int side)
    {
        thisRoomInfo.isActiveRoom = false;
        roomController.LoadRoom(thisRoomInfo, side);
        Destroy(gameObject);
    }
}

