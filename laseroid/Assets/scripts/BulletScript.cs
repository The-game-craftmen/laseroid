using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{
    private float timer;
    private int damage;
    // Use this for initialization
    void Start () {
        timer = 0;
        damage = 20;
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
    /*        
            if(collision.gameObject.tag == "ship")
            {
                
                DarkFighterPlayer shipControl = collision.gameObject.GetComponent<DarkFighterPlayer>();
                
                if (shipControl != null)
                {
                    Debug.Log("damage");
                    shipControl.SetDamage(damage);
                }else
                {
                    Debug.Log("not damage");
                }
            }
            */
            Debug.Log(collision.gameObject);
           
            GameObject explosion = Resources.Load("LightExplosion") as GameObject;
            GameObject expl = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            NetworkServer.Spawn(expl);
            Destroy(this.gameObject);
            
        }
    }
}
