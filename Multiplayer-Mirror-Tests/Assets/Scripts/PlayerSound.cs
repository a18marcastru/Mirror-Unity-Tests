using UnityEngine;
using Mirror;

public class PlayerSound : NetworkBehaviour
{
    public MicLoudnessDetector micDetector;
    public float loudnessThreshold = 0.1f; // Volumen mínimo para que se detecte como ruido

    void Update()
    {
        if (!isLocalPlayer) return;  // Solo el jugador local puede enviar su voz

        float loudness = micDetector.GetLoudness();

        if (loudness > loudnessThreshold)
        {
            // Enviar al servidor si el volumen supera el umbral
            CmdReportLoudness(loudness);
        }

        Debug.Log("Volumen del micrófono: " + loudness);
    }

    // Este método se ejecuta en el servidor, y solo el servidor puede llamar a él
    [Command]
    void CmdReportLoudness(float loudness)
    {
        Debug.Log("Jugador hizo ruido con volumen: " + loudness);

        // Aquí puedes agregar lógica para hacer que el enemigo reaccione al ruido.
        // Por ejemplo, podrías hacer que los enemigos se muevan hacia la posición del jugador.

        RpcReactToNoise(loudness);
    }

    // Este método se ejecuta en todos los clientes, y puede usarse para indicar que el servidor ha recibido el ruido
    [ClientRpc]
    void RpcReactToNoise(float loudness)
    {
        if (isLocalPlayer) return;  // No queremos que el jugador local reaccione

        // Aquí podrías agregar lógica para hacer que un enemigo reaccione a la detección de ruido
        Debug.Log("Un enemigo está reaccionando al ruido");
    }
}
