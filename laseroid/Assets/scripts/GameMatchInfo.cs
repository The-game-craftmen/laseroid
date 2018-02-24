using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class GameMatchInfo : NetworkBehaviour {
    public struct PlayerInfo
    {
        public int id;
        public string name;
        public int score;
    };
    // Use this for initialization
    public class ListOfPlayer : SyncListStruct<PlayerInfo> { }
    [SerializeField]
    ListOfPlayer listPlayer = new ListOfPlayer();

    public ListOfPlayer GetListOfPlayer() { return listPlayer; }

    void Start()
    {
        GameObject DataOver = GameObject.Find("DataOverScene");
        if (DataOver)
        {
            DataOverScene dos = DataOver.GetComponent<DataOverScene>();
            if (dos)
            {

                CmdAddPlayer(ClientScene.readyConnection.connectionId, dos.GetNickname());
            }
        }
        listPlayer.Callback = BufChanged;
    }

    void BufChanged(SyncListStruct<PlayerInfo>.Operation op, int itemIndex)
    {
        Debug.Log("buf changed:" + op);
    }

    

    [Command]
    public void CmdAddPlayer(int _id,string _name)
    {
        PlayerInfo temp = new PlayerInfo();
        temp.id = _id;
        temp.name = _name;
        temp.score = 0;
        listPlayer.Add(temp);
        Debug.Log("CmdAddPlayer " + _name);
    }

    public void RemovePlayer(int _id)
    {
        IEnumerator<PlayerInfo> enumerator = listPlayer.GetEnumerator();
        PlayerInfo foundPlayer = new PlayerInfo();
        bool found = false;
        while (enumerator.MoveNext() && !found )
        {
            PlayerInfo temp = enumerator.Current;
            if (temp.id == _id)
            {
                found = true;
                foundPlayer = temp;
            }
        }

        if (found)
        {
            listPlayer.Remove(foundPlayer);
        }
    }
}
