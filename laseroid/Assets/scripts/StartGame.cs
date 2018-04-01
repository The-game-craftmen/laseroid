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

    //NetworkClient myClient;

    void SetupUI()
    {
        RectTransform panel = GameObject.Find("Canvas").transform.Find("PanelListPlayer").gameObject.GetComponent<RectTransform>();
        panel.anchoredPosition = new Vector2(Screen.width/2- panel.rect.width * panel.localScale.x * 0.7f, Screen.height/2- panel.rect.height * panel.localScale.y * 0.7f);
        
    }

    // Use thTexts for initialization
     void Start () {
        dataOverSceneScript = dataOverScene.GetComponent<DataOverScene>();
        SetupUI();
     }

    public void Retry()
    {
        GameObject.Find("CanvasStart").transform.Find("PanelConnexion").gameObject.SetActive(true);
        GameObject.Find("CanvasStart").transform.Find("PanellNoConnection").gameObject.SetActive(false);
    }

     // Update is called once per frame
     void Update () {
         LaserNetworkManager lnm = networkmanager.GetComponent<LaserNetworkManager>();
         if (lnm && lnm.GetStatusConnection() )
         {
             GameObject canvasStart = GameObject.Find("CanvasStart");
            GameObject stateObject = GameObject.Find("GameState");
             if (canvasStart && stateObject)
             {
                canvasStart.SetActive(false);
                GameState gsScript = stateObject.GetComponent<GameState>();
                if (gsScript)
                {
                    gsScript.SetState(GameState.C_STATE_INGAME);
                }
             }
         }

     }

    void SetNickname()
    {
        dataOverSceneScript.SetNickname(nickname.text);
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
             }


         }

     }
}
