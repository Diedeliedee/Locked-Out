using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DateTimeUIElement : MonoBehaviour
{
    private TextMeshProUGUI text;
    private DateTime localDate;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(updateTime());
    }

    public void StopUpdating()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// updates the UI elemet every 15 seconds
    /// </summary>
    IEnumerator updateTime()
    {
        while (true) //cursed I know but it should loop as long as the game runs
        {
            localDate = DateTime.Now;
            text.text = localDate.ToString("h:mm tt d/MM/yyyy");
            yield return new WaitForSecondsRealtime(15);
        }
    }
}
