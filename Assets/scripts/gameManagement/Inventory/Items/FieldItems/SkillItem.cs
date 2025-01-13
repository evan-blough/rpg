using System.Collections.Generic;
using System.Linq;

public class SkillItem : FieldItem
{
    public Skill skillToAdd;

    public override bool UseItemInField(List<PlayerCharacterData> targets)
    {
        List<string> returns = new List<string>();

        foreach (PlayerCharacterData target in targets)
        {
            if (target.skills.Contains(skillToAdd))
            {
                continue;
            }

            target.skills.Add(skillToAdd);
            if (target.equippedSkills.Count < target.skillCount)
            {
                target.equippedSkills.Add(skillToAdd);
            }
            returns.Add("This could be nice...");
        }

        return returns.Any();
    }
}
