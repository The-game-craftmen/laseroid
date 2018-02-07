using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class DarkFighterPlayer : NetworkBehaviour
{
    private float torqueStep = 100;
    private float speed = 0;
    private const float speedMax = 10;
    [SyncVar]
    private int hitpoint = 100;
    public GameObject bulletPrefab;
    private Vector3 reduceVector = new Vector3(0.8f, 0.8f, 0.8f);
    private const float stepAcceleration = 0.5f;
    private Rigidbody rb;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        if (isLocalPlayer) { 
                Camera camToPlayer = Camera.main;
                camToPlayer.transform.parent = this.transform;
                camToPlayer.transform.position = new Vector3(0, 0, -1);

        }
        Material mat = this.GetComponent<MeshRenderer>().material;

        //mat.EnableKeyword("_EMISSION");
        //mat.SetColor("_EmissionColor", Color.green);
        //mat.SetFloat("_EmissionMap", 10);
        //mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.
        //this.GetComponent<MeshRenderer>().material.color = Color.white * 10;
        ///this.GetComponent<MeshRenderer>().material.SetInt(_EmissionScaleUI)

    }

    public void SetDamage(int _hitpoint)
    {
        hitpoint -= _hitpoint;
        if (hitpoint < 0) hitpoint = 0;
    }

    [Command]
    private void CmdFire(Vector3 playerPosition, Vector3 playerForward, Quaternion playerRotation)
    {
        Vector3 location = playerPosition + playerForward * 2;
        var bullet = (GameObject)Instantiate(bulletPrefab);
        NetworkServer.Spawn(bullet);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        
        rbBullet.transform.position = location;
        rbBullet.transform.rotation = playerRotation;
        rbBullet.AddForce(rbBullet.transform.forward * 1500, ForceMode.Acceleration);

        Destroy(bullet, 20.0f);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }

    public void DoDamage(int _damage)
    {
        if (isServer)
        {
            this.hitpoint -= _damage;
            if (hitpoint <= 0)
            {
                GameObject explosion = Resources.Load("LoudExplosion") as GameObject;
                GameObject expl = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
                NetworkServer.Spawn(expl);
                RpcRespawn();
            }
        }
        
    }


    void UpdateSpeed(float speedDelta)
    {
        if (Mathf.Abs (speed + speedDelta) < speedMax && (speed + speedDelta)>=0)
        {
            speed += speedDelta;
        }
    }

    void ManageKeyboard()
    {
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
            UpdateSpeed(stepAcceleration);
        }
        else if (Input.GetKey(KeyCode.W) == true)
        {
            UpdateSpeed(-stepAcceleration);
        }
        else if (Input.GetKey(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire(rb.transform.position, rb.transform.forward, rb.transform.rotation);
        }
    }


    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        ManageKeyboard();
        
        rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);
        rb.velocity.Scale(reduceVector);
        
    }
}
