using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI durrationText;

    private int id;
    private MediaPlayer player;

    public void Onclick()
    {
        player.changeClip(id);
    }

    /// <summary>
    /// Sets all variables on the instantiated button 
    /// </summary>
    public void SetupButton(MediaPlayer player,int id, AudioClip clip)
    {
        this.player = player;
        this.id = id;

        nameText.text = clip.name;
        System.TimeSpan t = System.TimeSpan.FromSeconds(clip.length);
        durrationText.text = t.ToString(@"hh\:mm\:ss");
    }
}
