using Mirror;
using UnityEngine;

public class PlayerMic : NetworkBehaviour
{
    public MicrophoneListener micListener;
    [SyncVar] public float currentMicVolume;

    void Update()
    {
        if (isLocalPlayer)
        {
            currentMicVolume = micListener.micLoudness;
            Debug.Log("📡 Enviando volumen al servidor: " + currentMicVolume);
            CmdUpdateMicVolume(currentMicVolume);
        }
    }

    [Command]
    void CmdUpdateMicVolume(float volume)
    {
        currentMicVolume = volume;
        Debug.Log("✅ Volumen recibido en el servidor: " + currentMicVolume);
    }
}
