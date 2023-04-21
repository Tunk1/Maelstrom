using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5;
    public float stoppingDistance;
    public float retreatDistance;
    public float detectDistance = 20;
    public float health = 5;

    private float cooldownTimer;
    public float shotStart;

    public GameObject EnemyBullet;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        cooldownTimer = shotStart;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) > stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }

        else if (Vector2 .Distance(transform.position, player.position) > detectDistance )
        {
            transform.position = this.transform.position;

        }

        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(cooldownTimer <= 0)
        {
            Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            cooldownTimer = shotStart;
            
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            health--;
        }

        if (other.CompareTag("Player"))
        {
            health--;
        }
    }
}
