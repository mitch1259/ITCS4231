using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button StartButton;
    public bool startGame;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(startGameFunc);
        startGame = false;
    }

    void startGameFunc() {
        startGame = true;
    }
}
