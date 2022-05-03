using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEagle : Enemy
{
    private Collider2D coll;
   // private Rigidbody2D rigid;

    [SerializeField] private float flySpeed = 3f;
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    private bool FacingLeft = false;

    protected override void Start()
    {
        base.Start();
       // rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    protected void Update()
    {
        Move();
    }

    private void Move()
    {
        if (FacingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                rigid.velocity = new Vector2(-flySpeed, transform.position.y);
            }
            else
            {
                FacingLeft = false;
            }

        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                rigid.velocity = new Vector2(flySpeed, transform.position.y);
            }
            else
            {
                FacingLeft = true;
            }

        }
    }
}
