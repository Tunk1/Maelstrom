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

    public Rigidbody2D myRB;
    public InputAction Move;
    public InputAction Shoot;
    public InputAction Fire;
    public Weapon Weapon;

    private bool isGamepad;

    private Vector2 pointerInput;
    public Vector2 position;

    private CharacterController controller;
    private Weapon weaponParent;
    public Transform bulletPrefab;
    public Transform firePoint;
    public GameObject bulletprefab;

    public float bulletForce = 20f;
    public float fireCooldown = 2;
    public float CoolDownTime = .5f;
    public float bulletLife;
    public bool allowFire = true;
    // Start is called before the first frame update
    void Start()
    {

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
        Fire.Enable();
    }
    private void OnDisable()
    {
        Move.Disable();
        Shoot.Disable();
        Fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        facingDirection = transform.eulerAngles.y;
        lookingDirection = transform.eulerAngles.y;
        lookingDirection = 180;
        myRB.velocity = Move.ReadValue<Vector2>() * moveSpeed;
        pointerInput = GetPointerInput();
        weaponParent.Pointerposition = pointerInput;
        if (Fire.IsPressed() && allowFire)
        {
            StartCoroutine(Shoot());
        }
        IEnumerator Shoot()
        {
            allowFire = false;
            GameObject bullet = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(CoolDownTime);
            allowFire = true;
        }
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
    }

}
