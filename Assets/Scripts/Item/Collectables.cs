using Fusion;
using UnityEngine;

public class Collectables : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }

    public void Init()
    {
        life = TickTimer.CreateFromSeconds(Runner, 50f);
    }
}
