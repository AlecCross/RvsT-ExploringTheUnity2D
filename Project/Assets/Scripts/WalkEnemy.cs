using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    public float speed;
    Vector3 direction;
    Character player;
    int scoreCount = 50;
    int health = 5;
    int damage = 1;
    public GameStats gameStats;
    void Start()
    {
       speed = 3.0f;
       direction = new Vector3(-1f, 0f, 0f);
       player = GameObject.Find("Player").GetComponent<Character>();
    }
    void Update()
    {
        Run();
        if(health==0){
            gameStats.AddScore(scoreCount);
            Destroy(this.gameObject);
        }
    }

    IEnumerator RunInterval(){
        yield return new WaitForSeconds(2f);
        this.transform.localScale = new Vector3(-1f,1f,1f);
            direction = new Vector3(-1f,0f,0f);
        yield return new WaitForSeconds(2f);
        this.transform.localScale = new Vector3(1f,1f,1f);
            direction = new Vector3(1f,0f,0f);

    }

    public void Run(){
        transform.position = Vector3.MoveTowards(
                                        transform.position, 
                                        transform.position+direction, 
                                        speed*Time.deltaTime);
    }
    void OnTriggerEnter2d(Collider2D other){
        if(other.gameObject.name=="LevelLeftWall"){
            this.transform.localScale = new Vector3(-1f,1f,1f);
            direction = new Vector3(-1f,0f,0f);
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Player"){
            player.ReceivedDamage(damage);
        }
        if(collision.gameObject.tag == "PlayerBullet"){
            health--;
            print("walk terminator health-- "+ health);
        }
    }
}
