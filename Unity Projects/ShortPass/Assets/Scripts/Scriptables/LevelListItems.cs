[System.Serializable]
public class LevelListItems
{
    public string name;
    public BallConfigScriptable Ball;
    public FriendConfigScriptable FriendList;
    public EnemyConfigScriptable EnemyList;
    public ArrowConfigScriptable Arrow;

    private LevelListItems(string newName, BallConfigScriptable newBall,ArrowConfigScriptable newArrow, FriendConfigScriptable newFriendList, EnemyConfigScriptable newEnemyList)
    {
        name = newName;
        Ball = newBall;
        Arrow = newArrow;
        FriendList = newFriendList;
        EnemyList = newEnemyList;
    }
    
    public BallConfigScriptable BallOfLevel
    {
        get => Ball;
        set => Ball = value;
    }

    public ArrowConfigScriptable ArrowOfLevel
    {
        get => Arrow;
        set => Arrow = value;
    }

    public FriendConfigScriptable FriendListOfLevel
    {
        get => FriendList;
        set => FriendList = value;
    }

    public EnemyConfigScriptable EnemyListOfLevel
    {
        get => EnemyList;
        set => EnemyList = value;
    }
    
}
