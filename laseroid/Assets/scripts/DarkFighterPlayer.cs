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
    private int hitpointMax = 100;
    public GameObject bulletPrefab;
    private Vector3 reduceVector = new Vector3(0.8f, 0.8f, 0.8f);
    private const float stepAcceleration = 0.5f;
    private Rigidbody rb;
    private GameObject speedBar;
    private GameObject healthBar;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        if (isLocalPlayer) {
            Camera camToPlayer = Camera.main;
            camToPlayer.transform.parent = this.transform;
            camToPlayer.transform.position = new Vector3(0, 0, 0);

            speedBar = GameObject.Find("Canvas").transform.Find("SpeedBar").gameObject;
            healthBar = GameObject.Find("Canvas").transform.Find("HealthBar").gameObject;

        }
        
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
        //if (hitpoint < 0) hitpoint = 0;
        if (hitpoint <= 0)
        {
            GameObject explosion = Resources.Load("LoudExplosion") as GameObject;
            GameObject expl = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            NetworkServer.Spawn(expl);
            RpcRespawn();
        }
        Debug.Log("SetDamage " + hitpoint);
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
            hitpoint = hitpointMax;
        }
    }

   
    static bool ShouldEmissionBeEnabled(Color color)
    {
        return color.maxColorComponent > (0.1f / 255.0f);
    }


    void UpdateSpeed(float speedDelta)
    {
        if (Mathf.Abs (speed + speedDelta) <= speedMax && (speed + speedDelta)>=0)
        {
            speed += speedDelta;
        }
        else
        {
            if (Mathf.Abs(speed + speedDelta) > speedMax) {
                speed = speedMax;
            }
            else
            {
                speed = 0;
            }
        }

        /*
         * Trying to change emissive scale value to glow the engine
         *
         
        Renderer render = this.GetComponent<MeshRenderer>();
        Material mat = this.GetComponent<MeshRenderer>().material;
        bool shouldEmissionBeEnabled = ShouldEmissionBeEnabled(render.material.GetColor("_EmissionColor"));
        if (shouldEmissionBeEnabled)
        {
            render.material.EnableKeyword("_EMISSION");
        }
        else
        {
            render.material.DisableKeyword("_EMISSION");
        }


        //render.material.EnableKeyword("_EMISSION");
        //DynamicGI.SetEmissive(render, Color.white * 0.1f);
        //render.UpdateGIMaterials();

        //DynamicGI.UpdateEnvironment();
        mat.SetColor("_EmissionColor", Color.white * 0.1f);
        */
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

    void updateUi()
    {
        if (speedBar)
        {
            SpeedBar speedbarScript = speedBar.GetComponent<SpeedBar>();
            if (speedbarScript)
            {
                speedbarScript.updateUi(speed);
            }
        }

        if (healthBar)
        {
            HealthBar healthBarScript = healthBar.GetComponent<HealthBar>();
            if (healthBarScript)
            {
                healthBarScript.UpdateSpeedBar((float) hitpoint / (float)hitpointMax );
            }
        }
    }


    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        ManageKeyboard();

        updateUi();
        rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);
        rb.velocity.Scale(reduceVector);
        
    }
}
