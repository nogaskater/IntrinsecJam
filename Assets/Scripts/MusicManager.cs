using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public enum MusicType { MENU, INGAME }
    public MusicType SelectedMusicType;
    private void Start()
    {
        if (SelectedMusicType == MusicType.MENU)
        {
            //AudioManager.Instance.PlaySound("Menu Music");
            AudioManager.Instance.StopSound("Ambience");
        }
        else if (SelectedMusicType == MusicType.INGAME)
        {
            //AudioManager.Instance.StopSound("Menu Music");
            AudioManager.Instance.PlaySound("Ambience");
        }
    }
}
