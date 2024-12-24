using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapons : MonoBehaviour
{
    public int Damage = 1;
    public float attackTime = 0.5f;
    public float maxTime = 0.5f;
    private bool attacking = false;
    public GameObject player;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Enemy"))
        {
            other.gameObject.GetComponent<Health>().healthvalue -= Damage;

        }
    }

    private void Update()
    {
        if (attackTime <= 0)
        {
            attackTime = maxTime;
            attacking = false;
            //player.GetComponent<Movement>().AttackEnd();
            Debug.Log("Done");
            Debug.Log(attackTime);
        }
        else if(attacking == true)
        {
            attackTime -= Time.deltaTime;
        }
        //Debug.Log(attackTime);
    }

    public void attackDelay()
    {
        if(attacking == false)
        {
            attacking = true;
        }
        
    }
  

}
