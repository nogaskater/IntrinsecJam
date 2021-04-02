using UnityEngine;

public class MatchManagerInitializer : MonoBehaviour
{
    private void Awake()
    {
        MatchManager.Instance.StartMatch();
    }
}
