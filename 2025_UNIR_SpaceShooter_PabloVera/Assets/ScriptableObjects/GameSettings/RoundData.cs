using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Scriptable Objects/RoundData")]
public class RoundData : ScriptableObject
{
    public int numDummies, numSnipper, numDashers, numKamimazes;
    public float dummyRatio, snipperRatio, dasherRatio, kamikazeRatio;
    public float dummySpawnDelay, snipperSpawnDelay, dasherSpawnDelay, kamikazeSpawnDelay;
}
