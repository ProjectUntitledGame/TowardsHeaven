using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoomInfo
{
    public Vector2 position;
    public bool isRoom;
    public bool isBoss;
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool isActiveRoom;
}

public class RoomController : MonoBehaviour
{
    //Dungeon settings:
    public int minSize = 5;
    public int maxSize = 11;
    public int amountOfRooms = 10;
    public int mainRoutePercent = 40;
    public int otherRoutePercent = 40;
    public int deadEndPercent = 20;
    public int minDistanceToBoss = 2;

    public GameObject roomPrefab;
    public GameObject player;

    RoomInfo[,] RoomArray;
    int size;
    int half;
    Vector2 bossRoomPos;
    int remainingRooms;
    List<int> roomsForEach = new List<int>();
    List<Vector2> emptyRooms = new List<Vector2>();
    GameObject currentRoom;
    int splitAttempts = 0;


    private void Start()
    {
        size = Random.Range(minSize, maxSize+1); //Create a random number in range

        if (size % 2 != 1) //If the number comes up even, subtract 1 to make it odd. This guarantees that there is a 0,0 and that it's symmetrical
        {
            size--;
        }

        half = (size - 1) / 2; //so that the centre point is (0,0), RoomArray[0,0] has to be half the size-1.

        RoomArray = new RoomInfo[size, size]; //Create the 2D array

        for (int i = 0; i < size; i++) //Tell each item in the array its position.
        {
            for (int k = 0; k < size; k++) 
            {
                RoomArray[i, k].position = new Vector2(i - half, k - half);
            }
        }

        remainingRooms = amountOfRooms-1; //Set the remaining amount of rooms to the max amount to start with, -1 to include the start room.

        AddBossRoom();

        //Start creating the rooms
        RoomArray[half, half].isRoom = true;
        RoomArray[half, half].isActiveRoom = true;

        CreateRouteToBoss();
        CreateAltRoutes();
        CheckForAdjacentRooms();
        LoadRoom(RoomArray[half, half],-1);

    }
    //Draw a 2D visualisation of the dungeon.
    private void OnDrawGizmos()
    {
        if (RoomArray != null)
        {
            foreach (RoomInfo room in RoomArray)
            {
                Vector2 cur = room.position;
                if (room.isRoom && !room.isBoss)
                {
                    Gizmos.color = Color.red;

                    Gizmos.DrawCube(new Vector3(cur.x, cur.y, -5), Vector3.one / 2);
                }
                else if (!room.isRoom && !room.isBoss)
                {
                    Gizmos.color = Color.grey;

                    Gizmos.DrawCube(new Vector3(cur.x, cur.y, -5), Vector3.one / 2);
                }
                else if (room.isBoss)
                {
                    Gizmos.color = Color.blue;

                    Gizmos.DrawCube(new Vector3(cur.x, cur.y, -5), Vector3.one / 2);
                }
                Gizmos.color = Color.white;
                if (room.left)
                {
                    Gizmos.DrawCube(new Vector3(cur.x - 0.25f, cur.y, -6), Vector3.one / 8);
                }
                if (room.right)
                {
                    Gizmos.DrawCube(new Vector3(cur.x + 0.25f, cur.y, -6), Vector3.one / 8);
                }
                if (room.up)
                {
                    Gizmos.DrawCube(new Vector3(cur.x, cur.y + 0.25f, -6), Vector3.one / 8);
                }
                if (room.down)
                {
                    Gizmos.DrawCube(new Vector3(cur.x, cur.y - 0.25f, -6), Vector3.one / 8);
                }
                if (room.isActiveRoom)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(new Vector3(cur.x, cur.y, -6), Vector3.one / 4);
                }
            }
        }
    }

    private void AddBossRoom()
    {

        int xPosNeg = 1;
        int yPosNeg = 1;
        for (int i = 0; i < 2; i++)
        {
            int posNeg = Random.Range(0, 2);
            if (i == 0)
            {
                if (posNeg != 0) 
                {
                    xPosNeg = 1;
                }
                else
                {
                    xPosNeg = -1;
                }
            }
            else if (i == 1)
            {
                if (posNeg != 0)
                {
                    yPosNeg = 1;
                }
                else
                {
                    yPosNeg = -1;
                }
            }
        }
        //print(xPosNeg + " " + yPosNeg);

        bossRoomPos = new Vector2(Random.Range(minDistanceToBoss, half+1)*xPosNeg, Random.Range(minDistanceToBoss, half + 1)*yPosNeg);
        RoomArray[Mathf.FloorToInt(bossRoomPos.x + half), Mathf.FloorToInt(bossRoomPos.y + half)].isBoss = true;

    }
       
    private void CreateRouteToBoss()
    {
        int roomQuota = Mathf.FloorToInt((amountOfRooms / 100f) * 40);
        int centre = Mathf.FloorToInt(size / 2);
        int xPos = 0;
        int yPos = 0;
        int bossX = Mathf.FloorToInt(bossRoomPos.x);
        int bossY = Mathf.FloorToInt(bossRoomPos.y);
        //print("before loop. xPos = " + xPos + " yPos = " + yPos + " bossX = " + bossX + " bossY = " + bossY);
        while (xPos!=bossX || yPos != bossY)
        {
            bool bossXReached = false;
            bool bossYReached = false;
            if (xPos == bossX)
            {
                bossXReached = true;
            }
            else if (yPos == bossY)
            {
                bossYReached = true;
            }
            //Start
            if (!bossXReached && !bossYReached)
            {
                bool xOrY = Random.Range(0, 2) == 0;
                if (xOrY)
                {
                    if (bossX < xPos)
                    {
                        xPos--;
                    }
                    else if (bossX > xPos)
                    {
                        xPos++;
                    }
                }
                else if (!xOrY)
                {
                    if (bossY < yPos)
                    {
                        yPos--;
                    }
                    else if (bossY > yPos)
                    {
                        yPos++;
                    }
                }

                RoomArray[xPos + half, yPos + half].isRoom = true;
            }
            else if (bossXReached && !bossYReached)
            {
                if (bossY < yPos)
                {
                    yPos--;
                }
                else if (bossY > yPos)
                {
                    yPos++;
                }

                RoomArray[xPos + half, yPos + half].isRoom = true;
            }
            else if (!bossXReached && bossYReached)
            {
                if (bossX < xPos)
                {
                    xPos--;
                }
                else if (bossX > xPos)
                {
                    xPos++;
                }

                RoomArray[xPos + half, yPos + half].isRoom = true;
            }
            //print("after loop. xPos = " + xPos + " yPos = " + yPos + " bossX = " + bossX + " bossY = " + bossY);
            remainingRooms--;
            //print(remainingRooms);

        }
    }

    void SplitRandomly(int numberToSplit)
    {
        if (numberToSplit < 3)
        {
            numberToSplit = 3;
        }
        
        roomsForEach.Clear();
        if (splitAttempts > 9)
        {
            print("Reached the recursion limit! Manually setting the percentages.");
            int oneThird = Mathf.FloorToInt(numberToSplit / 3);
            int remainder = numberToSplit - oneThird * 3;
            roomsForEach.Add(oneThird + remainder);
            roomsForEach.Add(oneThird);
            roomsForEach.Add(oneThird);
            print(oneThird + " remainder: " + remainder);
            return;
        }
        splitAttempts++;

        int startingAmountOfRooms = numberToSplit;
        int remainingPercentage = startingAmountOfRooms; //numbersToSplit
        for (int i = 0; i < 2; i++) //howManySplits        //One less than the amount desired, since the last one will be the remainder.
        {
            int randomNumber = Random.Range(1, remainingPercentage + 1);
            roomsForEach.Add(randomNumber);
            remainingPercentage -= randomNumber;
        }
        roomsForEach.Add(remainingPercentage);
        roomsForEach.Sort();
        int[] results = roomsForEach.ToArray();
        foreach (int num in results)
        {
            if (num <= 0)
            {
                int x = numberToSplit;
                remainingPercentage = startingAmountOfRooms;
                SplitRandomly(remainingPercentage); //this is to try and fix an error where it doesn't always generate 4 numbers which i cannot figure out what is causing it exactly but its something to do with this.
                return;
            }


        }
    }

    private void CreateAltRoutes()
    {
        //Find out which sides of spawn haven't got rooms
        List<Vector2> emptySidesOfSpawn = new List<Vector2>();
        Vector2[] roomsToCheck = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) };
        for (int i = 0; i < roomsToCheck.Length; i++)
        {
            int x = Mathf.FloorToInt(roomsToCheck[i].x);
            int y = Mathf.FloorToInt(roomsToCheck[i].y);

            if (RoomArray[x+half, y+half].isRoom == false)
            {
                emptySidesOfSpawn.Add(new Vector2(x, y));
            }
            
        }

        SplitRandomly(remainingRooms);
        int[] roomsEach = roomsForEach.ToArray();

        for (int i = 0; i<emptySidesOfSpawn.Count; i++)
        {
            int roomsAllowed = roomsEach[i];
            Vector2 lastRoom = new Vector2(0, 0);
            while (roomsAllowed > 0)
            {
                GetEmptySpacesAroundPoint(Mathf.FloorToInt(lastRoom.x), Mathf.FloorToInt(lastRoom.y));
                if (emptyRooms.Count > 0)
                {
                    Vector2[] freeRooms = emptyRooms.ToArray();
                    Vector2 selectedRoom = freeRooms[Random.Range(0, freeRooms.Length)];
                    int x = Mathf.FloorToInt(selectedRoom.x);
                    int y = Mathf.FloorToInt(selectedRoom.y);
                    if (RoomArray[x + half, y + half].isRoom)
                    {
                        print(x + "," + y + " already exists!! Trying again.");
                        continue;
                    }
                    RoomArray[x + half, y + half].isRoom = true;
                    lastRoom = new Vector2(x, y);
                }
                roomsAllowed--; 
                
            }
        }
    }

    private void GetEmptySpacesAroundPoint(int x, int y)
    {
        x = x + half;
        y = y + half;
        emptyRooms.Clear();
        if (x + 1 < RoomArray.GetLength(0))
        {
            if (!RoomArray[x+1, y].isRoom)
            {
                emptyRooms.Add(new Vector2((x + 1) - half, y - half));
            }
        }
        if (x - 1 >= 0)
        {
            if (!RoomArray[x-1, y].isRoom)
            {
                emptyRooms.Add(new Vector2((x - 1) - half, y - half));
            }
        }
        if (y + 1 < RoomArray.GetLength(0))
        {
            if (!RoomArray[x, y + 1].isRoom)
            {
                emptyRooms.Add(new Vector2(x - half, (y + 1) - half));
            }
        }
        if (y - 1 >= 0)
        {
            if (!RoomArray[x, y - 1].isRoom)
            {
                emptyRooms.Add(new Vector2(x - half, (y - 1) - half));
            }
        }
        if (emptyRooms.Count == 0)
        {
            while (emptyRooms.Count == 0){
                int randX = Random.Range(0, size);
                int randY = Random.Range(0, size);
                if (RoomArray[randX, randY].isRoom)
                {
                    GetEmptySpacesAroundPoint(randX - half, randY - half);
                    if (emptyRooms.Count > 0)
                    {
                        break;
                    }
                }

            }
        }
        
    }

    private void CheckForAdjacentRooms()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (RoomArray[x, y].isRoom)
                {
                    if (x + 1 < size)
                    {
                        if (RoomArray[x + 1, y].isRoom)
                        {
                            RoomArray[x, y].right = true;
                        }
                    }
                    if (x - 1 >= 0)
                    {
                        if (RoomArray[x - 1, y].isRoom)
                        {
                            RoomArray[x, y].left = true;
                        }
                    }
                    if (y + 1 < size)
                    {
                        if (RoomArray[x, y + 1].isRoom)
                        {
                            RoomArray[x, y].up = true;
                        }
                    }
                    if (y - 1 >= 0)
                    {
                        if (RoomArray[x, y - 1].isRoom)
                        {
                            RoomArray[x, y].down = true;
                        }
                    }
                }
            }
        }
    }

    public void LoadRoom(RoomInfo room, int hitSide) //set hitSide to -1 if it's the first room.
    { //Rooms work in this order: left, right, up, down, where left = 0 and down = 3.

        player.transform.position = new Vector3(0, -0.5f, -5); //Set player to centre. this is temporary.

        RoomArray[Mathf.FloorToInt(room.position.x) + half, Mathf.FloorToInt(room.position.y) + half] = room; //Update the room in the array

        RoomInfo newRoom = room;

        int newX = Mathf.FloorToInt(room.position.x);
        int newY = Mathf.FloorToInt(room.position.y);

        if (hitSide >= 0)
        {
            if (hitSide == 0)
            {
                newRoom = RoomArray[newX - 1 + half, newY + half];
                RoomArray[newX - 1 + half, newY + half].isActiveRoom = true;
            }
            else if (hitSide == 1)
            {
                newRoom = RoomArray[newX + 1 + half, newY + half];
                RoomArray[newX + 1 + half, newY + half].isActiveRoom = true;
            }
            else if (hitSide == 2)
            {
                newRoom = RoomArray[newX + half, newY + 1 + half];
                RoomArray[newX + half, newY + 1 + half].isActiveRoom = true;
            }
            else if (hitSide == 3)
            {
                newRoom = RoomArray[newX + half, newY - 1 + half];
                RoomArray[newX + half, newY - 1 + half].isActiveRoom = true;
            }
        }
        

        GameObject roomObject = Instantiate(roomPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
        newRoom.isActiveRoom = true;
        RoomGenerator roomGenerator = roomObject.GetComponent<RoomGenerator>();
        roomGenerator.thisRoomInfo = newRoom;
        roomGenerator.roomController = this;

        currentRoom = roomObject;

    }
    
}
