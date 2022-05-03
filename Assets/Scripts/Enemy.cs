using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected AudioSource explosion;
   protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        explosion = GetComponent<AudioSource>();
    }
    public void JumpedOn()
    {
        explosion.Play();
        anim.SetTrigger("Death");
        rigid.velocity = Vector2.zero;
        rigid.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
