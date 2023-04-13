using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody playerBody;
    public Transform cameraTransform;
    public WorldManager instance;
    public GameManager gameManager;
    public List<GameObject> spawns;
    private float speed;
    private float speedIncrease;
    private float ground;
    private float jump;
    private float fall;
    private float gravity;
    private bool jumping;
    private int passedTime;
    private bool safe;
    private bool dead;
    private bool falling;
    private bool win;
    private bool playing;
    public int score;

    public void Init() {
        spawns.Add(Instantiate(gameManager.start[1], new Vector3(0, 0, 0), Quaternion.identity));
        spawns.Add(Instantiate(gameManager.start[0], new Vector3(0, 0, 19.5f), Quaternion.identity));

        playerTransform.position = new Vector3(0f, ground + 0.5f, 0f);
        cameraTransform.position = new Vector3(0f, 5f, playerTransform.position.z - 7f);
        cameraTransform.rotation = Quaternion.Euler(15f, 0f, 0f);
        Debug.Log("Player reInit");
    }

    public void ResetPlayer() {
        speed = 5f;
        speedIncrease = 0.05f;
        ground = 1.25f;
        jump = 10f;
        fall = .035f;
        gravity = -0.01f;
        jumping = false;
        passedTime = 0;
        safe = false;
        dead = false;
        falling = true;
        win = false;
        score = 0;

        foreach(var spawn in spawns) {
            Destroy(spawn);
        }
        spawns.Clear();
        Debug.Log("Player reset");
    }

    public bool getWin() {
        return win;
    }

    public void setWin(bool value) {
        win = value;
    }

    public void setDead(bool value) {
        dead = value;
    }

    public bool getDead() {
        return dead;
    }

    public void setPlay(bool value) {
        playing = value;
    }

    void Update()
    {
    if (playing) {
        Vector3 movement = Vector3.zero;
        float x = playerTransform.position.x;
        x = Mathf.Clamp(x, -3.25f, 3.25f);

        if (Input.GetKey(KeyCode.A)) {
            playerTransform.position = new Vector3(x - (speed * Time.deltaTime), playerTransform.position.y, playerTransform.position.z);
        } else if (Input.GetKey(KeyCode.D)) {
            playerTransform.position = new Vector3(x + (speed * Time.deltaTime), playerTransform.position.y, playerTransform.position.z);
        }

        if (Input.GetKey(KeyCode.W) && jumping == false) {
            gravity = -0.01f;
            jumping = true;
            safe = false;

            //Code below is from office hours when we couldnt figure out framerate issues
            //playerTransform.position += Vector3.up * 0.5f;
            //playerBody.useGravity = true;
            //playerBody.velocity = new Vector3(0f, jump, 0f);
        }

        if (jumping) {
            //fall = 0.00035f;
            //jump = .25f;


            gravity -= fall * Time.deltaTime * 1.5f;
            playerTransform.position += new Vector3(0f, gravity + jump * Time.deltaTime * 1.55f, 0f);
            //playerTransform.position += new Vector3(0f, gravity + jump * 1.55f, 0f)

            //print(fall);
            //print(Time.deltaTime);
            //print(gravity);
            //print(jump);
            //print("");

            if (safe) {
                jumping = false;
                playerTransform.position = new Vector3(playerTransform.position.x, ground, playerTransform.position.z);
                //playerBody.useGravity = false;
            } //else {
                //playerBody.velocity += Vector3.down * 0.15f;
            //}
        }


        //Debug.Log(Time.deltaTime);

        if (falling && !jumping) {
            gravity -= fall * Time.deltaTime;
            playerTransform.position += new Vector3(0f, gravity, 0f);
        }

        if (playerTransform.position.y < ground - 0.5f) {
            dead = true;
        }

        playerBody.velocity = new Vector3(0f, 0f, 0f);
    }
    }

    void FixedUpdate()
    {
    if (playing) {
        playerTransform.position += new Vector3(0f, 0f, speed * speedIncrease);
        cameraTransform.position = new Vector3(0f, 5f, playerTransform.position.z - 7f);
        cameraTransform.rotation = Quaternion.Euler(15f, 0f, 0f);

        passedTime += 1;
        if (passedTime >= 600) {
            passedTime = 0;
            speedIncrease += 0.001f;
            score += 5;
            Debug.Log("Speed Increased");
        }
    }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<MeshRenderer>().material.name == "SafeGround (Instance)" || c.gameObject.GetComponent<MeshRenderer>().material.name == "Start (Instance)") {
            Debug.Log("Landed on Solid Ground");
            safe = true;
            falling = false;
        } else if (c.gameObject.GetComponent<MeshRenderer>().material.name == "Death (Instance)") {
            Debug.Log("Hit an obstacle");
            dead = true;
        } else if (c.gameObject.GetComponent<MeshRenderer>().material.name == "Breakout (Instance)") {
            win = true;
            Debug.Log("Player wins!");
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<MeshRenderer>().material.name == "NewSpawn (Instance)") {
            if (instance.worldBlock[(int)instance.world[instance.block]].name == "Exit") {
                spawns.Add(Instantiate(instance.worldBlock[(int)instance.world[instance.block]], new Vector3(0, 0, instance.spawn - 16.5f), Quaternion.identity));
            } else {
                spawns.Add(Instantiate(instance.worldBlock[(int)instance.world[instance.block]], new Vector3(0, 0, instance.spawn), Quaternion.identity));
                instance.spawn += 36f;
            }
            instance.block++;
            //Debug.Log(spawns[spawns.Count - 1].name);
            Destroy(spawns[spawns.Count - 3]);
        } else if (c.gameObject.GetComponent<MeshRenderer>().material.name == "Coin (Instance)") {
            score += 1;
            Destroy(c.gameObject);
        }
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.GetComponent<MeshRenderer>().material.name == "SafeGround (Instance)" || c.gameObject.GetComponent<MeshRenderer>().material.name == "Start (Instance)") {
            falling = false;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.GetComponent<MeshRenderer>().material.name == "SafeGround (Instance)" || c.gameObject.GetComponent<MeshRenderer>().material.name == "Start (Instance)") {
            Debug.Log("Left Solid Ground");
            falling = true;
        }
    }
}