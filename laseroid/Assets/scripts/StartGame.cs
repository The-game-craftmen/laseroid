using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class StartGame : MonoBehaviour {
    public bool isAtStartup = true;
    public InputField IP;
    public InputField nickname;
    public GameObject networkmanager;
    public GameObject dataOverScene;
    private DataOverScene dataOverSceneScript;

    NetworkClient myClient;
    // Use thTexts for initialization
    void Start () {
        dataOverSceneScript = dataOverScene.GetComponent<DataOverScene>();
    }
	
	// Update is called once per frame
	void Update () {
        LaserNetworkManager lnm = networkmanager.GetComponent<LaserNetworkManager>();
        Debug.Log("update" + lnm.GetStatusConnection());
        if (lnm && lnm.GetStatusConnection())
        {
            SceneManager.LoadScene("scenes/level1");
        }

    }

    public void Join()
    {
        if (dataOverSceneScript != null)
        {
            dataOverSceneScript.SetNickname(nickname.text);
            LaserNetworkManager lnm = networkmanager.GetComponent<LaserNetworkManager>();
            if (lnm)
            {
                lnm.Join(IP.text);
                //SceneManager.LoadScene("scenes/level1");
            }

        }
         
    }

    public void Host() { 
        if (dataOverSceneScript != null)
        {
            dataOverSceneScript.SetNickname(nickname.text);
            LaserNetworkManager lnm = networkmanager.GetComponent<LaserNetworkManager>();
            if (lnm)
            {
                lnm.Host();
                //SceneManager.LoadScene("scenes/level1");
            }
            

        }
   
    }


}
