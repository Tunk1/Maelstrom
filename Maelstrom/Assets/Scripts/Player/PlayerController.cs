using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 7;
    public float facingDirection;
    public float lookingDirection;
    public float health = 20;
    public float maxHealth = 10;

    public float dashSpeed = 15;
    public float dashLength = .5f, dashCooldown = 1f;
    public float activeMoveSpeed;
    private float dashCounter;
    private float dashCool;

    public Rigidbody2D myRB;
    public InputAction Move;
    public InputAction Shoot;
    public InputAction Dodge;

    public Weapon Weapon;

    private bool isGamepad;
    public bool Dashable;

    private Vector2 pointerInput;
    public Vector2 position;
    private Vector3 rollDir;


    private CharacterController controller;
    private Weapon weaponParent;
    public Transform bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
        Physics2D.IgnoreCollision(bulletPrefab.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //Create Rigidbody
        myRB = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController>();
        weaponParent = GetComponentInChildren<Weapon>();
    }
    private void OnEnable()
    {
        //Enables Input System Actions
        Move.Enable();
        Shoot.Enable();
        Dodge.Enable();
    }
    private void OnDisable()
    {
        Move.Disable();
        Shoot.Disable();
        Dodge.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCool = dashCooldown;

            }
        }
        if (dashCool > 0)
        {
            dashCool -= Time.deltaTime;
        }


        //Dodge Stuff
        if (Dodge.IsPressed())
        {

           
            if (dashCool <=0 && dashCounter <=0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
            
        }

         if (health <= 0)
         {
                    Destroy(gameObject);
         }
         facingDirection = transform.eulerAngles.y;
         lookingDirection = transform.eulerAngles.y;
         lookingDirection = 180;
         myRB.velocity = Move.ReadValue<Vector2>() * activeMoveSpeed;
         pointerInput = GetPointerInput();
         weaponParent.Pointerposition = pointerInput;
          
    }
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = Shoot.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);

    }
    public void RotatePlayer0()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    public void RotatePlayer180()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, lookingDirection, 0));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            health--;
        }
        if (other.CompareTag("Enemy"))
        {
            health--;

        }
        if (other.CompareTag("Health") && health < maxHealth)
        {
            Destroy(other.gameObject);
            health++;
        }
    }

}
