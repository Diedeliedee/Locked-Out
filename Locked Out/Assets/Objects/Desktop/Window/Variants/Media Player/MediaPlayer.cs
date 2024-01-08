using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MediaPlayer : MonoBehaviour
{
    [SerializeField] private Slider musicProgress;
    [SerializeField] private TextMeshProUGUI currentTime;
    [SerializeField] private TextMeshProUGUI songDurration;
    [SerializeField] private TextMeshProUGUI songName;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private Sprite[] playButtonImages;
    [SerializeField] private Sprite[] loopButtonImages;
    [SerializeField] private Sprite[] randomButtonImages;
    [SerializeField] private Image playButton;
    [SerializeField] private Image loopButton;
    [SerializeField] private Image randomButton;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private MusicButton MusicButtonPrefab;
    private bool loop;
    private bool random;
    private bool paused;
    private AudioSource source;
    private int currentClip;
    private int previousClip;


    /// <summary>
    /// Finds the audio source and assigns the inital data to it so that the play button can be pressed
    /// </summary>
    public void Awake()
    {
        source = FindObjectOfType<MediaPlayerSource>().GetComponent<AudioSource>();
        changeClip(0);
        TogglePlay();
        playButton.sprite = playButtonImages[0];

        for (int i = 0; i < songs.Length; i++)
        {
            MusicButton b = Instantiate(MusicButtonPrefab, buttonParent);
            b.SetupButton(this, i, songs[i]);
        }
    }

    public void ToggleLooping()
    {
        loop = !loop;
        source.loop = loop;
        //change image
        if (loop)
        {
            loopButton.sprite = loopButtonImages[1];
        }
        else
        {
            loopButton.sprite = loopButtonImages[0];
        }
    }

    public void ToggleRandomize()
    {
        random = !random;
        //change image
        if (random)
        {
            randomButton.sprite = randomButtonImages[1];
        }
        else
        {
            randomButton.sprite = randomButtonImages[0];
        }
    }

    public void TogglePlay()
    {
        paused = !paused;
        if (paused)
        {
            source.Pause();
            playButton.sprite = playButtonImages[0];
        }
        else
        {
            source.UnPause();
            playButton.sprite = playButtonImages[1];
        }
    }

    /// <summary>
    /// calls update timer and checks if the current song is done playing
    /// </summary>
    private void Update()
    {
        UpdateTimer();
        if(!paused && !loop && !source.isPlaying)
        {
            PlayNextClip();
        }
    }

    /// <summary>
    /// Selects the next id to played and stores which song was played last
    /// </summary>
    public void PlayNextClip()
    {
        previousClip = currentClip;
        if (random)
        {
            changeClip(Random.Range(0, songs.Length));
        }
        else
        {
            int i = currentClip;
            i++;
            if(i >= songs.Length)
            {
                i = 0;
            }
            changeClip(i);
        }
    }

    /// <summary>
    /// updates the Slider and time text UI elemets using the audiosource timestamp
    /// </summary>
    private void UpdateTimer()
    {
        if (paused) { return; }
        System.TimeSpan t = System.TimeSpan.FromSeconds(source.time);
        currentTime.text = t.ToString(@"hh\:mm\:ss");
        musicProgress.value = source.time;
    }

    /// <summary>
    /// Sets the clip on the AudioSource to the given ID and updates all UI elements to show the correct information
    /// </summary>
    public void changeClip(int clip)
    {
        currentClip = clip;
        source.clip = songs[currentClip];
        source.Play();
        paused = false;
        playButton.sprite = playButtonImages[1];
        musicProgress.maxValue = songs[currentClip].length;
        System.TimeSpan t = System.TimeSpan.FromSeconds(songs[currentClip].length);
        songDurration.text = t.ToString(@"hh\:mm\:ss");
        currentTime.text = "00:00:00";
        musicProgress.value = 0;
        songName.text = songs[currentClip].name;
    }


    /// <summary>
    /// Changes the time on the audio source to change where the song should be 
    /// </summary>
    public void SliderChaged()
    {
        if(musicProgress.value != source.time)
        {
            source.time = musicProgress.value;
        }
    }

    /// <summary>
    /// changes the clip the the previous clip. Current implentation can only go back one song and then is stuck
    /// </summary>
    public void PreviousClip()
    {
        changeClip(previousClip);
    }

    /// <summary>
    /// stops the Audio source 
    /// </summary>
    public void StopMusic()
    {
        source.Stop();
    }
}
