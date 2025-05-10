using UnityEngine;

public abstract class Skill
{
    public string skillName;
    public int mpCost;

    // This will be overridden in the derived classes to implement the skill's logic.
    public abstract bool Execute(Unit user, Unit target);
}

public class GoodieSkill : Skill
{

    public GoodieSkill()
    {
        skillName = "Goodie Bag";
        mpCost = 0;
    }
    public override bool Execute(Unit user, Unit target)
    {
        int goodie = Random.Range(-2, 3);
        target.TakeDamage(user.ATK + goodie);
        return target.currentHP <= 0;  // Check if the target is dead
    }
}

public class PowerSkill : Skill
{
    public PowerSkill()
    {
        skillName = "Power Attack";
        mpCost = 2;
    }
    public override bool Execute(Unit user, Unit target)
    {
        if(user.UseMP(mpCost))
        {
            target.TakeDamage(user.ATK * 2);
            return target.currentHP <= 0;  // Check if target is dead
        }
        return false;  // Not enough MP, cannot execute
    }
}

public class RecoverSkill : Skill
{
    public RecoverSkill()
    {
        skillName = "Recover";
        mpCost = 4;
    }
    public override bool Execute(Unit user, Unit target)
    {
        if (user.UseMP(mpCost))
        {
            user.currentHP += 10;  // Heal the user by 10 HP
            return false;  // Target is not dead
        }
        return false;  // Not enough MP, cannot execute
    }
}

public class EmpowerSkill : Skill
{
    public EmpowerSkill()
    {
        skillName = "Empower";
        mpCost = 1;
    }
    public override bool Execute(Unit user, Unit target)
    {
        if (user.UseMP(mpCost))
        {
            user.ATK += 1;  // Increase user's ATK by 1
            return false;  // Target is not dead
        }
        return false;  // Not enough MP, cannot execute
    }
}


