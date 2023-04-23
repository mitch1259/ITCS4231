using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WorldManager world;
    public PlayerManager player;
    public ButtonManager UI;
    public GameObject[] start;
    public Animator animator;
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

            if (player.getWin()) {
                player.score += 10;
            }

            player.setDead(false);
            player.setWin(false);
            player.setPlay(false);
            UI.StartButton.gameObject.SetActive(true);
            UI.Difficulty.gameObject.SetActive(true);
            UI.ExitButton.gameObject.SetActive(true);
            animator.SetBool("jumping", false);
            animator.SetBool("playing", false);
            
            if (player.score > score) {
                score = player.score;

                UI.score.SetText("High Score: " + score);
            }

            UI.score.gameObject.SetActive(true);
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
            UI.score.gameObject.SetActive(false);
            UI.ExitButton.gameObject.SetActive(false);
            animator.SetBool("playing", true);
        }
    }
}