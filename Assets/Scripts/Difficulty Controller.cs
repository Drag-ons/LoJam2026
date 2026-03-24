using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public float timer;
    public float cooldown;
    void FixedUpdate()
    {
        if (timer > cooldown)
        {
            timer = 0;
            this.GetComponent<EnemyManager>().difficultyup();
        }
        timer++;
    }

}
