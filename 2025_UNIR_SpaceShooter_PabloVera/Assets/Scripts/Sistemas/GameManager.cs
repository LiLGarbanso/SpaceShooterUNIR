using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numRondas;
    private int currentRound;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        EventBus.EmpezarPartida();
        currentRound = 0;
        SiguienteRonda();
    }

    public void SiguienteRonda()
    {
        currentRound++;
        if (currentRound > numRondas)
        {
            Debug.Log("NIVEL COMPPLETADO");
            return;
        }
        EventBus.NextRound(currentRound);
    }

    private void OnDisable()
    {
        
    }
}
