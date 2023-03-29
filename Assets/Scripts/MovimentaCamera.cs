using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaCamera : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;
    [SerializeField]public float Min=0.16f;
    [SerializeField]public float Max=0.61f;

    private Vector3 previousPosition;

    void Update() //Vector3(-1.81427503,0.649423778,1.28633201)
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // frente
            {
                if (distanceToTarget > Max)//maximo de zoom
                {
                    distanceToTarget -= 0.1f;
                }

            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // tras
            {
                if (distanceToTarget < Min)//minimo de zoom
                {
                    distanceToTarget += 0.1f;
                }
            }

            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }


    }
}
