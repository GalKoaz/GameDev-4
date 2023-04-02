using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component destroys its object whenever it triggers a 2D collider with the given tag.
 */
public class DestroyOnTrigger2D : MonoBehaviour {
    [Tooltip("Every object tagged with this tag will trigger the destruction of this object")]
    [SerializeField] string triggeringTag;

    Animator animator;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == triggeringTag && enabled) {


            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
    void Start(){
        animator = GetComponent<Animator>();
    }
    public void StartExplosion(){
        if (animator == null)
            animator = GetComponent<Animator>();
        animator.SetBool("expl", true);
    }

    private void Update() {
        /* Just to show the enabled checkbox in Editor */
    }
}
