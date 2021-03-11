using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Vector3 plStartPosition;
    public Rigidbody2D _rigidbody;
    [SerializeField]
    bool isKneeing;
    [SerializeField]
    float playerLocalScale;
    [SerializeField]
    float speed;
    [SerializeField]
    int maxHealth = 8;
    bool isDie;
    int curentHealth;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    Transform groundCheck;
    //Переменные для выстрела и пули
    public GameObject bullet;
    public GameObject bulSpawn;
    Vector3 bulSpawnUp;
    //Animation setting
    Animator anim;
    //SpriteRenderer sprite;
    CharState State
    {
        get { return (CharState)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    //Подключенные скрипты:
    public HealthBar healthBar;
    //public ScoreBar scoreBar;
    void Start()
    {
        //_rigidbody = GetComponent<Rigidbody2D>();
        plStartPosition = this.transform.position;
        jumpForce = 530.0f;
        speed = 3.0f;
        curentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
        State = CharState.Stand;
        isDie = false;
        isKneeing = false;
        bulSpawnUp = bulSpawn.transform.localPosition;
        //sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        CheckGround();
        if (Input.GetButton("Horizontal") && !isDie)
        {
            Walk();
            if (isGrounded == true)
                State = CharState.Walk;
        }

        if (Input.GetButton("VerticalDown") && !isDie)
        {
            if (isGrounded == true)
                State = CharState.Kneeing;
            isKneeing = true;
            bulSpawn.transform.localPosition = new Vector3(0.46f, 0.009f, 0);
            GetComponent<BoxCollider2D>().enabled = !isKneeing;
        }
        else
        {
            isKneeing = false;
            bulSpawn.transform.localPosition = bulSpawnUp; 
            GetComponent<BoxCollider2D>().enabled = !isKneeing;
        }
        if (Input.GetButton("Jump") && isGrounded && !isDie)
            Jump();

        //if (Input.GetButton("Fire1")) StartCoroutine(ShootLimiter());

        if (!Input.GetButton("Horizontal") &&
            !Input.GetButtonDown("Jump") &&
             !Input.GetButtonDown("Fire1") &&
              !Input.GetButton("VerticalDown") &&
                !isDie &&
             isGrounded)
        { State = CharState.Idle; }
        //Проверка состояния для анимаций
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
            SingleShooting();
        if (curentHealth == 0) 
            StartCoroutine(Die());
    }
    public void Jump()
    {
        //print("Jump()");

        _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = CharState.Jump;
    }
    void Walk()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(
                                        transform.position,
                                        transform.position + direction,
                                        speed * Time.deltaTime);
        if (direction.x < 0f)
            this.transform.localScale = new Vector3(-playerLocalScale,
                                                     playerLocalScale,
                                                     playerLocalScale);
        else
            this.transform.localScale = new Vector3(playerLocalScale,
                                                    playerLocalScale,
                                                    playerLocalScale);
    }
    void CheckGround()
    {
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        // isGrounded = colliders.Length > 1;
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            curentHealth--;
            healthBar.SetHealth(curentHealth);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlameTrap")
        {
            curentHealth--;
            healthBar.SetHealth(curentHealth);
        }
    }
    void SingleShooting()
    {
        Vector3 position = bulSpawn.transform.position;
        Instantiate(bullet, position, bullet.transform.rotation);
    }
    // IEnumerator ShootLimiter(){
    //     yield return StartCoroutine(Shooting());
    // }
    // IEnumerator Shooting(){
    //     yield return new WaitForSeconds(.1f);
    //     SingleShooting();
    // }
    public void ReceivedDamage(int damage){
        curentHealth-=damage;
    }
    IEnumerator Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        isDie = true;
        curentHealth = -1;
        State = CharState.Die;
        yield return new WaitForSeconds(2f);
        this.transform.position = plStartPosition;
        isDie = false;
        curentHealth = maxHealth;
        healthBar.SetHealth(curentHealth);
        print("curentHealth = maxHealth "+ curentHealth);
        GetComponent<BoxCollider2D>().enabled = true;
        //Destroy(this.gameObject);
    }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.name == "FloorDown")
    //     {
    //         transform.position = rPoint.position;
    //     }
    //     if (collision.gameObject.name == "EndLevel")
    //     {
    //         transform.position = rPoint.position;
    //     }
    //     Destroy(this.gameObject);
    // }
    public enum CharState
    {
        Stand,
        Idle,
        Walk,
        Jump,
        ShootintAuto9,
        Die,
        Kneeing
    }
}

