using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public float LaunchingSpeed = 3f;
    public int distance = 4;
    private Transform target;
    private Animator anim;
    public AudioSource exp;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Launch();
    }

    private void Launch()
    {
        if (Vector2.Distance(transform.position, target.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, LaunchingSpeed * Time.deltaTime);
        }
        if (transform.position.x == target.position.x)
        {
            anim.SetTrigger("Death");
            GetComponent<Collider2D>().enabled = false;
            exp.Play();
        }

    }

    public void Des()
    {
        Destroy(gameObject);
    }
}
