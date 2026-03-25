using System.Collections;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public float cooldownTime;

    private bool canUpDificulty = true;

    void Update()
    {
        if (canUpDificulty)
        {
            canUpDificulty = false;

            this.GetComponent<EnemySpawner>().UpDifficulty();

            StartCoroutine(CooldownRoutine());
        }
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        canUpDificulty = true;
    }

}
