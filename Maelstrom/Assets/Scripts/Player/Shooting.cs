 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private PlayerController PlayerController;
    public Transform firePoint;
    public GameObject bulletprefab;
    public InputAction Fire;

    public float bulletForce = 20f;
    public float fireCooldown = 2;
    public float CoolDownTime = .5f;
    public float bulletLife;
    public bool allowFire = true;


    void Start()
    {
        PlayerController = GameObject.FindObjectOfType<PlayerController>();
    }

    public void OnEnable()
    {
        Fire.Enable();
    }

    public void OnDisable()
    {
        Fire.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (Fire.IsPressed() && allowFire)
        {
            StartCoroutine(Shoot());
        }
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