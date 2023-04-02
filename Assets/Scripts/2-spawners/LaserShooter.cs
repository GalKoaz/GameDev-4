using UnityEngine;

/**
 * This component spawns the given laser-prefab whenever the player clicks a given key.
 * It also updates the "scoreText" field of the new laser.
 */
public class LaserShooter : ClickSpawner
{
    [SerializeField] NumberField scoreField;
    [SerializeField] GameObject BulletStart;
    [SerializeField] float fireDelay = 0.5f;

    private float timeSinceLastShot;

    protected override GameObject spawnObject()
    {
        if (Time.time - timeSinceLastShot < fireDelay) {
            // The fire delay has not yet expired, so don't fire another shot.
            return null;
        }

        GameObject newObject = base.spawnObject();
        if (newObject != null) {
            // Only update the timeSinceLastShot if a new object was actually spawned.
            timeSinceLastShot = Time.time;
            BulletStart = GameObject.Find("BulletStart");
            newObject.transform.position = BulletStart.transform.position;

            ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
            if (newObjectScoreAdder)
                newObjectScoreAdder.SetScoreField(scoreField);
        }
        return newObject;
    }
}
