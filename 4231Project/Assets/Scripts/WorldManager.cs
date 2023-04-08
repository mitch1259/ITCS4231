using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject[] worldBlock;
    public GameObject[] start;
    public bool done;
    public float spawn;
    public int exits;
    public ArrayList world;
    public int block;
    public int threshold;

    void Start() {
        threshold = 0;
    }

    public void GenerateWorld() {
        worldBlock = Resources.LoadAll<GameObject>("WorldBlocks");
        done = false;
        spawn = 55.5f;
        exits = 0;
        world = new ArrayList();
        block = 0;
        //threshold = 5;

        while (done == false) {
            int rand = Random.Range(0, worldBlock.Length);

            if (worldBlock[rand].name == "Exit") {
                exits++;
                if (exits >= threshold) {
                    done = true;
                    world.Add(rand);
                }
            } else {
                world.Add(rand);
            } 
            //Debug.Log(rand);
        }
    }
}