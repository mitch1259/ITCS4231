using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;
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

    void Start()
    {
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
        falling = false;
        win = false;

        playerTransform.position = new Vector3(0f, ground, 0f);
    }

    void Update()
    {
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
        }

        if (jumping) {
            gravity -= fall * Time.deltaTime;
            playerTransform.position += new Vector3(0f, gravity + jump * Time.deltaTime, 0f);

            if (safe) {
                jumping = false;
                playerTransform.position = new Vector3(playerTransform.position.x, ground, playerTransform.position.z);
            }
        }

        //Debug.Log(Time.deltaTime);

        if (falling && !jumping) {
            gravity -= fall * Time.deltaTime;
            playerTransform.position += new Vector3(0f, gravity, 0f);
        }

        if (playerTransform.position.y < ground - 1) {
            dead = true;
        }

        if (dead) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void FixedUpdate()
    {
        playerTransform.position += new Vector3(0f, 0f, speed * speedIncrease);
        cameraTransform.position = new Vector3(0f, 5f, playerTransform.position.z - 7f);
        cameraTransform.rotation = Quaternion.Euler(15f, 0f, 0f);

        passedTime += 1;
        if (passedTime >= 600) {
            passedTime = 0;
            speedIncrease += 0.001f;
            Debug.Log("Speed Increased");
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
