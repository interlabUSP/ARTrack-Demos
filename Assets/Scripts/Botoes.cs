using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botoes : MonoBehaviour
{
    static public bool Aviso=false;
    bool mostrando = false;
    public GameObject info;
    public Sprite verde, vermelho;
    public Image marcado;

    public GameObject telaNormal;
    public GameObject telaAjuda;
    public GameObject telaAviso;
    int Ajuda = 1;


    void Start() {
        try{
        if(!Aviso){
            telaAviso.SetActive(true);
            Aviso=true;
        }else{
            telaAviso.SetActive(false);
        }
        } catch  (System.Exception e){

        }
    }

    public void FechaAviso(){
        telaAviso.SetActive(false);
    }

    public void MostraInfs()
    {
        mostrando = !mostrando;
        info.SetActive(mostrando);
    }

    public void Conectou(bool conect)
    {
        if (conect)
        {
            marcado.sprite = verde;
        }
        else
        {
            marcado.sprite = vermelho;
        }
    }

    public void TrocaCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void AbrirSite(string site)
    {
        Application.OpenURL(site);
    }

    public void TrocaAjuda(bool mais)
    {
        int ajAnterior = Ajuda;

        if (mais && Ajuda < 4)
        {
            Ajuda++;
        }
        else if (!mais && Ajuda > 1)
        {
            Ajuda--;
        }

        TrocaFoto(ajAnterior);

    }

    void TrocaFoto(int ant)
    {
        //Debug.Log(Ajuda);
        GameObject.Find("a" + ant.ToString()).SetActive(false);//tira os anteriores
        GameObject.Find("b" + ant.ToString()).GetComponent<Image>().color = Color.white;//tira os anteriores


        GameObject.Find("b" + Ajuda.ToString()).GetComponent<Image>().color = Color.gray; ;
        FindInActiveObjectByName("a" + Ajuda.ToString()).SetActive(true);//Aparece os novos
        if (ant == 1)
        {
            GameObject.Find("link1_1").SetActive(false);
            GameObject.Find("link1").SetActive(false);
        }
        else if (Ajuda == 1)
        {
            FindInActiveObjectByName("link1_1").SetActive(true);
            FindInActiveObjectByName("link1").SetActive(true);
        }
        else if (Ajuda == 3)
        {
            FindInActiveObjectByName("link3").SetActive(true);
        }
        else if (ant == 3)
        {
            GameObject.Find("link3").SetActive(false);
        }
    }

    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }


    public void TrocaTela(bool tela)
    {
        if (tela)
        {
            telaNormal.SetActive(false);
            telaAjuda.SetActive(true);
        }
        else
        {
            telaNormal.SetActive(true);
            telaAjuda.SetActive(false);
        }
    }

    public void Fechar()
    {
        Application.Quit();
    }
}
