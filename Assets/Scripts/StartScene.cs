using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject Bot;             // prefab to spawn
    public float spawnInterval = 3f;   // seconds between spawns
    private float timer;

    private Vector3 spawnBasePos = new(-228f, .5f, 63f);

    void Update()
    {
        // countdown
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnBot();
            // reset timer with slight randomness
            timer = Random.Range(spawnInterval - 2.9f, spawnInterval + 1f);
        }
    }

    void SpawnBot()
    {
        if (Bot != null)
        {
            float zOffset = Random.Range(-3f, 3f);
            Vector3 spawnPos = new(spawnBasePos.x, spawnBasePos.y, spawnBasePos.z + zOffset);

            Quaternion spawnRot = Quaternion.Euler(0f, 90f, 0f);

            Instantiate(Bot, spawnPos, spawnRot);
        }
        else
        {
            Debug.LogWarning("Bot prefab not assigned in inspector!");
        }
    }

    public void SinglePlayer()
    {
        SceneManager.LoadScene("Assets/Scenes/SinglePlayer.unity");
    }

    public void MultiPlayer()
    {
        Debug.Log("Load MultiPlayer Scene");
    }
}
