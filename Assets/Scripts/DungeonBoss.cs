using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DungeonBoss : Boss
{
    private Collider2D coll;
    private bool FacingLeft = true;
    public float leftCap;
    public float rightCap;
    public float speed = 2f;
    private Transform target;
    public int distance = 7;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

   protected void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < distance)
        {
            if (transform.position.x > target.position.x && transform.localScale.x != 0.09879303f)
            {
                transform.localScale = new Vector3(0.09879303f, 0.1016f, 1f);
            }
            else if (transform.position.x < target.position.x && transform.localScale.x != -0.09879303f)
            {
                transform.localScale = new Vector3(-0.09879303f, 0.1016f, 1f);
            }
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (FacingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 0.09879303f)
                {
                    transform.localScale = new Vector3(0.09879303f, 0.1016f, 1);
                }
                rb.velocity = new Vector2(-speed, rb.velocity.y);
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
                if (transform.localScale.x != -0.09879303f)
                {
                    transform.localScale = new Vector3(-0.09879303f, 0.1016f, 1);
                }
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                FacingLeft = true;
            }
        }
    }
    public void BossScaleController()
    {
        if (transform.position.x < target.position.x)
        {
            transform.Rotate(0f, 180f, 0f);
        }
        else if (transform.position.x > target.position.x)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
