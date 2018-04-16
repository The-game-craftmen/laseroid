using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelFollowingScript : MonoBehaviour {
    Text nameText;
    Text distanceText;
    Slider hullSlider;

	// Use this for initialization
	void Start () {
        nameText = this.transform.Find("name").gameObject.GetComponent<Text>();
        hullSlider = this.transform.Find("life").gameObject.GetComponent<Slider>();
        distanceText = this.transform.Find("distance").gameObject.GetComponent<Text>();
    }

    public void updateUi(string _name, float _prcentHull,  float _distance)
    {
        if(nameText == null)
        {
            nameText = this.transform.Find("name").gameObject.GetComponent<Text>();
            hullSlider = this.transform.Find("life").gameObject.GetComponent<Slider>();
            distanceText = this.transform.Find("distance").gameObject.GetComponent<Text>();
        }   
        nameText.text = _name;
        hullSlider.value = _prcentHull;
        distanceText.text = _distance.ToString();
    }	
}
