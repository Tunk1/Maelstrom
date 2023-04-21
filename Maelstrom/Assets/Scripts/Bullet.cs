using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject hitEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        if (other.CompareTag("Enemy"))
        { 
           Destroy(gameObject);
        }
        if (other.CompareTag("Level"))
        {
            Destroy(gameObject);
        }
    }
}
