using UnityEngine;

public class HPManager : MonoBehaviour
{
    public int hp;
    public int hpmax;
    public bool isenemy;
    public GameObject deathmenu;
    void Start()
    {
        hp = hpmax;
    }
    public void Damage(int dmg)
    {
        hp = hp - dmg;
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
