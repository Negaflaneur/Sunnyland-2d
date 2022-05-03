using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
 private Rigidbody2D rb;
  private Animator anim;
    private Collider2D coll;

    public string SceneName;
    public int SceneIndex;
    public int CurrentSceneIndex = 1;
    public int PreviousSceneIndex;

    public static PlayerController playerController;

    public bool Level1 = true;
    public bool Level2 = false;
    public bool Level3 = false;
    public bool Level4 = false;
    public int LevelIndex = 1;

    public CinemachineVirtualCamera cameraFx;
    private Transform target;
    public float offset = 0.25f;

    [SerializeField] private string Scene;
    [SerializeField] private LayerMask ground;

    private Color originalColor;
    private bool getCheckPoint;

    private Transform dialogueCharacter;
    private bool Invul;

    private enum State {idle, running, jumping, falling, hurt, climb, crouch}
    private State state = State.idle;
    private bool isCrouching = false;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float climbSpeed = 7;
    [SerializeField] private float jump = 15f;
    [SerializeField] private float hurtForce = 1f;

    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource cherrySound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource PowerUpSound;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        originalColor = GetComponent<SpriteRenderer>().color;
        permanentUi.perm.healthPoints = 100f;
        permanentUi.perm.Health.text = permanentUi.perm.healthPoints.ToString();

        Invul = false;
        getCheckPoint = false;

        if (getCheckPoint)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));
        }

        Music.musicInstance.music.Play();

        CheckScene();
        if (!playerController)
        {
            playerController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
 private void Update()
    {
        if ((state != State.hurt) && (DialogueScript.dialogueScript.DialogueIsActive != true))
        {
            Movement();
        }
        VelocityState();
        anim.SetInteger("state", (int)state);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        CameraControlller();
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            CherryCollactableFX cherryCollactable = collision.gameObject.GetComponent<CherryCollactableFX>();
            cherryCollactable.SetCherryAnimation();
            cherrySound.Play();
        }
        if (collision.tag == "PowerUp")
        {
            PowerUpSound.Play();
            Destroy(collision.gameObject);
            permanentUi.perm.Pcounter += 1;
            permanentUi.perm.gemText.text = permanentUi.perm.Pcounter.ToString();
            speed = 12f;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(PowerUp());

        }
        if (collision.tag == "PowerUpJump")
        {
            PowerUpSound.Play();
            Destroy(collision.gameObject);
            permanentUi.perm.Pcounter += 1;
            permanentUi.perm.gemText.text = permanentUi.perm.Pcounter.ToString();
            jump = 18f;
            GetComponent<SpriteRenderer>().color = Color.blue;
            StartCoroutine(PowerUpJump());
        }
        if (collision.tag == "PowerUpScale")
        {
            PowerUpSound.Play();
            Destroy(collision.gameObject);
            permanentUi.perm.Pcounter += 1;
            permanentUi.perm.gemText.text = permanentUi.perm.Pcounter.ToString();
            GetComponent<SpriteRenderer>().color = Color.green;
            permanentUi.perm.healthPoints = 200f;
            permanentUi.perm.Health.text = permanentUi.perm.healthPoints.ToString();
            StartCoroutine(PowerUpHealth());
        }
        if (collision.tag == "PowerUpTime")
        {
            PowerUpSound.Play();
            Destroy(collision.gameObject);
            permanentUi.perm.Pcounter += 1;
            permanentUi.perm.gemText.text = permanentUi.perm.Pcounter.ToString();
            GetComponent<SpriteRenderer>().color = Color.black;
            Time.timeScale = 0.5f;
            StartCoroutine(PowerUpTime());
        }
        if (collision.tag == "PowerUpInvul")
        {
            PowerUpSound.Play();
            Destroy(collision.gameObject);
            permanentUi.perm.Pcounter += 1;
            permanentUi.perm.gemText.text = permanentUi.perm.Pcounter.ToString();
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Invul = true;
            StartCoroutine(InvPowerUp());
        }
        if (collision.tag == "Armor")
        {
            permanentUi.perm.HasArmor = true;
            if (permanentUi.perm.ArmorPoints < 100f)
            {
                permanentUi.perm.ArmorPoints += 50f;
            }
            permanentUi.perm.Armor.text = permanentUi.perm.ArmorPoints.ToString();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "HealingItem")
        {
            if (permanentUi.perm.healthPoints < 100f && permanentUi.perm.ArmorPoints < 100f)
            {
                permanentUi.perm.healthPoints += 20f;
                permanentUi.perm.Health.text = permanentUi.perm.healthPoints.ToString();
                permanentUi.perm.ArmorPoints += 25f;
                permanentUi.perm.Armor.text = permanentUi.perm.ArmorPoints.ToString();
                Destroy(collision.gameObject);
            }
        }
        if (collision.tag == "Chest")
        {
            ChestScript chestWithItems = collision.GetComponent<ChestScript>();
            chestWithItems.SpawnRandomItem();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "FallEdge")
        {
            permanentUi.perm.healthPoints = 0;
            permanentUi.perm.Health.text = permanentUi.perm.healthPoints.ToString();
            SceneManager.LoadScene(Scene);
        }
        if (collision.tag == "Shop")
        {
            ShopScript shopScript = collision.GetComponent<ShopScript>();
            shopScript.SetShopMenuActive();
        }
        if (collision.tag == "Dialogue")
        {
            DialogueScript dialogue = collision.GetComponent<DialogueScript>();
            dialogue.SetDialogueActive();
            rb.velocity = new Vector2(0, 0);
           /* if(dialogue.DialogueIsActive != false)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
            if(dialogue.DialogueIsActive == false)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }*/
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            state = State.climb;
            Ladder();
        }
        if (collision.tag == "Checkpoint")
        {
            PlayerPrefs.SetFloat("xPos", collision.transform.position.x);
            PlayerPrefs.SetFloat("yPos", collision.transform.position.y);
            getCheckPoint = true;
        }
        if (collision.tag == "Trap")
        {
            state = State.hurt;
            ArmorController();
            HealthController();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                if (Invul == false)
                {
                    state = State.hurt;
                    ArmorController();
                    HealthController();
                }
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
        if (other.gameObject.tag == "Boss")
        {
            //Boss boss = other.gameObject.GetComponent<Boss>();
            //boss.SetBossHpActive();
            Boss.boss.SetBossHpActive();
            if (state == State.falling)
            {
                //boss.BossDamage();
                Boss.boss.BossDamage();
                Jump();
            }
            else
            {
                state = State.hurt;
                ArmorController();
                BossHealthController();
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);
        state = State.jumping;
    }
    private void Movement()
    {
        float HDirection = Input.GetAxis("Horizontal");
        float dirX = CrossPlatformInputManager.GetAxis("Horizontal");
        if (dirX > 0 || HDirection > 0)
        {
            rb.velocity = new Vector2(speed - 2f, rb.velocity.y);
            //rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
            transform.localScale = new Vector2(1, 1);
        }
        else if (dirX < 0 || HDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            //rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
            transform.localScale = new Vector2(-1, 1);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
        else if (CrossPlatformInputManager.GetButtonDown("Crouch") && coll.IsTouchingLayers(ground))
        {
            state = State.crouch;
            isCrouching = true;
        }
    }
    private void VelocityState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (isCrouching)
        {
            StartCoroutine(DisableCrouch());
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
             {
                state = State.idle;
             }
        }
        else if (state == State.climb)
        {
            if (Mathf.Abs(rb.velocity.y) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else if(state == State.running)
        {
            if(Mathf.Abs(rb.velocity.x) < 2f)
            {
                state = State.idle;
            }
        }
        else
        {
            state = State.idle;
        }
    }
    private void GrassSound()
    {
        footstep.Play();
    }
    private void JumpSound()
    {
        jumpSound.Play();
    }
    private void HurtSound()
    {
        hurtSound.Play();
    }
    private IEnumerator DisableCrouch()
    {
        yield return new WaitForSeconds(0.35f);
        isCrouching = false;
    }
    private IEnumerator PowerUp()
    {
        yield return new WaitForSeconds(5);
        speed = 7f;
        GetComponent<SpriteRenderer>().color = originalColor;
    }
    private IEnumerator PowerUpJump()
    {
        yield return new WaitForSeconds(5);
        jump = 15f;
        GetComponent<SpriteRenderer>().color = originalColor;
    }
    private IEnumerator PowerUpHealth()
    {
        yield return new WaitForSeconds(15);
        GetComponent<SpriteRenderer>().color = originalColor;
        permanentUi.perm.healthPoints = 100f;
        permanentUi.perm.Health.text = permanentUi.perm.healthPoints.ToString();

    }
    private IEnumerator PowerUpTime()
    {
        yield return new WaitForSeconds(10);
        GetComponent<SpriteRenderer>().color = originalColor;
        Time.timeScale = 1f;
    }
    private IEnumerator InvPowerUp()
    {
        yield return new WaitForSeconds(10);
        GetComponent<SpriteRenderer>().color = originalColor;
        Invul = false;
    }
    private void HealthController()
    {
        if (permanentUi.perm.HasArmor == false)
        {
            permanentUi.perm.healthPoints -= 20f;
            permanentUi.perm.Health.text = permanentUi.perm.healthPoints.ToString();
            if (permanentUi.perm.healthPoints <= 0)
            {
                SceneManager.LoadScene(Scene);
            }
        }
    }
    private void BossHealthController()
    {
        if (permanentUi.perm.HasArmor == false)
        {
            permanentUi.perm.healthPoints -= 50f;
            permanentUi.perm.Health.text = permanentUi.perm.healthPoints.ToString();
            if (permanentUi.perm.healthPoints <= 0)
            {
                SceneManager.LoadScene(Scene);
            }
        }
    }
    private void ArmorController()
    {
        if (permanentUi.perm.HasArmor)
        {
            permanentUi.perm.ArmorPoints -= 25f;
            permanentUi.perm.Armor.text = permanentUi.perm.ArmorPoints.ToString();
        }
        if (permanentUi.perm.ArmorPoints <= 0)
        {
            StartCoroutine(CheckHasArmor());
        }
    }
    private IEnumerator CheckHasArmor()
    {
        yield return new WaitForSeconds(0.1f);
        permanentUi.perm.HasArmor = false;
    }
    private void Ladder()
    {
        float YDirection = Input.GetAxis("Vertical");
        if (YDirection > 0)
        {
            rb.velocity = new Vector2(0, climbSpeed);
        }
        else if (YDirection < 0)
        {
            rb.velocity = new Vector2(0, -climbSpeed);
        }
    }
    public void CheckScene()
    {
        SceneName = SceneManager.GetActiveScene().name;
        SceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PreviousSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("levelReached", PreviousSceneIndex - 3);
    }
    private void CameraControlller()
    {
        target = GameObject.FindGameObjectWithTag("VCam").GetComponent<Transform>();
        target.position = new Vector3(transform.position.x + offset, transform.position.y + offset, target.position.z);
    }
}


