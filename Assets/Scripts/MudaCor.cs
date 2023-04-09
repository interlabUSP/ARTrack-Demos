using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudaCor : MonoBehaviour
{
    Color cor;
    public bool olhando = false;
    bool vendo = false;
    [SerializeField]
    public string Informacao;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {


        Color corB = this.GetComponent<SpriteRenderer>().color;
        if (corB.r <= 0.02f && corB.b <= 0.02f)
        {
            GameObject.Find("InfoPin").GetComponent<Text>().text = Informacao;
        }

        if (!olhando) { DiminuiCor(); } else { AumentaCor(); };
    }

    public void AumentaCor()
    {

        Color corA = this.GetComponent<SpriteRenderer>().color;
        if (corA.r >= 0.02f && corA.b >= 0.02f)
        {
            corA.r -= 0.9f * Time.deltaTime;
            corA.b -= 0.9f * Time.deltaTime;
            this.GetComponent<SpriteRenderer>().color = corA;
        }


        //Debug.Log(corA);
    }

    void DiminuiCor()
    {



        Color corC = this.GetComponent<SpriteRenderer>().color;
        //Debug.Log(corA);
        if (corC.r < 1f && corC.b < 1f)
        {
            corC.r += 0.5f * Time.deltaTime;
            corC.b += 0.5f * Time.deltaTime;
            this.GetComponent<SpriteRenderer>().color = corC;
        }
    }
}
