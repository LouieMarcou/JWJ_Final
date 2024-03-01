using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class AndroidMusicFinder : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private string pathName;
    private WebCamDevice m_Device;
    private WebCamTexture m_Texture;


    private void Start()
    {
        WebCamDevice device = WebCamTexture.devices[0];
    }


}
