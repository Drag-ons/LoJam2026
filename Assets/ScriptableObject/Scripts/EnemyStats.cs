using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float minimumSpeed;
    public float maximumSpeed;
    public float damage;
    public float abilityGain;
    public float weight;
    public float minimumSpawnRangeX;
    public float maximumSpawnRangeX;
    public float minimumSpawnRangeY;
    public float maximumSpawnRangeY;
}
