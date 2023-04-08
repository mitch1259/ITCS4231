using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WorldManager world;
    public PlayerManager player;
    public ButtonManager startButton;
    public GameObject[] start;
    public int difficulty;
    public int oldDifficulty;

    void Awake() {
        start = Resources.LoadAll<GameObject>("Start");
        difficulty = 0;
        oldDifficulty = -1;

        InitGame();
    }

    void InitGame() {
        world.GenerateWorld();
        player.ResetPlayer();
        player.Init();
    }

    void Update() {
        if (player.getDead() || player.getWin()) {
            player.setDead(false);
            player.setWin(false);
            player.setPlay(false);
            startButton.StartButton.gameObject.SetActive(true);
            InitGame();
        }

        //difficulty = slider.value or something like that
        if (difficulty != oldDifficulty) {
            Debug.Log("Difficulty Updated");

            oldDifficulty = difficulty;
        }

        if (startButton.startGame == true) {
            player.spawns.Add(Instantiate(start[0], new Vector3(0, 0, 19.5f), Quaternion.identity));
            player.setPlay(true);
            startButton.startGame = false;
            startButton.StartButton.gameObject.SetActive(false);
        }
    }
}