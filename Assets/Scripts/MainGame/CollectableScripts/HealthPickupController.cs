using UnityEngine;

public class HealthPickupController : MonoBehaviour, ICollectable
{
    public float healingPower;

    public void Collect(PlayerResourceController playerResourceController)
    {
        playerResourceController.AddSanity(healingPower);
        Destroy(gameObject);
    }
}
