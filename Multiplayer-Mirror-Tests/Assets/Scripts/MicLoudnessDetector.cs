using UnityEngine;

public class MicLoudnessDetector : MonoBehaviour
{
    private AudioClip micClip;
    private const int sampleWindow = 128;
    private string micDevice;

    void Start()
    {
        // Iniciar grabación del micrófono
        if (Microphone.devices.Length > 0)
        {
            micDevice = Microphone.devices[0];
            micClip = Microphone.Start(micDevice, true, 1, 44100);
        }
        else
        {
            Debug.LogWarning("No se detectó micrófono.");
        }
    }

    public float GetLoudness()
    {
        int micPosition = Microphone.GetPosition(null) - sampleWindow;
        if (micPosition < 0) return 0;

        float[] samples = new float[sampleWindow];
        micClip.GetData(samples, micPosition);

        float maxLoudness = 0f;
        foreach (float sample in samples)
        {
            float abs = Mathf.Abs(sample);
            if (abs > maxLoudness)
                maxLoudness = abs;
        }

        return maxLoudness;
    }
}
