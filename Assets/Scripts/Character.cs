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
    Animation animator;
    SpriteRenderer sprite;
    [SerializeField]
    bool isGrounded;
    public GameObject bullet;
    public GameObject bulSpawn;
    public Rigidbody2D _rigidbody;
    void Start(){
        //_rigidbody = GetComponent<Rigidbody2D>();
        jumpForce = 530.0f;
        speed = 3.0f;
        lives = 15;
        animator = GetComponent<Animation>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void FixedUpdate(){
        CheckGround();
    }
    void Update(){
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump")&& isGrounded) Jump();// 
        if (Input.GetButtonDown("Fire1")) Shoot();

        if(lives<=0){
            Destroy(this.gameObject);
        }
#region Name
    // if (pl.transform.localScale.x == 1)
        //     direction = new Vector3(1f, 0f, 0f);
        // else direction = new Vector3(-1f, 0f, 0f);
        // transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * -25); if (act == false)
        // {
        //     lDirection = direction;
        //     act = true;
        // }
        // transform.position = Vector3.MoveTowards(
                            // transform.position, 
                            // transform.position + lDirection, 
                            // speed * Time.deltaTime);
#endregion
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
        //sprite.flipX = direction.x < 0.0f;
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
    IEnumerator BulLife()
    {
        yield return new WaitForSeconds(4.0f); 
        Destroy(this.gameObject);
        StopCoroutine("BulLife");
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "EnemyBullet"){
            lives--;
        }
    }
}