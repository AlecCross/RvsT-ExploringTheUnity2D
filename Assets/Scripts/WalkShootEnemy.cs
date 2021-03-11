using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkShootEnemy : MonoBehaviour
{
    [SerializeField]
    float dist;
    [SerializeField]
    bool isAction = false;
    public GameObject bullet;
    public GameObject bulSpawn;
    public float enemyLocalScale;
    GameObject player;
    [SerializeField]
    int lives = 10;
    //public ScoreBar scoreBar;
    public GameStats gameStats;
    [SerializeField]
    int scoreCount;
    Vector3 direction;
    public float speed;
    void Start(){ 
        isAction = false;
        player = GameObject.Find("Player");
        scoreCount = 100;
        speed = 3.0f;
        direction = new Vector3(-1f, 0f, 0f);
    }

    // Update is called once per frame
    void Update(){
        dist=Vector3.Distance(player.transform.position, transform.position);
        if(dist<=10 && isAction==false){
            isAction=true;
            Shoot();
        }
        if(lives<=0){
            gameStats.AddScore(scoreCount);
            Destroy(this.gameObject);
        }
        Run();
    }
    public void Run(){
        transform.position = Vector3.MoveTowards(
                                        transform.position, 
                                        transform.position+direction, 
                                        speed*Time.deltaTime);
    }
    void Shoot(){
        Vector3 position = bulSpawn.transform.position;
        Instantiate(bullet, position, bullet.transform.rotation);
        StartCoroutine(Charge());
    }
    IEnumerator Charge(){
        yield return new WaitForSeconds(1.0f);
        isAction=false;
        StopCoroutine(Charge());
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "PlayerBullet"){
            lives--;
        }
    }
}
