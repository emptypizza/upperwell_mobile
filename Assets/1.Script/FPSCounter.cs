
using UnityEngine;
using UnityEngine.UI; // If you're using TextMeshPro, replace this with: using TMPro;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f;
    private float lastInterval;
    private int frames = 0;
    private float fps;
    public Text displayText; // If you're using TextMeshPro, change this to: private TMP_Text displayText;

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
   //     displayText = GetComponent<Text>(); // If you're using TextMeshPro, change this to: displayText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        frames++;

        float timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
            displayText.text = $"FPS: {fps.ToString("f2")}";
        }
    }
}
