using System;

namespace CompleteProject
{
    [Serializable]
    public class CompleteGameState : GameState
    {
        public int Score;

        public PlayerState Player;
        public CameraState Camera;

        public EnemyManagerState[] EnemyManagers;
    }
}
