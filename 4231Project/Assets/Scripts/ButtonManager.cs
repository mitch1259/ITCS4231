using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonManager : MonoBehaviour
{
    public Button StartButton;
    public Slider Difficulty;
    public Image handle;
    public Image fill;
    public bool startGame;
    public int diff;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(startGameFunc);
        startGame = false;
        Difficulty.onValueChanged.AddListener(difficultyUpdate);
        diff = 0;
    }

    void startGameFunc() {
        startGame = true;
    }

    void difficultyUpdate(float value) {
        diff = (int)Math.Round(value);
        //Debug.Log(diff);
        if(diff == 0){
            handle.color = Color.green;
            fill.color = Color.green;
        } else if (diff == 1) {
            handle.color = Color.yellow;
            fill.color = Color.yellow;
        } else if (diff == 2) {
            handle.color = Color.red;
            fill.color = Color.red;
        }
    }
}