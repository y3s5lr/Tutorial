using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControllerTask2 : MonoBehaviour
{
    [SerializeField] public int score = 0;
    [SerializeField] public float timer = 60.0f;
    [SerializeField] public bool LevelComplete = false;

    [SerializeField] TextMeshProUGUI ScoreTextBox;
    [SerializeField] TextMeshProUGUI TimerTextBox;
    [SerializeField] TextMeshProUGUI CompeletionTextBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 20;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
