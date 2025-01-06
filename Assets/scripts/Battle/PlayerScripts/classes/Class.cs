using System;

[Serializable]
public class Class
{
    public ClassSlot classSlot;
    public ClassLevel currLevel;
    public int classXp = 0;
    public int classLevel = 1;
    public bool isMaxed = false;

    public int classAtkMod
    {
        get
        {
            int returnVal = classSlot.classAtkBuff;
            foreach (var level in classSlot.levels)
            {
                if (classSlot.levels.IndexOf(level) <= classLevel)
                    returnVal += level.attackBuff;
            }

            return returnVal;
        }
    }
    public int classDefMod
    {
        get
        {
            int returnVal = classSlot.classDefBuff;
            foreach (var level in classSlot.levels)
            {
                if (classSlot.levels.IndexOf(level) <= classLevel)
                    returnVal += level.defenseBuff;

            }

            return returnVal;
        }
    }
    public int classMgAtkMod
    {
        get
        {
            int returnVal = classSlot.classMgAtkBuff;
            foreach (var level in classSlot.levels)
            {
                if (classSlot.levels.IndexOf(level) <= classLevel)

                    returnVal += level.magAtkBuff;
            }

            return returnVal;
        }
    }
    public int classMgDefMod
    {
        get
        {
            int returnVal = classSlot.classMgDefBuff;
            foreach (var level in classSlot.levels)
            {
                if (classSlot.levels.IndexOf(level) <= classLevel)
                    returnVal += level.magDefBuff;
            }

            return returnVal;
        }
    }
    public int classAgilityMod
    {
        get
        {
            int returnVal = classSlot.classAgilityBuff;
            foreach (var level in classSlot.levels)
            {
                if (classSlot.levels.IndexOf(level) <= classLevel)
                    returnVal += level.agilityBuff;
            }

            return returnVal;
        }
    }

    public void OnClassLevelUp(PlayerCharacter character)
    {
        if (classSlot.levels[classLevel - 1] is null)
        {
            isMaxed = true;
            character.AutoUpdateCurrentClass();
        }
        else
        {
            currLevel = classSlot.levels[classLevel - 1];
        }
    }
}
