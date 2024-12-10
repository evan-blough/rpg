using UnityEngine;

public class RadiusTester : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Character entered radius.");
    }
    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Character stayed in radius.");
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Character left the radius... check logic.");
    }
}
