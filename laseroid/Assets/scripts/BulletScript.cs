using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{
    private float timer;
    private int damage;
    private NetworkInstanceId ShipId;

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
    
    void OnCollisionEnter(Collision _collision)
    {
        if (isServer) {
            if(_collision.gameObject.tag == "Ship")
            {
                DarkFighterPlayer shipControl = _collision.gameObject.GetComponent<DarkFighterPlayer>();                
                if (shipControl != null)
                {
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
                }
            }
            
            GameObject explosion = Resources.Load("LightExplosion") as GameObject;
            GameObject expl = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            NetworkServer.Spawn(expl);
            Destroy(this.gameObject);
            
        }
    }
}
