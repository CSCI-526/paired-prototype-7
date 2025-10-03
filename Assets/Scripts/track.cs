using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class track : MonoBehaviour
{
    public GameObject startLights;
    private float startTimer = 1.5f;
    private int lightCount = 0;
    private readonly List<CheckPointCheck> players = new();

    class CheckPointCheck
    {
        public int playerID;
        public GameObject player;
        public float playerTimer;
        public int currentSection;
        public GameObject checkpoint;

        public CheckPointCheck(int playerID, GameObject player, GameObject checkpoint)
        {
            this.playerTimer = 15f;
            this.currentSection = 0;
            this.player = player;
            this.playerID = playerID;
            this.checkpoint = checkpoint;
        }
    }

    private void Start()
    {
        players.Add(new CheckPointCheck(0, GameObject.Find("Player 0"), GameObject.Find("Track/StartStraight 0/Checkpoint")));
        if (SceneManager.GetActiveScene().name == "MultiPlayer") players.Add(new CheckPointCheck(1, GameObject.Find("Player 1"), GameObject.Find("Track/StartStraight 0/Checkpoint")));
    }

    private void Update()
    {
        if (lightCount < 6)
        {
            startTimer -= Time.deltaTime;

            if (startTimer <= 0f)
            {
                TurnOnLight();

                lightCount++;

                startTimer = 1.5f;

                if (lightCount == 5) startTimer += Random.Range(-.25f, .5f);
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].playerTimer -= Time.deltaTime;

            if (players[i].playerTimer <= 0f)
            {
                Debug.Log($"Player {players[i].playerID} is out of time. Current Checkpoint {players[i].currentSection}"); // need to move the player to the reset spot
                players[i].playerTimer = 5f;

                RectTransform rt = players[i].player.GetComponent<RectTransform>();
                if (players.Count > 1)
                {
                    Vector3 pos = players[i].checkpoint.transform.position;
                    if (i == 0) pos.x -= 1;
                    else pos.x += 1;
                    rt.position = pos;
                }
                else rt.position = players[i].checkpoint.GetComponent<Transform>().position;
                rt.rotation = players[i].checkpoint.transform.parent.GetComponent<Transform>().rotation;

                Rigidbody rb = players[i].player.GetComponent<Rigidbody>();
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void TurnOnLight()
    {
        if (lightCount == 5)
        {
            startLights.SetActive(false);
        }
        else
        {
            GameObject light = startLights.transform.Find($"l{lightCount + 1}/light").gameObject;
            light.SetActive(true);
        }
    }

    private void OnEnable()
    {
        CheckPointTrigger.OnAnyPlaneTrigger += HandlePlaneTrigger;
    }

    private void OnDisable()
    {
        CheckPointTrigger.OnAnyPlaneTrigger -= HandlePlaneTrigger;
    }

    private void HandlePlaneTrigger(Transform section, string obj)
    {
        string[] parts1 = obj.Split();
        int playerID = int.Parse(parts1[1]);

        string[] parts2 = section.name.Split();
        int sectionID = int.Parse(parts2[1]);

        UpdateSection(playerID, sectionID, section.Find("Checkpoint").gameObject);
    }

    private void UpdateSection(int playerID, int sectionID, GameObject checkpoint)
    {
        if (players[playerID].currentSection + 1 == sectionID)
        {
            players[playerID].currentSection++;
            players[playerID].playerTimer = 5f;
            players[playerID].checkpoint = checkpoint;

            if (sectionID == 91) Debug.Log("Race Over");
        }
    }
}
