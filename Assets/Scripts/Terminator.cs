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
    GameObject player;
    [SerializeField]
    int lives = 10;
    void Start()
    { 
        isAction = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dist=Vector3.Distance(player.transform.position, transform.position);
        if(dist<=23 && isAction==false){
            isAction=true;
            Shoot();
        }
        if(lives<=0){
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
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "PlayerBullet"){
            lives--;
        }
    }
}
