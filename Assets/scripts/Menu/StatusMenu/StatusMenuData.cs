using UnityEngine;

public class StatusMenuData : MonoBehaviour
{
    PlayerCharacterData data;

    public void PopulateData(PlayerCharacterData pcd)
    {
        pcd = data;
        return;
    }
}
