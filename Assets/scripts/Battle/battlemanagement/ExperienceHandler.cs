using UnityEngine;

[System.Serializable]
public class ExperienceHandler
{
    public AnimationCurve experienceCurve;
    public int nextLevelExp;
    int prevLevelExp;


    public void AddExperience(PlayerCharacter character, int amount)
    {
        character.exp += amount;
        CheckForLevelUp(character);
    }

    public void CheckForLevelUp(PlayerCharacter character)
    {
        while (character.exp >= nextLevelExp)
        {
            character.level++;
            character.OnLevelUp();
            UpdateLevel(character);
        }
    }

    public void UpdateLevel(PlayerCharacter character)
    {
        prevLevelExp = (int)experienceCurve.Evaluate(character.level);
        nextLevelExp = (int)experienceCurve.Evaluate(character.level + 1);
    }
}
