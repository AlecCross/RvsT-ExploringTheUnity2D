using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 10f;
    Vector3 direction;
    Vector3 lDirection;
    GameObject player;
    bool act = false;
    public float playerLocalScale;
    void Start()
    {
        gameObject.tag = "PlayerBullet";
        player = GameObject.Find("Player");
        StartCoroutine(BulLife());
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.localScale.x==playerLocalScale)
            direction = new Vector3(1f, 0f, 0f);
        else    
            direction = new Vector3(-1f, 0f, 0f);
            transform.Rotate(new Vector3(0, 0, 45)  * Time.deltaTime*-25);
            if(act==false){
                lDirection = direction;
                act=true;
            }
            transform.position = Vector3.MoveTowards(transform.position, 
                                                transform.position+lDirection,
                                                speed * Time.deltaTime);                

    }
    IEnumerator BulLife(){
        //print("StartCoroutine BulLife");
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
        StopCoroutine("BulLife");
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "StandShootEnemy"){}
        print("destroy");
        Destroy(this.gameObject);
    }
}
