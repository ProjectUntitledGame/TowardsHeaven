using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject spawnPoint;
    Vector2 dir;
    Rigidbody2D RB;
    BoxCollider2D BC;
    private int speed = 5;
    public GameObject currentProjectile;
    public GameObject player;
    int animState = 0;
    int currentState = 0;
    Animator anim;
    public float attackTime;
    public float maxAttackTime = 1f;
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        RB = transform.GetComponent<Rigidbody2D>();
        BC = transform.GetComponent<BoxCollider2D>();
        attackTime = maxAttackTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState = 5;
        dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (dir.y > 0) currentState = 2;
        if (dir.y < 0) currentState = 0;
        if (dir.x < 0) currentState = 1;
        if (dir.x > 0) currentState = 3;
        if (animState != currentState)
        {
            animState = currentState;
            anim.SetInteger("animState", animState);
        } 
        
       
            
            //currentWeapon.SetActive(true);
            //currentWeapon.GetComponent<MeleeWeapons>().attackDelay();
            
        
        
    }

    

    private void FixedUpdate()
    {
        RB.velocity = dir * speed;
       

    }

 

    
}
