using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/**
 * This component instantiates a given prefab at random time intervals and random bias from its object position.
 */
public class RandomSpawner: MonoBehaviour {
    [SerializeField] Mover prefabToSpawn;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject Player;
    [Tooltip("Minimum time between consecutive spawns, in seconds")] [SerializeField] float minTimeBetweenSpawns = 0f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")] [SerializeField] float maxTimeBetweenSpawns = 0f;
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")] [SerializeField] float maxXDistance = 8f;

    void Start() {
         this.StartCoroutine(SpawnRoutine());    // co-routines
        // _ = SpawnRoutine();                   // async-await
    }
//dd
    IEnumerator SpawnRoutine() {    // co-routines
    // async Task SpawnRoutine() {  // async-await
        while (true) {
            Player = GameObject.Find("PlayerSpaceship");
            shield = GameObject.Find("Shield");
            GameObject shieldSpawner = GameObject.Find("ShieldSpawn(Clone)");
            var destroyComponent = Player.GetComponent<DestroyOnTrigger2D>();
            if(destroyComponent.enabled == false && shield == null){
                yield return new WaitUntil(() => destroyComponent.enabled);
            }
            if(shield == null && shieldSpawner == null){
                Debug.Log("Shield Was Created!");
                float timeBetweenSpawnsInSeconds = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
                yield return new WaitForSeconds(timeBetweenSpawnsInSeconds);       // co-routines
                // await Task.Delay((int)(timeBetweenSpawnsInSeconds*1000));       // async-await
                Vector3 positionOfSpawnedObject = new Vector3(
                    transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                    transform.position.y,
                    transform.position.z);
                GameObject newObject = Instantiate(prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
            }else{
                yield return new WaitUntil(() => (shieldSpawner = GameObject.Find("ShieldSpawn(Clone)"))==null);
            }
        }
    }
}
