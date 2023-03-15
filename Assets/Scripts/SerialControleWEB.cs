using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;
using System;

public class SerialControleWEB : MonoBehaviour
{

    ////////////////////////////////////////////
    float[] oldPositions = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//x ,y, z - right - up - forward
    float[] newPositions = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//x ,y, z- right - up - forward
    JObject json;
    bool uma_vez = false; //Saber quando o programa rodar na primeira vez

    [Serializable]
    public class Configuracoes
    {
        [Header("Inverter Direcao")]
        [Range(-1, 1)]
        public int x_inversor = 1;
        [Range(-1, 1)]
        public int y_inversor = 1;
        [Range(-1, 1)]
        public int z_inversor = 1;
        [Header("Ajustes")]
        [SerializeField]
        public int Sensibilidade = 5;
        [Header("Limites")]
        [SerializeField]
        public bool Limitar = true;
        public float max_x = 4;
        public float min_x = -4;
        public float max_y = 2.5f;
        public float min_y = -2.5f;
        public float max_z = 4;
        public float min_z = -4;


    }



    [Header("Objetos")]

    public GameObject Cubo; // Objeto que sera movimentado
    //bool Movimentar = false; // Booleana para liberar o movimento do objeto
    string new_timestamp, old_timestamp; //Variaveis que guardam o tempo enviado pelo ar tracking


    Vector3 UltimaPos;
    StreamWriter arquivo;
    string[] jtokens = {"timestamp","success","translation_x","translation_y","translation_z","rotation_right_x"
    ,"rotation_right_y","rotation_right_z","rotation_up_x","rotation_up_y","rotation_up_z","rotation_forward_x"
    ,"rotation_forward_y","rotation_forward_z"};


    public Text Infos_debug; //Texto que mostra os dados recebidos do ar tracking
    string receivedString;

    public Configuracoes config;
    void Start()
    {
        
    }
    


    void Update()
    {
         MovimentaCubo();

    }


    void MovimentaCubo()
    {
        try // Movimenta Cubo
        {
            if (json["success"].ToString() == "True")
            {
                Infos_debug.text = receivedString;
                SalvaDadosJson();//Salva os dados recebidos


                


                if (uma_vez == false) // executa uma vez para o cubo nao ir longe
                {
                    oldPositions[0] = newPositions[0];
                    oldPositions[1] = newPositions[1];
                    oldPositions[2] = newPositions[2];
                    uma_vez = true;
                    UltimaPos = Cubo.transform.position;
                }


                //Translacao
                Cubo.transform.Translate(new Vector3((newPositions[0] - oldPositions[0]) * config.x_inversor, (-newPositions[1] + oldPositions[1]) * config.y_inversor, (-newPositions[2] + oldPositions[2]) * config.z_inversor), Space.World);

                if(config.Limitar)LimitesCubo();//Limites  de translacao do cubo

                //Rotacao 
                Vector3 up = new Vector3(newPositions[6], newPositions[7], newPositions[8]);
                Vector3 forward = new Vector3(newPositions[9], newPositions[10], newPositions[11]);
                Cubo.transform.localRotation = Quaternion.LookRotation(forward, up);


                //Salva posicoes antigas
                //Debug.Log(news[0]);
                oldPositions[0] = newPositions[0];
                oldPositions[1] = newPositions[1];
                oldPositions[2] = newPositions[2];
                old_timestamp = new_timestamp;
                UltimaPos = Cubo.transform.position;
            }
            else
            {//Caso o cubo nao esteja sendoreconhecido
                new_timestamp = json["timestamp"].ToString();
                Infos_debug.text = "Nao Conectado " + new_timestamp;
                /* TIMESPAN do sistema
                TimeSpan tempo = System.DateTime.Now.Subtract(System.DateTime.UnixEpoch);
                Debug.Log(tempo.TotalMilliseconds / 1000 + " aaaa");
                Debug.Log(new_timestamp + " bbbb");
                */
                old_timestamp = new_timestamp;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    void LimitesCubo()
    {
        if (Cubo.transform.position.z < config.min_z || Cubo.transform.position.z > config.max_z)
        {//Limite de posicao z
            Cubo.transform.position = UltimaPos;
        }
        else if (Cubo.transform.position.x < config.min_x || Cubo.transform.position.x > config.max_x)
        { //limite x
            Cubo.transform.position = UltimaPos;
        }
        else if (Cubo.transform.position.y < config.min_y || Cubo.transform.position.y > config.max_y)
        { //limite z
            Cubo.transform.position = UltimaPos;
        }
    }
    void SalvaDadosJson()
    {
        newPositions[0] = float.Parse(json["translation_x"].ToString()) / config.Sensibilidade;
        newPositions[1] = float.Parse(json["translation_y"].ToString()) / config.Sensibilidade;
        newPositions[2] = float.Parse(json["translation_z"].ToString()) / config.Sensibilidade;
        newPositions[6] = float.Parse(json["rotation_up_x"].ToString());
        newPositions[7] = float.Parse(json["rotation_up_y"].ToString());
        newPositions[8] = float.Parse(json["rotation_up_z"].ToString());
        newPositions[9] = float.Parse(json["rotation_forward_x"].ToString());
        newPositions[10] = float.Parse(json["rotation_forward_y"].ToString());
        newPositions[11] = float.Parse(json["rotation_forward_z"].ToString());
        new_timestamp = json["timestamp"].ToString();
    }


    void OnDestroy()
    {
    

    }

}


/* RETORNO do AR TRACKING
{"timestamp": 1677594319.8186967, "success": true, "translation_x": 11.430583295477035, "translation_y": 5.142802280146702, "translation_z": 42.815210412740825, 
"rotation_right_x": -0.9399653958653161, "rotation_right_y": 0.31731478045920997, "rotation_right_z": -0.12560407906546253, "rotation_up_x": -0.3016497091359976, 
"rotation_up_y": -0.6003944856751889, "rotation_up_z": 0.7406307545254879, "rotation_forward_x": 0.15960108882438012, "rotation_forward_y": 0.7340557142839697, 
"rotation_forward_z": 0.6600679516331054}
*/

