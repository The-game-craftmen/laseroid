using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public void UpdateHealthBar(float _hp)
    {
        Slider healthBar = (Slider)GetComponent<Slider>();
        healthBar.value = _hp;
    }
}
