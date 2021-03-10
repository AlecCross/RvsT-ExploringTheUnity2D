using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBullet : MonoBehaviour
{
    float speed = 8f;
    Vector3 direction;
    Vector3 lDirection;
    GameObject enemy;
    bool act = false;
    public float enemyLocalScale1;
    public float enemyLocalScale;
    void Start(){
        gameObject.tag = "EnemyBullet";
        enemy = GameObject.Find("StandShootEnemy");
        enemyLocalScale1 = enemy.transform.localScale.x;
        StartCoroutine(BulLife());
    }
    void Update(){
        if (enemy != null)
        {
            if (enemyLocalScale1 == enemyLocalScale)
            {
                direction = new Vector3(1f, 0f, 0f);
                //print("EnemyLocation in Bullet" + direction.ToString());
            }
            else
                direction = new Vector3(-1f, 0f, 0f);
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * -25);
            if (act == false)
            {
                lDirection = direction;
                act = true;
            }
            transform.position = Vector3.MoveTowards(transform.position,
                                                transform.position - lDirection,
                                                speed * Time.deltaTime);
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position,
                                                transform.position - lDirection,
                                                speed * Time.deltaTime);
        }
    }
    IEnumerator BulLife(){
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
        StopCoroutine("BulLife");
    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "Player") { }
        //print("destroy Enemy Bullet");
        Destroy(this.gameObject);
    }
}
