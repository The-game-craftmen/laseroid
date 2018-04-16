using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShimUi : MonoBehaviour {

    void ShowScore()
    {
        GameObject[] listOfShips = GameObject.FindGameObjectsWithTag("Ship");
        GameObject canvas = GameObject.Find("Canvas");
        for (int itShip = 0; itShip < listOfShips.Length; itShip++)
        {
            DarkFighterPlayer shipScript = listOfShips[itShip].GetComponent<DarkFighterPlayer>();
            if (shipScript != null)
            {
                GameObject pls = shipScript.GetPlayerScoreUI();
                if (pls == null)
                {
                    GameObject pst = Resources.Load("ui/PlayerScoreText") as GameObject;
                    GameObject pstInstance = Instantiate(pst) as GameObject;
                    GameObject panel = GameObject.Find("Canvas").transform.Find("PanelListPlayer").gameObject;
                    if (panel)
                    {
                        pstInstance.transform.SetParent(panel.transform);
                        pstInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f, 100f - 130f * itShip);

                    }
                    shipScript.SetPlayerScoreUI(pstInstance);
                }
                else
                {
                    Text plsText = pls.GetComponent<Text>();
                    plsText.text = shipScript.GetNickName() + " : " + shipScript.GetScore();
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        ShowScore();	
	}
}
