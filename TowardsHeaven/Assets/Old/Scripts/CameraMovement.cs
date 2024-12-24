using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float offset = -9;
    public float smoothSpeed = 0.125f;
    GameObject currentSpawnPoint;
    GameObject[] nearest;
    private Transform target;
    private float xMin, xMax, yMin, yMax;
    private Tilemap tilemap;
    [SerializeField]
    private GameObject LoadingScreen;
    
   


    private void Start()
    {
        currentSpawnPoint = CheckNearestTarget(); //Setting the nearest spawn point as the current.
        target = player.transform;
        LoadingScreen.SetActive(true);
        




        Invoke("RetrieveNearestSpawns", 2f); //Wait 1 seconds before searching because of generation times
        Invoke("SetTileMap", 2f);


    }

    void FixedUpdate()
    {
        /*Vector3 desiredPos = target.position + offset;
        //Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        //transform.position = smoothedPos;

        //Check nearest spawn point
        //if (transform.position.x)*/

    }

    void Update()
    {
        float distanceGoingIn = Vector3.Distance(player.transform.position, currentSpawnPoint.transform.position);
        float distance = distanceGoingIn;
        GameObject newNearest = null;
        if (nearest!=null)
        {
            foreach (GameObject cur in nearest)
            {
                float distanceFromCur = Vector3.Distance(player.transform.position, cur.transform.position);
                if (distanceFromCur < distance)
                {
                    distance = distanceFromCur;
                    newNearest = cur;
                }
            }
        }
        if (distance < distanceGoingIn)
        {
            SetCameraToNext(newNearest);
        }


    }

    private void LateUpdate()
    {
        //Apparently this is necessary because player moves in fixed update so the camera follow should happen after the player moves, which late update is.
        //Enable this to make camera move again.
        //transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax),offset);
    }

    GameObject CheckNearestTarget()
    {

        GameObject[] spawns;
        spawns = GameObject.FindGameObjectsWithTag("CentrePoint");
        GameObject closest = null;
        Vector3 position = player.transform.position;
        float distance = Mathf.Infinity;
        foreach (GameObject cur in spawns)
        {
            Vector3 difference = cur.transform.position - position;
            float curDistance = difference.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = cur;
                distance = curDistance;
            }
        }
        Debug.Log("Nearest targets set, qty: " + spawns.Length);
        return closest;
    }

    void RetrieveNearestSpawns()
    {
        if (!currentSpawnPoint)
        {
            currentSpawnPoint = CheckNearestTarget();
        }

        GameObject[] nearestSpawnsToCurrent = currentSpawnPoint.GetComponent<SpawnPointCheck>().nearests;
        int length = nearestSpawnsToCurrent.Length;
        List<GameObject> list = new List<GameObject>(nearestSpawnsToCurrent);
        List<GameObject> dupeList = new List<GameObject>(list);
        foreach (GameObject cur in list)
        {
            if (list.IndexOf(cur) == 0 || list.IndexOf(cur) > 4)
            {
                dupeList.Remove(cur);
            }
        }
        
        
        //list.RemoveAt(0); //Remove the centre spawn point from the list
        nearestSpawnsToCurrent = dupeList.ToArray();
        
        nearest = nearestSpawnsToCurrent;
    }

    void SetCameraToNext(GameObject newSpawn)
    {
        transform.position = new Vector3(newSpawn.transform.position.x, newSpawn.transform.position.y, offset);
        currentSpawnPoint = newSpawn;
        RetrieveNearestSpawns();
        //SetTileMap(); //Update the tilemap to the grid the player is in.
    }
    
    private void SetLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        xMin = minTile.x + width / 2;
        xMax = maxTile.x - (width / 2)-10;
        yMin = minTile.y + height / 2;
        yMax = maxTile.y - height / 2;


    }

    /*private void SetTileMap()
    {
        GameObject Grid = currentSpawnPoint.transform.parent.Find("Grid").gameObject;
        tilemap = Grid.transform.Find("GroundBase").GetComponent<Tilemap>();
        
        //New camera movement idea
        Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
        Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);

        SetLimits(minTile, maxTile);
        transform.position = new Vector3(currentSpawnPoint.transform.position.x, currentSpawnPoint.transform.position.y, offset); // Set camera to centre incase it isnt already
        
    }*/

    /*private float LoadingScreenPercentage()
    {

        return ;
    }*/

}

