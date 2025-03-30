using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Mixed Status Skill", menuName = "Skills/Mixed Status Skill")]
public class MixedStatusSkill : Skill
{
    public override List<string> UseSkill(Character character, List<Character> targets, int turnCounter)
    {
        List<string> results = new List<string>();
        if (applySelfStatuses.Count > 0)
        {
            foreach (var status in applySelfStatuses)
                SelfStatusApplication(character, status, turnCounter);
        }

        if (removeSelfStatuses.Count > 0)
        {
            foreach (var status in removeSelfStatuses)
                SelfStatusCure(character, status, turnCounter);
        }

        foreach (var target in targets)
        {
            if (!target.isActive)
            {
                results.Add("");
                continue;
            }

            if (applyTargetStatuses.Count > 0)
            {
                foreach (var status in applyTargetStatuses)
                {
                    results.Add(TargetStatusApplication(character, status, target, turnCounter));
                }
            }
            if (removeTargetStatuses.Count > 0)
            {
                foreach (var status in removeTargetStatuses)
                    TargetStatusRemoval(character, status, target, turnCounter);
            }
        }
        return results;
    }
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Status Removal Skill", menuName = "Skills/Status Removal Skill")]
public class StatusRemovalSkill : Skill
{

    public override List<string> UseSkill(Character character, List<Character> targets, int turnCounter)
    {
        List<string> results = new List<string>();
        if (removeSelfStatuses.Count > 0)
        {
            foreach (var status in removeSelfStatuses)
                SelfStatusCure(character, status, turnCounter);
        }

        foreach (var target in targets)
        {
            if (!target.isActive)
            {
                results.Add("");
                continue;
            }

            if (removeTargetStatuses.Count > 0)
            {
                foreach (var status in removeTargetStatuses)
                    TargetStatusRemoval(character, status, target, turnCounter);
            }
        }
        return results;
    }

    public override void UseFieldSkill(PlayerCharacterData character, List<PlayerCharacterData> targets)
    {
        if (removeSelfStatuses.Count > 0)
        {
            foreach (var status in removeSelfStatuses)
            {
                var curingStatus = character.currStatuses.FirstOrDefault(s => s.status == status);
                if (curingStatus != null && curingStatus.canBeCured)
                {
                    character.currStatuses.RemoveAll(s => s.status == status);

                }
            }

        }

        foreach (var target in targets)
        {
            if (!target.isActive)
            {
                continue;
            }

            if (removeTargetStatuses.Count > 0)
            {
                foreach (var status in removeTargetStatuses)
                {
                    var curingStatus = target.currStatuses.FirstOrDefault(s => s.status == status);
                    if (curingStatus != null && curingStatus.canBeCured)
                    {
                        target.currStatuses.RemoveAll(s => s.status == status);
                    }
                }

            }
        }
    }
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Status Application Skill", menuName = "Skills/Status Application Skill")]
public class StatusApplicationSkill : Skill
{
    public override List<string> UseSkill(Character character, List<Character> targets, int turnCounter)
    {
        List<string> results = new List<string>();
        if (applySelfStatuses.Count > 0)
        {
            foreach (var status in applySelfStatuses)
                SelfStatusApplication(character, status, turnCounter);
        }

        foreach (var target in targets)
        {
            if (!target.isActive)
            {
                results.Add("");
                continue;
            }

            if (applyTargetStatuses.Count > 0)
            {
                foreach (var status in applyTargetStatuses)
                {
                    results.Add(TargetStatusApplication(character, status, target, turnCounter));
                }
            }
        }
        return results;
    }
}

