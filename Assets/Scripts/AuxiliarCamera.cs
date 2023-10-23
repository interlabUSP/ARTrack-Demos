using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliarCamera : MonoBehaviour
{
    public GameObject camera;
    public GameObject plano;

    // Update is called once per frame
    void Update()
    {
        plano.transform.position = new Vector3(plano.transform.position.x,plano.transform.position.y,camera.transform.position.z +(-4.58f+8.74f)) ;
    }
}
