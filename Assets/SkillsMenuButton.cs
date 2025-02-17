using UnityEngine;
using UnityEngine.UI;

public class SkillsMenuButton : MonoBehaviour
{
    public Text text;
    public Skill skill;

    public void PopulateButton(Skill newSkill)
    {
        skill = newSkill;
        text.text = skill.skillName;
    }
}
