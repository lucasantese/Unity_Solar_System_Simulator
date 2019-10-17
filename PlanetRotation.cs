using UnityEngine;
using System.Collections;

public class PlanetRotation : MonoBehaviour
{
    public float rotatation_time = 5.0f;
    private float angle;

    void Start()
    {

    }
    void Update()
    {
        transform.Rotate(0, (360 / (rotatation_time * 60 * 60)) * Time.deltaTime, 0, Space.Self);
    }
}

