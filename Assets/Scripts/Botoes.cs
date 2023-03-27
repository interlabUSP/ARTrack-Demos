using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botoes : MonoBehaviour
{
    bool mostrando = false;
    public GameObject info;
    public Sprite verde, vermelho;
    public Image marcado;
    public void MostraInfs(){
        mostrando = !mostrando;
        info.SetActive(mostrando);
    }

    public void Conectou(bool conect){
        if(conect){
            marcado.sprite = verde;
        } else{
            marcado.sprite = vermelho;
        }
    }
}
