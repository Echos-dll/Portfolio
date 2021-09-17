using UnityEngine;

[CreateAssetMenu(fileName = "Ball Configs", menuName = "Game Configs/Ball Configs")]
public class BallConfigScriptable : ScriptableObject
{
    [SerializeField]
    private BallConfigListItems BallConfig;

    public BallConfigListItems BallConfigGetter
    {
        get => BallConfig;
        set => BallConfig = value;
    }
}
