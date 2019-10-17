using UnityEngine;
using System.IO;
using System.Collections;

public class EarthMoonNotScaled : MonoBehaviour
{

    public Transform target2;
    public float orbitDistance2 = 10.0f;
    public float orbitDegreesPerSec2 = 0.00015f;



    void Orbit2()
    {
        if (target2 != null)
        {
            // Keep moon at orbitDistance from target
            transform.position = target2.position + (transform.position - target2.position).normalized * orbitDistance2;
            transform.RotateAround(target2.position, Vector3.up, orbitDegreesPerSec2 * Time.deltaTime);
        }
    }
    void LateUpdate()
    {

        Orbit2();

    }
}