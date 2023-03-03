using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerManager player;
    public GameObject[] worldBlock;
    public bool done;
    public float spawn;
    public int exits;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            worldBlock = Resources.LoadAll<GameObject>("WorldBlocks");
            done = false;
            spawn = 55.5f;
            exits = 0;

            Debug.Log("World Blocks Found:");
            for (int i = 0; i < worldBlock.Length; i++)
            {
                Debug.Log(worldBlock[i]);
            }

            while (done == false) {
                int rand = Random.Range(0, worldBlock.Length);

                if (worldBlock[rand].name == "Exit") {
                    exits++;
                    if (exits == 5) {
                        done = true;
                        Instantiate(worldBlock[rand], new Vector3(0, 0, spawn - 16.5f), Quaternion.identity);
                    }
                } else {
                    Instantiate(worldBlock[rand], new Vector3(0, 0, spawn), Quaternion.identity);
                    spawn += 36f;
                }
                
            }
        } else {
            Destroy(gameObject);
        }
    }
}
