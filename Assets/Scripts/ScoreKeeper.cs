using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    // Script Selection
    [SerializeField] bool score;

    // Scripts and GameObjects
    Health healthScript;

    // Score
    public int scoreValue = 0;
    public Text scoreText;
    
    
    // Wave

    private void Start()
    {
        healthScript = FindObjectOfType<Health>();
    }
    void Update()
    {

    }

    public void Score(int value)
    {
        scoreValue += value;
        scoreText.text = "Score: " + scoreValue.ToString();
    }



}
