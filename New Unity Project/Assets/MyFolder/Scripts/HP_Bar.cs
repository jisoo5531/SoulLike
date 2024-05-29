using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{
    public enum Type { Player, Enemy }
    public Type Unit;
    public enum Phase { One, Two }
    public Phase which_Phase;

    Player player;
    Enemy enemy;
    Slider HpBar;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        if (which_Phase == Phase.One)
        {
            enemy = FindObjectOfType<E_UndeadHorse>();
        }
        else if (which_Phase == Phase.Two)
        {
            enemy = FindObjectOfType<E_GroundedUK>();
        }

        HpBar = GetComponent<Slider>();

        if (Unit == Type.Player)
        {            
            HpBar.maxValue = player.MaxHP;
            HpBar.value = player.MaxHP;
        }
        else if (Unit == Type.Enemy)
        {            
            HpBar.maxValue = enemy.MaxHP;

            StartCoroutine(StartBossHPBar());
           
        }
    }
    IEnumerator StartBossHPBar()
    {
        while (true)
        {
            yield return null;

            HpBar.value = Mathf.Lerp(0.0f, enemy.MaxHP, 0.5f * Time.time);
            if (HpBar.value >= enemy.MaxHP)
            {
                break;
            }
        }
    }

    private void Update()
    {
        Set_HPBar();
    }

    void Set_HPBar()
    {
        if (Unit == Type.Player)
        {            
            HpBar.value = Mathf.Lerp(HpBar.value, player.HP, 2.0f * Time.deltaTime);
        }
        else if (Unit == Type.Enemy)
        {            
            HpBar.value = Mathf.Lerp(HpBar.value, enemy.HP, 2.0f * Time.deltaTime);
        }        
    }
}
