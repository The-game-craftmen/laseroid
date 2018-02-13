using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedBar : MonoBehaviour {

    public void updateUi(float _speed)
    {
        Slider speedBar = (Slider)GetComponent<Slider>();
        speedBar.value = _speed / 10;
    }
	
}
