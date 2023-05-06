using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject healthPlayer2;
    [SerializeField] private GameObject healthBoss;

    private void Awake()
    {
        instance = this;
    }

    public void TurnOnPlayer2UI()
    {
        healthPlayer2.transform.localScale = Vector3.one;
    }
}
