using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Text scoreBar;
    public void SetScore(int score){

        scoreBar.text = score.ToString();
    }
    public void SetDefault(){
        scoreBar.text = "0000000";
    }
}
