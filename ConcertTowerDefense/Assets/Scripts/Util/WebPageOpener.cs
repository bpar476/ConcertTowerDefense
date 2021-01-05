using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebPageOpener : MonoBehaviour
{
    public void OpenWebUrl(string url)
    {
        Application.OpenURL(url);
    }
}
