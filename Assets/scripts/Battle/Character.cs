using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Elements { None, Fire, Water, Earth, Electric, Wind, Light, Dark, }
public abstract class Character : MonoBehaviour
{
    public string unitName;
    public int level;
    public int maxHP;
    public int maxSP;
    public int currSP;
    public int currHP;
    public int strength;
    public int constitution;
    public int intelligence;
    public int spirit;
    public int speed;
    public int luck;
    public List<Statuses> currStatuses;
    public virtual List<Status> resistances { get; }
    public virtual List<Status> immunities { get; }
    public virtual List<Elements> elemWeaknesses { get; }
    public virtual List<Elements> elemResistances { get; }
    public virtual List<Elements> elemImmunities { get; }
    public bool isActive;
    public bool isBackRow;

    public virtual int attack => strength;
    public virtual int defense => constitution;
    public virtual int magAtk => intelligence;
    public virtual int magDef => spirit;
    public virtual int agility => speed;
    public virtual int hitPercent => 100;
    public virtual int dodgePercent => 0;

    public virtual float FindPhysicalDamageStatusModifier()
    {
        float modifier = 1;

        if (currStatuses.Where(s => s.status == Status.Vulnerable).FirstOrDefault() != null) { modifier *= 2; }

        if (currStatuses.Where(s => s.status == Status.Afraid).FirstOrDefault() != null) { modifier *= 1.5f; }

        if (currStatuses.Where(s => s.status == Status.Defending).FirstOrDefault() != null) { modifier /= 2; }

        return modifier;
    }

    public virtual float FindPhysicalAttackStatusModifier()
    {
        float modifier = 1;

        if (currStatuses.Where(s => s.status == Status.Vulnerable).FirstOrDefault() != null) { modifier /= 2; }

        if (currStatuses.Where(s => s.status == Status.Afraid).FirstOrDefault() != null) { modifier /= 1.5f; }

        if (currStatuses.Where(s => s.status == Status.Berserk).FirstOrDefault() != null) { modifier *= 1.5f; }

        return modifier;
    }

    public virtual float FindElementalDamageModifier(List<Elements> atkElements)
    {
        float modifier = 1;

        foreach (var element in elemImmunities)
        {
            if (atkElements.Any(x => x == element)) return 0;
        }

        foreach (var element in elemResistances)
        {
            if (atkElements.Any(x => x == element)) modifier /= 2;
        }

        foreach (var element in elemWeaknesses)
        {
            if (atkElements.Any(x => x == element)) modifier *= 2;
        }

        return modifier;
    }

    public virtual void ApplyStatuses(List<Statuses> statuses, int turnCounter)
    {
        foreach (var status in statuses)
        {
            Statuses newStatus = new Statuses(status);
            if (immunities.Any(s => s == newStatus.status))
            {
                continue;
            }
            else if (!resistances.Any(s => s == newStatus.status) || Random.Range(0, 1) == 1)
            {
                if (newStatus.accuracy * 100 >= Random.Range(1, 100))
                {
                    if (currStatuses.FirstOrDefault(s => s.status == newStatus.status) != null)
                    {
                        currStatuses.RemoveAll(s => s.status == newStatus.status);
                    }

                    newStatus.expirationTurn += turnCounter;
                    currStatuses.Add(newStatus);

                    if (newStatus.status == Status.Death)
                    {
                        currHP = 0;
                        isActive = false;
                    }
                }
            }
        }
    }

    public virtual void RemoveStatuses(List<Status> statusesToRemove)
    {
        foreach (var status in statusesToRemove)
        {
            var curingStatus = currStatuses.FirstOrDefault(s => s.status == status);
            if (curingStatus != null && curingStatus.canBeCured)
            {
                currStatuses.RemoveAll(s => s.status == status);
            }
        }
    }

    public virtual int TakeDamage(Attack attack)
    {
        int damage = attack.damage - (attack.isMagic ? magDef : defense);
        damage = (int)(damage * FindElementalDamageModifier(attack.attackElements));

        if (attack.isCritical)
            damage *= 2;

        if (!attack.isMagic)
            damage = (int)(damage * FindPhysicalDamageStatusModifier());

        ApplyStatuses(attack.attackStatuses, attack.turnCounter);
        RemoveStatuses(attack.statusToRemove);

        if (damage < 0) damage = 0;
        if (damage > 9999) damage = 9999;

        currHP -= damage;

        if (currHP <= 0)
        {
            isActive = false;
            currHP = 0;
        }

        return damage;
    }

    public virtual int Attack(Character enemy, int turnCounter)
    {
        double hitChance = hitPercent - enemy.dodgePercent;

        if (hitChance >= UnityEngine.Random.Range(0, 100))
        {
            bool criticalValue = UnityEngine.Random.Range(1, 20) == 20 ? true : false;
            int damage = (int)((attack * FindPhysicalAttackStatusModifier() * UnityEngine.Random.Range(1f, 1.25f)));

            Attack currAttack = new Attack(damage, criticalValue, false);
            damage = enemy.TakeDamage(currAttack);

            return damage;
        }
        return 0;
    }
    public virtual bool FleeCheck(Character enemy, int turnCounter)
    {
        double playerHpMultiplier = maxHP / currHP;
        double enemyHpMultiplier = enemy.currHP / enemy.maxHP;
        int playerChance = UnityEngine.Random.Range(1, 25);

        double playerFleeNum = playerHpMultiplier * (.5 * luck + agility) + playerChance;
        double enemyFleeNum = enemyHpMultiplier * (enemy.luck + enemy.agility);

        return playerFleeNum > enemyFleeNum;
    }

    public virtual void DeepCopyFrom(CharacterData c)
    {
        unitName = c.unitName;
        level = c.level;
        maxHP = c.maxHP;
        maxSP = c.maxSP;
        currSP = c.currSP;
        currHP = c.currHP;
        strength = c.strength;
        constitution = c.constitution;
        intelligence = c.intelligence;
        spirit = c.spirit;
        speed = c.speed;
        luck = c.luck;
        currStatuses = c.currStatuses.ConvertAll(cs => new Statuses(cs)).ToList();
        isActive = c.isActive;
        isBackRow = c.isBackRow;
    }
}
