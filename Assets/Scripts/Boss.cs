using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float BossHP = 100f;
    public Slider slider;
    public TextMeshProUGUI SliderValue;
    public GameObject canvas;
    public GameObject Chest;

    public static Boss boss;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!boss)
        {
            boss = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BossDamage()
    {
        BossHP -= 10f;
        slider.value = BossHP;
        SliderValue.text = BossHP.ToString();
        if (BossHP <= 0)
        {
            Destroy(gameObject);
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled = false;
            Destroy(canvas);
            Instantiate(Chest, transform.position, Quaternion.identity);
        }
    }
    public void SetBossHpActive()
    {
        canvas.SetActive(true);
    }
}
