using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    private int state = 1;
    public const int C_STATE_STARTMENU = 1;
    public const int C_STATE_INGAME = 2;
    public const int C_STATE_EXITMENU = 3;
	
    public void SetState(int _state)
    {
        state = _state;
    }

    public int GetState()
    {
        return state;
    }
	
}
