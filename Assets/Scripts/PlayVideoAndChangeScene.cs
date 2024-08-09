using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideoAndChangeScene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the Inspector

    void Start()
    {
        // If videoPlayer is not assigned, try to get the VideoPlayer component on this GameObject
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Subscribe to the loopPointReached event to detect when the video ends
        videoPlayer.loopPointReached += OnVideoEnd;

        // Play the video
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Change the scene when the video finishes
        SceneManager.LoadScene(0);
    }
}
