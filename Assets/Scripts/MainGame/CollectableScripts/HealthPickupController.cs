using UnityEngine;

public class HealthPickupController : MonoBehaviour, ICollectable
{
    public float healingPower;

    public void Collect(PlayerResourceController playerResourceController)
    {
         if (playerResourceController.sanity < playerResourceController.playerStats.maxSanity)
        {
            playerResourceController.AddSanity(healingPower);
            Destroy(gameObject);
        }
    }
}
