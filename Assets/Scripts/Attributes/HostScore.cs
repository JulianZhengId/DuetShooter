using Fusion;
using UnityEngine;
using TMPro;

public class HostScore : NetworkBehaviour
{
    public static HostScore instance;
    [Networked(OnChanged = nameof(OnScoreChanged))] private int points { get; set; } = 0;

    private TextMeshProUGUI pointsText;

    private void Awake()
    {
        instance = this;
        pointsText = GameObject.Find("Points Text Player 1").GetComponent<TextMeshProUGUI>();
    }

    public static void OnScoreChanged(Changed<HostScore> changed)
    {
        //edit score ui
        changed.Behaviour.pointsText.text = changed.Behaviour.points.ToString();
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }
}
