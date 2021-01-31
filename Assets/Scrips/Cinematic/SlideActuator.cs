using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SlideActuator : MonoBehaviour
{
    public VideoPlayer VideoPlayerGO;
    private long playerCurrentFrame;
    private long playerFrameCount;

    private void Start()
    {
        VideoPlayerGO.Play();
        playerCurrentFrame = VideoPlayerGO.GetComponent<VideoPlayer>().frame;
        playerFrameCount = Convert.ToInt64(VideoPlayerGO.GetComponent<VideoPlayer>().frameCount) - 5;
    }

    private void Update()
    {
        playerCurrentFrame = VideoPlayerGO.GetComponent<VideoPlayer>().frame;
        if (playerCurrentFrame >= playerFrameCount)
        {
            SceneManager.LoadScene("GameScene");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
