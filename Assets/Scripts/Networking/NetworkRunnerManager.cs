using Fusion;
using System.Threading.Tasks;
using UnityEngine;
using Fusion.Sockets;
using UnityEngine.SceneManagement;

public class NetworkRunnerManager : MonoBehaviour
{
    private NetworkRunner networkRunner;

    private void Start()
    {
        networkRunner = gameObject.GetComponent<NetworkRunner>();
        InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, "Fusion Room", NetAddress.Any());
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, string sessionName, NetAddress netAddress)
    {
        networkRunner.ProvideInput = true;
        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            SessionName = sessionName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            Address = netAddress
        });
    }
}
