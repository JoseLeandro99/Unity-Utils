using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI txtFpsDisplay;

    [SerializeField] private float pollingTime;
    private int frameCount;
    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            DisplayFps(frameRate);

            time -= pollingTime;
            frameCount = 0;
        }
    }

    private void DisplayFps(int frameRate)
    {
        if (txtFpsDisplay == null)
        {
            Debug.Log("FPS: " + frameRate);
            return;
        }

        txtFpsDisplay.text = string.Format("{0} FPS", frameRate);
    }
}
