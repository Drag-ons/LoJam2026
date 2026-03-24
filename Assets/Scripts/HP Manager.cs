using UnityEngine;

public class HPManager : MonoBehaviour
{
    public float hp;
    public float hpmax;
    public bool isenemy;
    public GameObject deathmenu;
    void Start()
    {
        hp = hpmax;
    }
    public void Damage(float dmg)
    {
        hp = Mathf.Clamp(hp - dmg, 0, hpmax);

    }
    void Update()
    {
        if (hp <= 0)
        {
            if (isenemy)
            {
                Destroy(this);
            }
            else
            {
                deathmenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
