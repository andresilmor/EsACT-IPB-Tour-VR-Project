using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoExpositionManager : MonoBehaviour
{
    [Header("Video Exposition Config:")]
    [SerializeField] VideoPlayer videoPlayer;
    
    public void SetVideoClip(VideoClip videoClip) {
        Debug.Log("Here");
        videoPlayer.clip = videoClip;
        videoPlayer.Play();

    }

    public void StopVideo() {
        videoPlayer.Stop();

    }

}
