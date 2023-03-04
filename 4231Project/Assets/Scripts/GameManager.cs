using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] worldBlock;
    public bool done;
    public float spawn;
    public int exits;
    public ArrayList world;
    public int block;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            worldBlock = Resources.LoadAll<GameObject>("WorldBlocks");
            done = false;
            spawn = 55.5f;
            exits = 0;
            world = new ArrayList();
            block = 0;

            while (done == false) {
                int rand = Random.Range(0, worldBlock.Length);

                if (worldBlock[rand].name == "Exit") {
                    exits++;
                    if (exits == 5) {
                        done = true;
                        world.Add(rand);
                    }
                } else {
                    world.Add(rand);
                } 
                Debug.Log(rand);
            }
        } else {
            Destroy(gameObject);
        }
    }
}
