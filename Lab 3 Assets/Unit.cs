using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int ATK;
    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;

    public List<Skill> skills = new List<Skill>();

    // Initialize skills
    public void SetUpSkills()
    {
        skills.Add(new GoodieSkill());  // Goodie Bag
        skills.Add(new PowerSkill()); // Power Attack
        skills.Add(new RecoverSkill());    // Recover
        skills.Add(new EmpowerSkill());    // Empower
    }

    // Take Damage logic
    public bool TakeDamage(int ATK)
    {
        currentHP -= ATK;
        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        return false;
    }

    // Use MP logic
    public bool UseMP(int mpCost)
    {
        if (currentMP >= mpCost)
        {
            currentMP -= mpCost;
            return true;
        }
        return false;
    }

    // Set HP and MP
    public void SetHP(int hp)
    {
        currentHP = hp;
    }

    public void SetMP(int mp)
    {
        currentMP = mp;
    }
}

