using UnityEngine;
using System.Linq;

namespace CompleteProject
{
    public class SavingLoadingManager : BaseSavingLoadingManager<CompleteGameState>
    {
        [SerializeField]
        private PlayerHealth Player;
        [SerializeField]
        private CameraFollow Camera;
        [SerializeField]
        private EnemyManager[] EnemyManagers;

        protected override void ApplyGameState(CompleteGameState state)
        {
            if (state == null)
                return;

            ScoreManager.score = state.Score;
            Player.Load(state.Player);
            Camera.Load(state.Camera);
            foreach(var manager in EnemyManagers)
                manager.Load(state.EnemyManagers
                    .Where(x => x.PrefabName == manager.enemyPrefab.name)
                    .First());
        }

        protected override CompleteGameState GenerateGameState()
        {
            return new CompleteGameState() {
                Score = ScoreManager.score,
                Player = Player.Save(),
                Camera = Camera.Save(),
                EnemyManagers = EnemyManagers.Select(x => x.Save()).ToArray()
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
                SaveGame();

            if (Input.GetKeyDown(KeyCode.F9))
                LoadGame();
        }
    }
}