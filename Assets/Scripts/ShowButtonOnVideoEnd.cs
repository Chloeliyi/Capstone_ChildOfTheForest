using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ShowButtonOnVideoEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer in the Inspector
    public Button hiddenButton;     // Assign your Button in the Inspector

    void Start()
    {
        // Ensure the button is initially hidden
        hiddenButton.gameObject.SetActive(false);

        // Subscribe to the video end event
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Show the button when the video ends
        hiddenButton.gameObject.SetActive(true);
    }
}
