using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class SavingLoadingManager : AbstractSavingLoadingManager<CurrentGameState>
    {
        [SerializeField]
        private PlayerHealth Player;
        [SerializeField]
        private CameraFollow Camera;

        protected override void ApplyGameState(CurrentGameState save)
        {
            Player.Load(save.Player);
            Camera.Load(save.Camera);
        }

        protected override CurrentGameState GenerateGameState()
        {
            return new CurrentGameState() {
                Player = Player.Save(),
                Camera = Camera.Save()
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