using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBladeScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float BladeLeftCap;
    public float BladeRightCap;
    public float BladeSpeed;
    public float rotationSpeed;
    private bool Left = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BladeMove();
        this.transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void BladeMove()
    {
        if (Left)
        {
            if (transform.position.x > BladeLeftCap)
            {
                rb.velocity = new Vector2(-BladeSpeed, transform.position.y);
            }
            else
            {
                Left = false;
            }
        }
        else
        {
            if (transform.position.x < BladeRightCap)
            {
                rb.velocity = new Vector2(BladeSpeed, transform.position.y);
            }
            else
            {
                Left = true;
            }
        }
    }
}
