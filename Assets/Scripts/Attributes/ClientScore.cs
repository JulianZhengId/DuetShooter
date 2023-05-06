using Fusion;
using UnityEngine;
using TMPro;

public class ClientScore : NetworkBehaviour
{
    public static ClientScore instance;
    [Networked(OnChanged = nameof(OnScoreChanged))] private int points { get; set; } = 0;

    private TextMeshProUGUI pointsText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pointsText = GameObject.Find("Points Text Player 2").GetComponent<TextMeshProUGUI>();
    }

    public static void OnScoreChanged(Changed<ClientScore> changed)
    {
        //edit score ui
        changed.Behaviour.pointsText.text = changed.Behaviour.points.ToString();
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }
}
