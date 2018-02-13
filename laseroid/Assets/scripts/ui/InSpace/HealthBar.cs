using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public void UpdateSpeedBar(float _hp)
    {
        Slider healthBar = (Slider)GetComponent<Slider>();
        healthBar.value = _hp;
    }
}
