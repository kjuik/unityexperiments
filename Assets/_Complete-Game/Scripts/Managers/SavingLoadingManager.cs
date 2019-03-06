using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
    public class SavingLoadingManager : AbstractSavingLoadingManager<CurrentGameSave>
    {
        [SerializeField]
        private Transform PlayerTransform;
        [SerializeField]
        private Transform CameraTransform;

        protected override void ApplyGameSave(CurrentGameSave save)
        {
            PlayerTransform.position = save.PlayerTransform.Position;
            PlayerTransform.rotation = save.PlayerTransform.Rotation;

            CameraTransform.position = save.CameraTransform.Position;
            CameraTransform.rotation = save.CameraTransform.Rotation;
        }

        protected override CurrentGameSave CreateGameSave()
        {
            var save = new CurrentGameSave();

            save.PlayerTransform.Position = PlayerTransform.position;
            save.PlayerTransform.Rotation = PlayerTransform.rotation;

            save.CameraTransform.Position = CameraTransform.position;
            save.CameraTransform.Rotation = CameraTransform.rotation;

            return save;
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