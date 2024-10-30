using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Minigame")]
public class Minigame : ScriptableObject
{
    [SerializeField] public int beatLength;
    [SerializeField] public float maxHitMargin;
    [SerializeField] private Lane lane1;
    [SerializeField] private Lane lane2;
    [SerializeField] private Lane lane3;
    [SerializeField] private Lane lane4;

    private PlayerMove linkedMove;

    public void StartMinigame(PlayerMove callback)
    {
        linkedMove = callback;
        AudioManager.Instance.Activate(beatLength, maxHitMargin, lane1, lane2, lane3, lane4);
        AudioManager.Instance.OnSessionFinished += MinigameFinished;
    }

    public void MinigameFinished(object sender, float score)
    {
        linkedMove.MinigameFinished(score);
    }
}