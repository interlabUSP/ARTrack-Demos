using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class conectarAR : MonoBehaviour
{
    UdpClient clientData;

    [Header("Server UDP")]
    public int portData = 9000;
    public int receiveBufferSize = 120000;

    IPEndPoint ipEndPointData;
    private object obj = null;
    private System.AsyncCallback AC;
    byte[] receivedBytes;
    string receivedString;
    bool pontos = false;
    public Text Conectado;
    public Text Abra;
    bool conected = false;
    void Start()
    {
        ConectarUDP();
    }
    public void ConectarUDP() //Conecta no servidor  do AR TRacking
    {
        ipEndPointData = new IPEndPoint(IPAddress.Loopback, portData);
        clientData = new UdpClient();
        clientData.Client.ReceiveBufferSize = receiveBufferSize;
        clientData.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, optionValue: true);
        clientData.Client.Bind(ipEndPointData);
        AC = new System.AsyncCallback(LeitorUDP);
        clientData.BeginReceive(AC, obj);
    }

    void LeitorUDP(System.IAsyncResult result) // Le os dados enviados pelo AR Tracking
    {
        receivedBytes = clientData.EndReceive(result, ref ipEndPointData);
        byte[] receiveBytes = clientData.Receive(ref ipEndPointData);
        receivedString = Encoding.ASCII.GetString(receiveBytes);
        clientData.BeginReceive(AC, obj);
        if (receivedString != null)
        {
            conected = true;
        }else{
            conected = false;
        }
        
    }


    void Update()
    {
        if (!pontos && !conected)
        {
            pontos = true;
            StartCoroutine(pontinhos());
        }

        if (conected)
        {
            Conectado.text = "";
            Abra.text = "AR Tracking\n conectado!";
            Abra.color = Color.green;
        }else{
            Abra.text = "Abra o\n AR Tracking";
            Abra.color = Color.red;
        }
    }
    IEnumerator pontinhos()
    {
        if (Conectado.text != ".....")
        {
            Conectado.text += ".";
        }
        else
        {
            Conectado.text = ".";
        }
        yield return new WaitForSeconds(0.7f);
        pontos = false;
    }
}

//*Abra o AR Tracking ..*//