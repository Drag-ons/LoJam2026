using UnityEngine;

public class HealthPickupController : MonoBehaviour, ICollectable
{
    public void Collect(PlayerResourceController playerResourceController)
    {
        playerResourceController.AddSanity(10);
        Destroy(gameObject);
    }
}
