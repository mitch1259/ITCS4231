using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public Button StartButton;
    public Slider Difficulty;
    public Image handle;
    public Image fill;
    public TMP_Text score;
    public Button ExitButton;
    public Button Instruct;
    public TMP_Text Rules;
    public Transform cameraTransform;
    public Button BackToGame;
    public bool startGame;
    public bool teach;
    public int diff;
    public bool played;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(startGameFunc);
        ExitButton.onClick.AddListener(exitGameFunc);
        startGame = false;
        Difficulty.onValueChanged.AddListener(difficultyUpdate);
        diff = 0;
        score.gameObject.SetActive(false);
        Instruct.onClick.AddListener(Instructions);
        teach = false;
        BackToGame.onClick.AddListener(Instructions);
        played = false;
    }

    void Instructions() {
        teach = !teach;
        if (teach) {
            StartButton.gameObject.SetActive(false);
            ExitButton.gameObject.SetActive(false);
            Difficulty.gameObject.SetActive(false);
            Instruct.gameObject.SetActive(false);
            score.gameObject.SetActive(false);
            Rules.gameObject.SetActive(true);
            BackToGame.gameObject.SetActive(true);
            cameraTransform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        } else {
            StartButton.gameObject.SetActive(true);
            ExitButton.gameObject.SetActive(true);
            Difficulty.gameObject.SetActive(true);
            Instruct.gameObject.SetActive(true);
            Rules.gameObject.SetActive(false);
            BackToGame.gameObject.SetActive(false);
            cameraTransform.rotation = Quaternion.Euler(15f, 0f, 0f);

            if (played) {
                score.gameObject.SetActive(true);
            }
        }
    }

    void exitGameFunc() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void startGameFunc() {
        startGame = true;
        played = true;
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