using UnityEngine;
public class Player1Controller : MonoBehaviour {
    public float speed = 10f;
    void Update() {
        float h = 0, v = 0;
        if (Input.GetKey(KeyCode.W)) v += 1;
        if (Input.GetKey(KeyCode.S)) v -= 1;
        if (Input.GetKey(KeyCode.A)) h -= 1;
        if (Input.GetKey(KeyCode.D)) h += 1;

        Vector3 movement = new Vector3(h, 0, v).normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
