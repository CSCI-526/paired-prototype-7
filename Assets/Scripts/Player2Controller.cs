using UnityEngine;
public class Player2Controller : MonoBehaviour {
    public float speed = 10f;
    void Update() {
        float h = 0, v = 0;
        if (Input.GetKey(KeyCode.UpArrow)) v += 1;
        if (Input.GetKey(KeyCode.DownArrow)) v -= 1;
        if (Input.GetKey(KeyCode.LeftArrow)) h -= 1;
        if (Input.GetKey(KeyCode.RightArrow)) h += 1;

        Vector3 movement = new Vector3(h, 0, v).normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
