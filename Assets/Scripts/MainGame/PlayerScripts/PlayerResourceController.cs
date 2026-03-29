using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PlayerResourceController : MonoBehaviour
{
    public FilterControl filter;
    public float sanity;
    public bool canBeDamaged = true;
    public float stamina;
    public float abilityPower;
    public bool canGainAbility = true;
    public bool isDead = false;
    public PlayerStats playerStats;
    public Slider sanityBarSlider;
    public Slider staminaBarSlider;
    public Slider ability1BarSlider;
    public Slider ability2BarSlider;
    public Slider ability3BarSlider;
    public Material damage;
    public LayerMask mask;
    private bool hit;

    private float orbUiIncrements;
    private float abilityIncrements;

    void Start()
    {
        sanity = playerStats.maxSanity;
        sanityBarSlider.maxValue = playerStats.maxSanity;
        sanityBarSlider.value = sanity;

        stamina = playerStats.maxStamina;
        staminaBarSlider.maxValue = playerStats.maxStamina;
        staminaBarSlider.value = stamina;

        abilityPower = playerStats.startingAbilityPower;
        abilityIncrements = playerStats.maxAbilityPower / 3;
        orbUiIncrements = 1 / abilityIncrements;

        damage.SetFloat("_DMG",0);
    }

    void Update()
    {
        stamina = Mathf.Clamp(stamina + playerStats.staminaRecoveryRate, 0, playerStats.maxStamina);
        staminaBarSlider.value = stamina;
        
    }

    public void AddSanity(float health)
    {
        sanity = Mathf.Clamp(sanity + health, 0, playerStats.maxSanity);
        sanityBarSlider.value = sanity;
    }

    public void RemoveSanity(float dmg)
    {
        sanity = Mathf.Clamp(sanity - dmg, 0, playerStats.maxSanity);
        sanityBarSlider.value = sanity;
        damage.SetFloat("_DMG",1);
        if(hit == false)
        {
            AudioManager.Instance.Play(AudioManager.SoundType.Player_Hit);
        }
    
        hit = true;
         
        StartCoroutine(Damagecheck());
        if (sanity <= 0)
        {
            filter.death = true;
            isDead = true;
        }
    }

    public void RemoveStamina(float drain)
    {
        stamina = Mathf.Clamp(stamina - drain, 0, playerStats.maxStamina);
    }

    public void AddAbilityResource(float ability)
    {
        if (canGainAbility)
        {
            abilityPower = Mathf.Clamp(abilityPower + ability, 0, playerStats.maxAbilityPower);
            UpdateOrbUi();
        }
    }

    public void RemoveAbilityOrbs(int abilityOrbCount)
    {
        abilityPower = Mathf.Clamp(abilityPower - abilityOrbCount * abilityIncrements, 0, playerStats.maxAbilityPower);

        ability1BarSlider.value = 0;
        ability2BarSlider.value = 0;
        ability3BarSlider.value = 0;

        UpdateOrbUi();
    }

    private void UpdateOrbUi()
    {
        if (abilityPower >= abilityIncrements)
        {
            ability1BarSlider.value = abilityIncrements * orbUiIncrements;
            if (abilityPower >= (abilityIncrements * 2))
            {
                ability2BarSlider.value = abilityIncrements * orbUiIncrements;
                if (abilityPower >= (abilityIncrements * 3))
                {
                    ability3BarSlider.value = abilityIncrements * orbUiIncrements;
                }
                else
                {
                    ability3BarSlider.value = (abilityPower - (abilityIncrements * 2)) * orbUiIncrements;
                }
            }
            else
            {
                ability2BarSlider.value = (abilityPower - abilityIncrements) * orbUiIncrements;
            }
        }
        else
        {
            ability1BarSlider.value = abilityPower * orbUiIncrements;
        }
    }

    public bool CanUseAbility(int orbCost)
    {
        return abilityPower >= abilityIncrements * orbCost;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy enemy) && canBeDamaged && !playerStats.godMode)
        {
            enemy.DamagePlayer(this);
        }

        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(this);
        }
    }
    IEnumerator Damagecheck()
    {
        while(hit == true)
        {
            
            yield return new WaitForSeconds(2);
            if(Physics2D.OverlapCircle(this.transform.position, 1, mask) == false){
                damage.SetFloat("_DMG",0);
                hit = false;
            }
            
        }
    }
}
