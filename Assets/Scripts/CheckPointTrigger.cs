using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public static System.Action<string, string> OnAnyPlaneTrigger;

    private void OnTriggerEnter(Collider other)
    {
        OnAnyPlaneTrigger?.Invoke(transform.parent.name, other.gameObject.name);
    }
}
