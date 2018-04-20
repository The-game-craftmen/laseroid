using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float tumble = 1.0f;

    void Start()
    {
        //GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble * 10;
        GetComponent<Rigidbody>().AddTorque((Random.Range(0, 2) - 0.5f) * Vector3.up * tumble * 5, ForceMode.VelocityChange);
    }
}