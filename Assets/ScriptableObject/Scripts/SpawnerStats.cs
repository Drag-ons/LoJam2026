using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerStats", menuName = "ScriptableObjects/SpawnerStats")]
public class SpawnerStats : ScriptableObject
{
    public float timeBetweenWave;
    public int spawnCap;
    public float cullRange;
    public float cullRangeCooldownTime;
    public List<WaveEvents> waveEvents = new();

    [System.Serializable]
    public class WaveEvents
    {
        public int waveTrigger;
        public int spawnSnakeAmount;
        public int spawnDeerAmount;
    }
}
