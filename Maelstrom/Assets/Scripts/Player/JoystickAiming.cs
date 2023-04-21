using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickAiming : MonoBehaviour
{
    private PlayerController PlayerController;
    public Transform weaponParent;
    public InputAction Shoot;
    public float returnTime = -.8f;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Shoot.Enable();
    }
    void Start()
    {
        PlayerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();


        Vector2 scale = transform.localScale;
        if (Shoot.ReadValue<Vector2>().x < 0)
        {
            scale.y = -1;
        }
    }
    public void Aim()
    {
        Vector3 Angle = weaponParent.transform.localEulerAngles;


        weaponParent.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(Shoot.ReadValue<Vector2>().x, Shoot.ReadValue<Vector2>().y) * -180 / Mathf.PI + 90f);
        

        Vector2 scale = transform.localScale;
        if (weaponParent.transform.localEulerAngles.x < 0)
        {
            scale.y = -1;
        }
    }
}
