using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bl_Joystick joystick;
    private Vector2 direction;
    private Rigidbody2D rigidbody;
    private bool isDie = false;
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction.x = Input.GetAxisRaw("Horizontal");
        //direction.y = Input.GetAxisRaw("Vertical");
        direction.x = joystick.Horizontal;
        direction.y = joystick.Vertical;



    }

    private void FixedUpdate()
    {
        if (!isDie)
        {
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Camera mainCamera = GetComponentInChildren<Camera>();
                mainCamera.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Camera mainCamera = GetComponentInChildren<Camera>();
                mainCamera.transform.localScale = new Vector3(1, 1, 1);
            }
            rigidbody.MovePosition(rigidbody.position + direction * speed * Time.fixedDeltaTime);
        }

    }

    public void Die()
    {
        isDie = true;
    }
}
