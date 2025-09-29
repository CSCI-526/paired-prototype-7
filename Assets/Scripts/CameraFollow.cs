using UnityEngine;
public class FollowPlayer : MonoBehaviour {
    public Transform player;
    void LateUpdate() {
        transform.position = player.position + new Vector3(0, 5, -10); // Adjust as needed
    }
}
