using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliarCamera : MonoBehaviour
{
    public GameObject camera;
    public GameObject plano;
    public float diff =(-4.58f+8.74f);
    // Update is called once per frame
    void Update()
    {
        plano.transform.position = new Vector3(plano.transform.position.x,plano.transform.position.y,camera.transform.position.z +(diff)) ;
    }
}
