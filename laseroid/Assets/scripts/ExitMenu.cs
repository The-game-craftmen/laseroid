using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ExitMenu : MonoBehaviour {

	public void CancelExit()
    {
        GameObject exitPanel = GameObject.Find("Canvas").transform.Find("PanelExit").gameObject;
        if (exitPanel)
        {
            exitPanel.SetActive(true);
            GameObject gm = GameObject.Find("GameState");
            if (gm)
            {
                GameState gameStateScript = gm.GetComponent<GameState>();
                if (gameStateScript)
                {
                    gameStateScript.SetState(GameState.C_STATE_EXITMENU);
                    exitPanel.SetActive(false);
                }
            }
        }
    }

    public void DoExit()
    {
        GameObject lnm = GameObject.Find("NetworkManager");
        if (lnm)
        {
            LaserNetworkManager lnmScript = lnm.GetComponent<LaserNetworkManager>();
            if (lnmScript)
            {
                lnmScript.LnmStopClient();
            }
        }
        Application.Quit();
    }
}
