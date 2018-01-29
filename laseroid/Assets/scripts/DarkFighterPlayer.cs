using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class DarkFighterPlayer : NetworkBehaviour
{
    private float torqueStep = 80;
    private float speed = 0;
    private float speedMax = 30;

    private float stepAcceleration = 25;
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    [Command]
    void CmdApplyTorqueUp(float torque)
    {
        Debug.Log("TorqueZZZ");
        rb.AddTorque(transform.right * torque, ForceMode.Acceleration);
    }

    [Command]
    void CmdApplyTorqueLeft(float torque)
    {
        rb.AddTorque(transform.up * torque, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Z) == true)
        {
            Debug.Log("TorqueWZ" + isServer);
        }
        if (!isServer)
        {
            if (Input.GetKey(KeyCode.Z) == true)
            {
                Debug.Log("TorqueZ");
                CmdApplyTorqueUp(torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S) == true)
            {
                CmdApplyTorqueUp(-torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) == true)
            {
                CmdApplyTorqueLeft(-torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Q) == true)
            {
                CmdApplyTorqueLeft(torqueStep * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Escape) == true)
            {
                Application.Quit();
            }
        }
    }
}
