using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminator : MonoBehaviour
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
    public Animator animator;
    void Start(){ 
        isAction = false;
        player = GameObject.Find("Player");
        scoreCount = 100;
    }

    // Update is called once per frame
    void Update(){
        dist=Vector3.Distance(player.transform.position, transform.position);
        if(dist<=10 && isAction==false){
            isAction=true;
            Shoot();
        }
        if(lives<=0){
            StartCoroutine(ExplodesAnim());
            gameStats.AddScore(scoreCount);
            Destroy(this.gameObject);
        }
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
    IEnumerator ExplodesAnim()
    {
        animator.SetTrigger("Explodes");
        print("Запущена коротина взрыва");
        yield return new WaitForSeconds(1f);
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "PlayerBullet"){
            lives--;
        }
    }
}
