using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTemplate : MonoBehaviour
{
    private string bossName;
    private int maxHealth;
    private int atk;

    public BossTemplate(string bossName, int maxHealth, int atk)
    {
        this.bossName = bossName;
        this.maxHealth = maxHealth;
        this.atk = atk;
    }

    public virtual float getChance()
    {
        System.Random random = new System.Random();
        double chance = (random.NextDouble() * (1.0));
        return (float)chance;
    }

    public virtual void normalAtk()
    {

    }

    public virtual void skill1()
    {

    }

    public virtual void skill2()
    {

    }

    public virtual void skill3()
    {

    }
}