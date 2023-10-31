using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    [SerializeField] public RawImage img;
    private WebCamTexture webCam;
    void Start()
    {
        webCam = new WebCamTexture();
        if(!webCam.isPlaying) webCam.Play();
        img.texture = webCam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
