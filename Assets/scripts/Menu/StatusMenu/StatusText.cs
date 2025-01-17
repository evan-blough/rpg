using UnityEngine;
using UnityEngine.UI;
public class StatusText : MonoBehaviour
{
    public Text text;
    public Status status;

    public void PopulateText(Status newStatus, bool isActive)
    {
        status = newStatus;
        text.text = status.ToString();
        text.color = isActive ? Color.white : Color.gray;
    }
}
