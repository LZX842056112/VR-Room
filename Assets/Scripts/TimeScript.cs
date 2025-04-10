using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class TimeScript : MonoBehaviour
{
    // show time on clock (unity)
    public GameObject hourHand;
    public GameObject minuteHand;
    public GameObject secondHand;

    private void OnEnable()
    {
        //SoundManager
    }
    void Update()
    {
        UpdateRealTimer();
    }
    private void UpdateRealTimer()
    {
        var currentTime = DateTime.Now;

        var secondsDegree = 360f / 60f;
        var minutesDegree = 360f / 60f;
        var hoursDegree = 360f / 12f;

        secondHand.transform.localRotation = Quaternion.Euler(new Vector3(secondsDegree * currentTime.Second + 84, 0, -90));
        minuteHand.transform.localRotation = Quaternion.Euler(new Vector3(minutesDegree * currentTime.Minute + 84, 0, -90));
        hourHand.transform.localRotation = Quaternion.Euler(new Vector3(hoursDegree * currentTime.Hour + 84, 0, -90));

    }
}
