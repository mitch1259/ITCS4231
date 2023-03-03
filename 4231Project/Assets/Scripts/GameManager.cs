using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerManager player;
    public GameObject[] worldBlock;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            worldBlock = Resources.LoadAll<GameObject>("WorldBlocks");

            Debug.Log("World Blocks Found:");
            for (int i = 0; i < worldBlock.Length; i++)
            {
                Debug.Log(worldBlock[i]);
            }

            Instantiate(worldBlock[0], new Vector3(0, 0, 19.5f), Quaternion.identity);
        } else {
            Destroy(gameObject);
        }
    }
}
