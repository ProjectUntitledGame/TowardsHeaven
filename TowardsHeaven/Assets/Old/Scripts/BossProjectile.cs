using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField]
    int projectileCount;
    [SerializeField]
    GameObject projectile;
    Vector2 startPoint;
    float radius;
    float movespeed;


    private void Start()
    {
        radius = 5f;
        movespeed = 5f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Rooms"))
        {
            //SpawnProjectiles(projectileCount);
            Destroy(gameObject);
            Debug.Log("Rooms");
        }
        
    }

    private void Update()
    {
        startPoint = transform.position;
    }

    void SpawnProjectiles(int projectileCount)
    {
        float angleStep = 360f / projectileCount;
        float angle = 0f;

        for (int i = 0; i <= projectileCount - 1; i++)
        {
            float projectileDirXPos = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPos = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXPos, projectileDirYPos);
            Vector2 projectileMoveDir = (projectileVector - startPoint).normalized * movespeed;

            var projectileInstantiate = Instantiate(projectile, startPoint, Quaternion.identity);
            projectileInstantiate.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDir.x, projectileMoveDir.y);
            angle += angleStep;
        }

    }


}
