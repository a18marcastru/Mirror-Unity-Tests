using UnityEngine;
using Mirror;

public class PlayerSound : NetworkBehaviour
{
    public MicLoudnessDetector micDetector;
    public float loudnessThreshold = 0.1f; // Volumen m�nimo para que se detecte como ruido

    void Update()
    {
        if (!isLocalPlayer) return;  // Solo el jugador local puede enviar su voz

        float loudness = micDetector.GetLoudness();

        if (loudness > loudnessThreshold)
        {
            // Enviar al servidor si el volumen supera el umbral
            CmdReportLoudness(loudness);
        }

        Debug.Log("Volumen del micr�fono: " + loudness);
    }

    // Este m�todo se ejecuta en el servidor, y solo el servidor puede llamar a �l
    [Command]
    void CmdReportLoudness(float loudness)
    {
        Debug.Log("Jugador hizo ruido con volumen: " + loudness);

        // Aqu� puedes agregar l�gica para hacer que el enemigo reaccione al ruido.
        // Por ejemplo, podr�as hacer que los enemigos se muevan hacia la posici�n del jugador.

        RpcReactToNoise(loudness);
    }

    // Este m�todo se ejecuta en todos los clientes, y puede usarse para indicar que el servidor ha recibido el ruido
    [ClientRpc]
    void RpcReactToNoise(float loudness)
    {
        if (isLocalPlayer) return;  // No queremos que el jugador local reaccione

        // Aqu� podr�as agregar l�gica para hacer que un enemigo reaccione a la detecci�n de ruido
        Debug.Log("Un enemigo est� reaccionando al ruido");
    }
}
