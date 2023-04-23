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
    public int highScore;
    public int currScore;

    void Awake() {
        start = Resources.LoadAll<GameObject>("Start");
        difficulty = 0;
        oldDifficulty = -1;
        highScore = -1;
        currScore = 0;

        InitGame();
    }

    void InitGame() {
        player.ResetPlayer();
        player.Init();
    }

    void Update() {
        if (player.dead) {

            if (player.win) {
                player.score += 10;
            }

            player.dead = false;
            player.win = false;
            player.playing = false;
            UI.StartButton.gameObject.SetActive(true);
            UI.Difficulty.gameObject.SetActive(true);
            UI.ExitButton.gameObject.SetActive(true);
            animator.SetBool("jumping", false);
            animator.SetBool("playing", false);
            currScore = 0;
            
            if (player.score > highScore) {
                highScore = player.score;
            }

            UI.score.SetText("High Score: " + highScore);
            Debug.Log("Score: " + highScore);
            InitGame();
        }

        difficulty = UI.diff;
        if (difficulty != oldDifficulty) {
            world.threshold = difficulty*4;
            oldDifficulty = difficulty;

            Debug.Log("Difficulty Updated: " + world.threshold);
        }

        if (UI.startGame) {
            world.GenerateWorld();
            player.playing = true;
            UI.startGame = false;
            UI.StartButton.gameObject.SetActive(false);
            UI.Difficulty.gameObject.SetActive(false);
            UI.score.gameObject.SetActive(true);
            UI.ExitButton.gameObject.SetActive(false);
            animator.SetBool("playing", true);
            UI.score.SetText("Score: " + currScore);
        }

        if (player.score > currScore) {
            currScore = player.score;
            UI.score.SetText("Score: " + currScore);
        }
    }
}