using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFrog : Enemy
{
    private enum State {idle, jump, fall}
    private State state = State.idle;

    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;

    [SerializeField] private LayerMask land;

    private bool FacingLeft = true;

    private Collider2D collider;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        StateControl();
        anim.SetInteger("state", (int)state);
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
                if (collider.IsTouchingLayers(land))
                {
                    rigid.velocity = new Vector2(-jumpLength, jumpHeight);
                    state = State.jump;
                }
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
                if (collider.IsTouchingLayers(land))
                {
                    rigid.velocity = new Vector2(jumpLength, jumpHeight);
                    state = State.jump;
                }
            }
            else
            {
                FacingLeft = true;
            }

        }
    }

    private void StateControl()
    {
        if (state == State.jump)
        {
            if(rigid.velocity.y < .1f)
            {
                state = State.fall;
            }
        }
        else if(state == State.fall)
        {
            if (collider.IsTouchingLayers(land))
            {
                state = State.idle;
            }
        }
    }
    }

 

