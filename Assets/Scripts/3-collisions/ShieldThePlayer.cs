using System.Collections;
using UnityEngine;

public class ShieldThePlayer : MonoBehaviour {
    [Tooltip("The number of seconds that the shield remains active")] [SerializeField] float duration;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject temp;
    [SerializeField] GameObject player;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material material;

    void Start() {
        temp = GameObject.Find("preShield");
        meshRenderer = temp.GetComponent<MeshRenderer>();
        if (meshRenderer != null) {
            material = temp.GetComponent<MeshRenderer>().material;
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Debug.Log("Shield triggered by player");
            var destroyComponent = other.GetComponent<DestroyOnTrigger2D>();
            if (destroyComponent) {
                destroyComponent.StartCoroutine(ShieldTemporarily(destroyComponent));        // co-routines
                // NOTE: If you just call "StartCoroutine", then it will not work, 
                //       since the present object is destroyed!
                // ShieldTemporarily(destroyComponent);                                      // async-await
                Destroy(gameObject);  // Destroy the shield itself - prevent double-use
            }
        } else {
            Debug.Log("Shield triggered by "+other.name);
        }
    }

    private IEnumerator ShieldTemporarily(DestroyOnTrigger2D destroyComponent) {   // co-routines
        // private async void ShieldTemporarily(DestroyOnTrigger2D destroyComponent) {      // async-await
        destroyComponent.enabled = false;
        player = GameObject.Find("PlayerSpaceship");
        temp.GetComponent<MeshRenderer>().enabled = true;   // Enable the Mesh Renderer component of preShield
        var playerspeed = player.GetComponent<InputMover>();
        Color pre = material.color;
        float t = 1f;
        for (float i = duration; i > 0; i--) {
            t -=0.2f;
            material.SetColor("_Color", new Color(pre.r, pre.g, pre.b, t));
            Debug.Log("Shield: " + i + " seconds remaining!");
            yield return new WaitForSeconds(1);       // co-routines
            // await Task.Delay(1000);                // async-await
        }
        temp.GetComponent<MeshRenderer>().enabled = false;   // Enable the Mesh Renderer component of preShield
        //Destroy(shield);
        Debug.Log("Shield gone!");
        destroyComponent.enabled = true;
    }
}
