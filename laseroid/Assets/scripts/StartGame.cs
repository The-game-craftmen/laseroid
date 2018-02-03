using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class StartGame : MonoBehaviour {
    public bool isAtStartup = true;
    public InputField IP;
    public InputField nickname;

    NetworkClient myClient;
    // Use thTexts for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Join()
    {
    
        GameObject dataOverScene = GameObject.Find("/DataOverScene");
        if (dataOverScene != null)
        {
            DataOverScene dataOverSceneScript = dataOverScene.GetComponent<DataOverScene>();
            if (dataOverSceneScript != null)
            {
                dataOverSceneScript.SetIp(IP.text);
                dataOverSceneScript.SetNickname(nickname.text);
                SceneManager.LoadScene("scenes/level1");

            }
            else
            {
                Debug.Log("data over Scene Script not found");
            }
        }
        else
        {
            Debug.Log("data over Scene not found");
        }
        SceneManager.LoadScene("scenes/level1");
    }

    public void Host() { 
   
        GameObject dataOverScene = GameObject.Find("/DataOverScene");
        if (dataOverScene != null)
        {
            DataOverScene dataOverSceneScript = dataOverScene.GetComponent<DataOverScene>();
            if (dataOverSceneScript != null)
            {
                dataOverSceneScript.SetIsHost(true);
                dataOverSceneScript.SetNickname(nickname.text);
                SceneManager.LoadScene("scenes/level1");

            }
            else
            {
                Debug.Log("data over Scene Script not found");
            }
        }
        else
        {
            Debug.Log("data over Scene not found");
        }
    }


}
