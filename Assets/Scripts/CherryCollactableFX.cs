using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCollactableFX : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetCherryAnimation()
    {
        anim.SetTrigger("Destroy");
        GetComponent<Collider2D>().enabled = false;
        permanentUi.perm.cherries += 1;
        permanentUi.perm.cherryText.text = permanentUi.perm.cherries.ToString();
    }

    private void Des()
    {
        Destroy(this.gameObject);
    }
}
