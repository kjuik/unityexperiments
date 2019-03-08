using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompleteProject
{
    [System.Serializable]
    public class EnemyManagerState : ObjectState
    {
        public string PrefabName;
        public EnemyState[] EnemyInstances;
    }

    public class EnemyManager : MonoBehaviour, ISaveable<EnemyManagerState>
    {
        public PlayerHealth playerHealth;       // Reference to the player's heatlh.
        public EnemyHealth enemyPrefab;         // The enemy prefab to be spawned.
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

        [HideInInspector]
        public List<EnemyHealth> Instances = new List<EnemyHealth>();

        void Start ()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating ("Spawn", spawnTime, spawnTime);
        }


        void Spawn ()
        {
            // If the player has no health left...
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create and save an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            SpawnEnemy(spawnPoints[spawnPointIndex].position,
                       spawnPoints[spawnPointIndex].rotation);
        }

        private EnemyHealth SpawnEnemy(Vector3 position, Quaternion rotation)
        {
            var spawnedEnemy = Instantiate(enemyPrefab, position, rotation);
            spawnedEnemy.OnDeath += OnEnemyDied;
            Instances.Add(spawnedEnemy);
            return spawnedEnemy;
        }

        void OnEnemyDied(EnemyHealth enemy) =>
            Instances.Remove(enemy);

        public EnemyManagerState Save() =>
            new EnemyManagerState()
            {
                PrefabName = enemyPrefab.name,
                EnemyInstances = Instances.Select(x => x.Save()).ToArray()
            };

        public void Load(EnemyManagerState save)
        {
            foreach (var enemy in Instances)
                Destroy(enemy.gameObject);
            Instances.Clear();

            foreach(var enemy in save.EnemyInstances)
            {
                var spawned = SpawnEnemy(enemy.Position, enemy.Rotation);
                spawned.currentHealth = enemy.Health;
            }
        }
    }
}