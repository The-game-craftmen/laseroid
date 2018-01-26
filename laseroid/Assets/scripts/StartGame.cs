using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class StartGame : MonoBehaviour {
    public Button hostBtn;
    public Button joinBtn;
    public bool isAtStartup = true;

    NetworkClient myClient;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Join()
    {
        Debug.Log("here");
        SceneManager.LoadScene("scenes/level1");
    }

    public void Host()
    {
        Debug.Log("Host Test");
        GameObject dataOverScene = GameObject.Find("/DataOverScene");
        if (dataOverScene != null)
        {
            Debug.Log("data over Scene found");
            DataOverScene dataOverSceneScript = dataOverScene.GetComponent<DataOverScene>();
            if (dataOverSceneScript != null)
            {
                Debug.Log("data over Scene Script found");
                dataOverSceneScript.SetIsHost(true);
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
