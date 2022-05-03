using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossumEnemy : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speed = 3f;

    private Collider2D coll;

    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    
   private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
            }
            else
            {
                facingLeft = false;
            }

        }
        else
        {
            if(transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
