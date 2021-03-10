using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{ 
    public ScoreBar scoreBar;
    public int scoreCount;

    // Start is called before the first frame update
    void Start()
    {
         scoreCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreBar.SetScore(scoreCount);
    }
    public void AddScore(int points){
        scoreCount+=points;
    }
}
