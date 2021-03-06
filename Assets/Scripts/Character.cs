using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float playerLocalScale;
    [SerializeField]
    float speed;
    [SerializeField]
    int lives;
    [SerializeField]
    float jumpForce;
    Animator anim;
    SpriteRenderer sprite;
    [SerializeField]
    bool isGrounded;
    public GameObject bullet;
    public GameObject bulSpawn;
    public Rigidbody2D _rigidbody;
    Vector3 plStartPosition;
    void Start(){

        //_rigidbody = GetComponent<Rigidbody2D>();
        plStartPosition = this.transform.position;
        jumpForce = 530.0f;
        speed = 3.0f;
        lives = 15;
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Update(){        
        if(lives<=0){
            //Destroy(this.gameObject);
            this.transform.position = plStartPosition;
            lives = 15;
        }
    }
    void FixedUpdate(){
        if (Input.GetButton("Horizontal")) Run();
        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(50));
        if (Input.GetButtonDown("Jump")&& isGrounded) Jump();// 
        if (Input.GetButtonDown("Fire1")) Shoot();
          CheckGround();
    }
    public void Jump(){
        print("Jump()");
        _rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    void Run(){
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f);
        isGrounded = colliders.Length > 1;
    }
    void Shoot()
    {
        Vector3 position = bulSpawn.transform.position;
        Instantiate(bullet, position, bullet.transform.rotation);
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "EnemyBullet"){
            lives--;
        }
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
}