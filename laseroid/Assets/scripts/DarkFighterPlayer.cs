using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

[NetworkSettings(sendInterval = 0.04f)]
public class DarkFighterPlayer : NetworkBehaviour
{
    private float torqueStep = 50;
    [SyncVar]
    private float speed = 0;
    private const float speedMax = 10;
    [SyncVar(hook = "OnChangeHitPoints")]
    private int hitpoint = 100;
    private int hitpointMax = 100;
    public GameObject bulletPrefab;
    private Vector3 reduceVector = new Vector3(0.8f, 0.8f, 0.8f);
    private const float stepAcceleration = 0.5f;
    private Rigidbody rb;
    private GameState gameStateScript;
    private GameObject speedBar;
    private GameObject healthBar;
    private GameObject floatingNameText = null;
    protected GameObject targetUi = null;
    private GameObject PlayerScore = null;
    private int doubledashRight = 0;
    private int doubledashLeft = 0;
    private float strokeLeftTimer = 0.0f;
    private float strokeRightTimer = 0.0f;
    [SyncVar]
    private int score = 0;
    private float ticksExitMenu ;
    [SyncVar]
    private string nickname;

    public int GetScore()
    {
        return score;
    }

    public GameObject GetPlayerScoreUI()
    {
        return PlayerScore;
    }
    public void SetPlayerScoreUI(GameObject _pls)
    {
        PlayerScore = _pls;
    }

    public GameObject getFloatingNameText()
    {
        return this.floatingNameText;
    }

    public void setFloatingNameText(GameObject flText)
    {
        this.floatingNameText = flText;
    }

    [Command]
    void CmdSetNickname(string _name)
    {
        nickname = _name;
    }

    void Start()
    {
        NetworkServer.SpawnObjects();
        rb = GetComponent<Rigidbody>();
        Debug.Log(isLocalPlayer);
        Debug.Log(isClient);
        if (isLocalPlayer) {
            Camera camToPlayer = Camera.main;
            camToPlayer.transform.parent = this.transform;
            camToPlayer.transform.position = new Vector3(0, 0, 0);

            speedBar = GameObject.Find("Canvas").transform.Find("SpeedBar").gameObject;
            healthBar = GameObject.Find("Canvas").transform.Find("HealthBar").gameObject;
            hitpoint = hitpointMax;

            GameObject gm = GameObject.Find("GameState");
            if (gm)
            {
                gameStateScript = gm.GetComponent<GameState>();
            }
            GameObject data = GameObject.Find("DataOverScene");
            if (data)
            {
                DataOverScene dos = data.GetComponent<DataOverScene>();
                if (dos)
                {
                    //nickname = dos.GetNickname();
                    CmdSetNickname(dos.GetNickname());
                    Debug.Log("NICKNAME " + nickname);
                }
            }
        }
        
        //mat.EnableKeyword("_EMISSION");
        //mat.SetColor("_EmissionColor", Color.green);
        //mat.SetFloat("_EmissionMap", 10);
        //mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.
        //this.GetComponent<MeshRenderer>().material.color = Color.white * 10;
        ///this.GetComponent<MeshRenderer>().material.SetInt(_EmissionScaleUI)

    }

    public void SetTargetUi(GameObject _targetUI)
    {
        this.targetUi = _targetUI;
    }

    public GameObject getTargetUi()
    {
        return targetUi;
    }

    public float getPrcentHull()
    {
        return (float)hitpoint / (float)hitpointMax;
    }

    void OnGUI()
    {
        if (isLocalPlayer) { 
            GameObject[] listOfShips = GameObject.FindGameObjectsWithTag("ship");
            GameObject canvas = GameObject.Find("Canvas");
            for (int itShip = 0; itShip < listOfShips.Length; itShip++)
            {
                DarkFighterPlayer shipScript = listOfShips[itShip].GetComponent<DarkFighterPlayer>();
                if (shipScript != null)
                {
                    if (shipScript.netId != this.netId) { 
                        if (shipScript.getFloatingNameText() == null)
                        {
                            GameObject floatingText = Resources.Load("ui/PanelShip") as GameObject;
                            GameObject targetUI = Resources.Load("ui/TargetImage") as GameObject;
                            GameObject floatingTextInstance = Instantiate(floatingText) as GameObject;
                            GameObject targetUIInstance = Instantiate(targetUI) as GameObject;
                            shipScript.setFloatingNameText(floatingTextInstance);
                            shipScript.SetTargetUi(targetUIInstance);
                            floatingTextInstance.transform.SetParent(canvas.transform);
                            targetUIInstance.transform.SetParent(canvas.transform);
                        }
                        else
                        {
                            Camera camera = GetComponent<Camera>();

                            Vector3 screenPos = Camera.main.WorldToScreenPoint(listOfShips[itShip].transform.position);
                            GameObject floatingNameText = shipScript.getFloatingNameText();
                            GameObject targetUI = shipScript.getTargetUi();
                            if (screenPos.z > 0 && screenPos.y > 0 && screenPos.x > 0)
                            {
                                targetUI.SetActive(true);
                                targetUI.transform.position = screenPos;
                                screenPos.x -= listOfShips[itShip].name.Length;
                                screenPos.y += 40;
                                float distance = CalcDistance(listOfShips[itShip]);
                                floatingNameText.SetActive(true);
                                floatingNameText.transform.position = screenPos;
                                GameObject distanceObj = floatingNameText.transform.Find("distance").gameObject;

                                PanelFollowingScript pf = floatingNameText.GetComponent<PanelFollowingScript>();
                                if (pf)
                                {
                                    pf.updateUi(shipScript.GetNickName(), shipScript.getPrcentHull(), distance);
                                }

                            }
                            else
                            {
                                floatingNameText.SetActive(false);
                                targetUI.SetActive(false);
                            }
                        }
                    }

                }
            }
            /*GameObject listPLayerGui = GameObject.Find("Canvas").transform.Find("listPLayer").gameObject;
            if (listPLayerGui)
            {
                InputField lp = listPLayerGui.GetComponent<InputField>();
                lp.text = pls;
            }*/
        }
    }

    private float CalcDistance(GameObject target)
    {
        float value = 0;

        Vector3 posTarget = target.transform.position;
        float dx = posTarget.x - transform.position.x;
        float dy = posTarget.y - transform.position.y;
        float dz = posTarget.z - transform.position.z;

        value = (float)Mathf.Round(Mathf.Sqrt(dx * dx + dy * dy + dz * dz));

        return value;
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
            transform.position = Vector3.zero;
            hitpoint = hitpointMax;
        }
    }

    [Command]
    private void CmdFire(NetworkInstanceId _id,Vector3 playerPosition, Vector3 playerForward, Quaternion playerRotation)
    {
        Vector3 location = playerPosition + playerForward * 2;
        var bullet = (GameObject)Instantiate(bulletPrefab);
        BulletScript bs = bullet.GetComponent<BulletScript>();
        bs.SetShipId(_id);
        NetworkServer.Spawn(bullet);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.transform.position = location;
        rbBullet.transform.rotation = playerRotation;
        rbBullet.AddForce(rbBullet.transform.forward * 1500, ForceMode.Acceleration);

        Destroy(bullet, 20.0f);
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

    [Command]
    void CmdUpdateSpeed(float _speedDelta)
    {
        if (Mathf.Abs(speed + _speedDelta) <= speedMax && (speed + _speedDelta) >= 0)
        {
            speed += _speedDelta;
        }
        else
        {
            if (Mathf.Abs(speed + _speedDelta) > speedMax)
            {
                speed = speedMax;
            }
            else
            {
                speed = 0;
            }
        }
    }

    void ManageKeyboard()
    {
        if (gameStateScript)
        {
            if (gameStateScript.GetState() == GameState.C_STATE_INGAME)
            {
                ManageKeyboardInGame();
            }
            else if (gameStateScript.GetState() == GameState.C_STATE_EXITMENU && (Time.time - ticksExitMenu) > 1) 
            {
                Debug.Log("EXITMENU GAME STATE");
                if (Input.GetKey(KeyCode.Escape) == true)
                {
                    GameObject exitPanel = GameObject.Find("Canvas").transform.Find("PanelExit").gameObject;
                    if (exitPanel)
                    {
                        exitPanel.SetActive(false);
                    }
                    if (gameStateScript)
                    {
                        gameStateScript.SetState(GameState.C_STATE_INGAME);
                        ticksExitMenu = Time.time;
                    }
                }
            }
        }
    }

    [Command]
    void CmdManageTorque(Vector3 torque)
    {
        rb.AddTorque(torque, ForceMode.Acceleration);
    }

    void ManageKeyboardInGame()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (this.doubledashLeft == 1) { 
                this.strokeLeftTimer = Time.time;
            }else
            {
                doubledashLeft = 0;
            }

        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (this.doubledashRight == 1)
            {
                this.strokeRightTimer = Time.time;
            }
            else
            {
                doubledashRight = 0;
            }
        }
        if (Input.GetKey(KeyCode.Z) == true)
        {
            CmdManageTorque(transform.right * torqueStep * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) == true)
        {
            CmdManageTorque(-transform.right * torqueStep * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            if (doubledashRight == 1 && (Time.time - strokeRightTimer) < 0.5f)
            {
                doubledashRight = 2;
            }
            else if (doubledashRight == 0)
            {
                doubledashRight = 1;
            }
            if (doubledashRight == 2)
            {
                CmdManageTorque(transform.up * torqueStep * Time.deltaTime * 3);
            }
            else
            {
                CmdManageTorque(transform.up * torqueStep * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.Q) == true)
        {
            if (doubledashLeft == 1 && (Time.time-strokeLeftTimer ) < 0.5f)
            {
                doubledashLeft = 2;
            }else if(doubledashLeft == 0)
            {
                doubledashLeft = 1;
            }
            if (doubledashLeft == 2) { 
                CmdManageTorque(-transform.up * torqueStep * Time.deltaTime * 3);
            }else
            {
                CmdManageTorque(-transform.up * torqueStep * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.A) == true)
        {
            CmdUpdateSpeed(stepAcceleration);
        }
        else if (Input.GetKey(KeyCode.W) == true)
        {
            CmdUpdateSpeed(-stepAcceleration);
        }
        else if (Input.GetKey(KeyCode.Escape) == true)
        {
            if (Time.time - ticksExitMenu > 1)
            {
                GameObject exitPanel = GameObject.Find("Canvas").transform.Find("PanelExit").gameObject;
                if (exitPanel)
                {
                    exitPanel.SetActive(true);
                    if (gameStateScript)
                    {
                        gameStateScript.SetState(GameState.C_STATE_EXITMENU);
                    }
                    ticksExitMenu = Time.time;
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire(this.netId,rb.transform.position, rb.transform.forward, rb.transform.rotation);
        }
    }

    void UpdateUi()
    {
        if (speedBar)
        {
            SpeedBar speedbarScript = speedBar.GetComponent<SpeedBar>();
            if (speedbarScript)
            {
                speedbarScript.updateUi(speed);
            }
        }

    }

    void OnChangeHitPoints(int _hp)
    {
        if (healthBar) {
            HealthBar healthBarScript = healthBar.GetComponent<HealthBar>();
            if (healthBarScript)
            {
                healthBarScript.UpdateHealthBar((float)_hp / (float)hitpointMax);
            }
        }
    }


    // Update is called once per frame
    void Update () {
        if (isLocalPlayer)
        {
            ManageKeyboard();

            UpdateUi();
        }
        if(isServer) { 
            rb.AddForce(rb.transform.forward * speed, ForceMode.Acceleration);
            rb.velocity.Scale(reduceVector);
        }
        
    }

    public string GetNickName()
    {
        return nickname;
    }

    public int GetHitPoint()
    {
        return hitpoint;
    }

    public void IncScore()
    {
        score += 1;
    }
}

