﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpener : MonoBehaviour
{
    public void Open(string Url)
    {
        Application.OpenURL(Url);
    }
}
