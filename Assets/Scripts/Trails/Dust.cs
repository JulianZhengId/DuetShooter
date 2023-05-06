using UnityEngine;

public class Dust : MonoBehaviour
{
    [SerializeField] private float xMovement;

    private void Update()
    {
        transform.localPosition += new Vector3(xMovement, 0, 0);
    }

    public void SetInactive()
    {
        this.gameObject.SetActive(false);
    }
}