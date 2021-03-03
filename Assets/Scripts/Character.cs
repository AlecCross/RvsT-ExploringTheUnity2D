using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    int lives;
    [SerializeField]
    float jumpForce;
    new Rigidbody2D rigidbody;
    Animation animator;
    SpriteRenderer sprite;
    void Avake(){
        speed = 3.0f;
        lives = 5;
        jumpForce = 15.0f;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animation>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if(Input.GetButton("Horizontal")) Run();
        if(Input.GetButton("Jump")) Jump();
    }
    void Run(){
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        //sprite.flipX = direction.x < 0.0f;
    }

    void Jump(){
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}