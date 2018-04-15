using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{
    private float timer;
    private int damage;
    private NetworkInstanceId ShipId;
    // Use this for initialization
    void Start () {
        timer = 0;
        damage = 20;
	}

    public void SetShipId(NetworkInstanceId _id)
    {
        ShipId = _id;
    }

    public NetworkInstanceId GetShipId()
    {
        return ShipId;
    }
	
	// Update is called once per frame
	void Update () {
        if (timer > 10)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (isServer) {
            Debug.Log("Collision" + collision.gameObject.tag);
            if(collision.gameObject.tag == "Ship")
            {
                
                DarkFighterPlayer shipControl = collision.gameObject.GetComponent<DarkFighterPlayer>();
                
                if (shipControl != null)
                {
                    Debug.Log("damage" + this.netId);
                    if ((shipControl.GetHitPoint()- damage) <= 0)
                    {
                        GameObject[] listOfShips = GameObject.FindGameObjectsWithTag("Ship");
                        for (int itShip = 0; itShip < listOfShips.Length; itShip++)
                        {
                            DarkFighterPlayer shipScript = listOfShips[itShip].GetComponent<DarkFighterPlayer>();
                            if (shipScript != null && shipScript.netId == ShipId)
                            {
                                shipScript.IncScore();
                            }
                        }
                    }
                    shipControl.SetDamage(damage);
                    
                }else
                {
                    Debug.Log("not damage");
                }
            }
            else
            {
                Debug.Log("pwer");
            }
            
            Debug.Log(collision.gameObject);
           
            GameObject explosion = Resources.Load("LightExplosion") as GameObject;
            GameObject expl = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            NetworkServer.Spawn(expl);
            Destroy(this.gameObject);
            
        }
    }
}
