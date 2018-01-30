using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class DarkFighterPlayer : NetworkBehaviour
{
    private float torqueStep = 80;
    private float speed = 0;
    private const float speedMax = 30;

    private const float stepAcceleration = 25;
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        if (isLocalPlayer) { 
                Camera camToPlayer = Camera.main;
                camToPlayer.transform.parent = this.transform;
                camToPlayer.transform.position = new Vector3(0, 0, -1);

        }
    }

    void UpdateSpeed(float speedDelta)
    {
        if ((speed + speedDelta) > speedMax)
        {
            speed = speedMax;
        }
        else
        {
            if ((speed + speedDelta) < 0)
            {
                speed = 0;
            }
            else
            {
                speed += speedDelta;
            }
        }
    }


    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Z) == true)
        {       
            rb.AddTorque(transform.right * torqueStep * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.S) == true)
        {
            rb.AddTorque(-transform.right * torqueStep * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            rb.AddTorque(transform.up * torqueStep * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.Q) == true)
        {
            rb.AddTorque(-transform.up * torqueStep * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.A) == true)
        {
            UpdateSpeed(Time.deltaTime * stepAcceleration);
        }
        else if (Input.GetKey(KeyCode.W) == true)
        {
            UpdateSpeed(Time.deltaTime * -stepAcceleration);
        }
        else if (Input.GetKey(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
        else
        {
            rb.AddForce(rb.transform.forward * -speed, ForceMode.Acceleration);
        }
    }
}
