using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPointCheck : MonoBehaviour
{
    public GameObject[] nearests;

    void Awake()
    {
        Invoke("GetNearestSpawns", 1.5f);
    }

    void GetNearestSpawns()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("CentrePoint");
        spawns = spawns.OrderBy((spawn) => (transform.position - spawn.transform.position).sqrMagnitude).ToArray();
        nearests = spawns;
    }
}
