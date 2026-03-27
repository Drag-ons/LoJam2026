using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public bool godMode;
    public float maxSanity;
    public float maxStamina;
    public float staminaRecoveryRate;
    public float maxAbilityPower;
    public float movementSpeed;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    public float dashingCost;
    public float pushingPower;
    public float pushingRange;
    public float pushingAngle;
    public float pushingTime;
    public int pushingCost;
    public int nukeOrbCost;
}
