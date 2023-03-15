using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class TesteWEB : MonoBehaviour
{
    public Text Infos_debug;
    public void RecebeDados(string data){
        //
        Debug.Log(data);
        Infos_debug.text = data;
    }
}
