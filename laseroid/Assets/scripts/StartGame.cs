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
    private int frame = -1;

    NetworkClient myClient;

  
    // Use thTexts for initialization
     void Start () {
         dataOverSceneScript = dataOverScene.GetComponent<DataOverScene>();
     }

     // Update is called once per frame
     void Update () {
         LaserNetworkManager lnm = networkmanager.GetComponent<LaserNetworkManager>();
         if (lnm && lnm.GetStatusConnection() && frame == -1 )
         {
             frame = 0;

         }
         if (frame > 50)
         {
             GameObject canvasStart = GameObject.Find("CanvasStart");
             if (canvasStart)
             {
                 canvasStart.SetActive(false);
             }
         }
         else
         {
             if (lnm && lnm.GetStatusConnection()){ 
                 frame += 1;
             }
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
