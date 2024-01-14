using UnityEngine;

public class FixedAspectRatioWindow : MonoBehaviour {
    public Vector2 aspectRatio = new Vector2(16, 9); // Desired aspect ratio (e.g., 16:9)

    private int screenWidth;
    private int screenHeight;
    private bool isFullscreen;

    void Start() {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        isFullscreen = Screen.fullScreen;
    }

    void Update() {
        if (Screen.width != screenWidth) {
            // Calculate the target height based on the desired aspect ratio
            int targetHeight = Mathf.RoundToInt(Screen.width / aspectRatio.x * aspectRatio.y);

            // Set the screen resolution to match the target aspect ratio
            Screen.SetResolution(Screen.width, targetHeight, isFullscreen);

            // Update the stored values
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            isFullscreen = Screen.fullScreen;
        } else if (Screen.height != screenHeight) {
            // Calculate the target height based on the desired aspect ratio
            int targetWidth = Mathf.RoundToInt(Screen.height / aspectRatio.y * aspectRatio.x);

            // Set the screen resolution to match the target aspect ratio
            Screen.SetResolution(targetWidth, Screen.height, isFullscreen);

            // Update the stored values
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            isFullscreen = Screen.fullScreen;
        }
    }
}