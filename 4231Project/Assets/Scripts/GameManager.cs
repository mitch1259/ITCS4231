using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WorldManager world;
    public PlayerManager player;
    public ButtonManager UI;
    public GameObject[] start;
    public int difficulty;
    public int oldDifficulty;
    public int score;

    void Awake() {
        start = Resources.LoadAll<GameObject>("Start");
        difficulty = 0;
        oldDifficulty = -1;
        score = -1;

        InitGame();
    }

    void InitGame() {
        player.ResetPlayer();
        player.Init();
    }

    void Update() {
        if (player.getDead() || player.getWin()) {
            player.setDead(false);
            player.setWin(false);
            player.setPlay(false);
            UI.StartButton.gameObject.SetActive(true);
            UI.Difficulty.gameObject.SetActive(true);
            score = player.score;
            Debug.Log("Score: " + score);
            InitGame();
        }

        difficulty = UI.diff;
        if (difficulty != oldDifficulty) {
            world.threshold = difficulty*4;
            oldDifficulty = difficulty;

            Debug.Log("Difficulty Updated: " + world.threshold);
        }

        if (UI.startGame == true) {
            world.GenerateWorld();
            player.setPlay(true);
            UI.startGame = false;
            UI.StartButton.gameObject.SetActive(false);
            UI.Difficulty.gameObject.SetActive(false);
        }
    }
}