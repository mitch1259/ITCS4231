using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WorldManager world;
    public PlayerManager player;

    void Update() {
        if (player.getDead() || player.getWin()) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}