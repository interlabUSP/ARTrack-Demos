using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RaycastPins : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject oldPin;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * 100f, Color.red);
        if (Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit hit, 100f))
        {
            //Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<MudaCor>().olhando = true;
            //hit.collider.gameObject.GetComponent<MudaCor>().AumentaCor();
            oldPin = hit.collider.gameObject;
        }
        else if (oldPin !=null)
        {
            oldPin.GetComponent<MudaCor>().olhando = false;
            GameObject.Find("InfoPin").GetComponent<Text>().text = "";
        }
    }
}
