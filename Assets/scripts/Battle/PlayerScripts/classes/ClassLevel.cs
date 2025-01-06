using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Class Level", menuName = "Classes/Class Level")]
public class ClassLevel : ScriptableObject
{
    public int expNeeded;
    public int attackBuff;
    public int defenseBuff;
    public int magAtkBuff;
    public int magDefBuff;
    public int agilityBuff;

    public Skill acquiredSkill;
}
