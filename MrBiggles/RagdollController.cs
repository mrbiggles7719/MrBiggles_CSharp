using UnityEngine;
using UnityEngine.AI;


namespace mrbiggles
{
    public class RagdollController : MonoBehaviour
    {
        private Animator[] animators;
        private Rigidbody[] rigidbodies;
        private Collider[] colliders;
        private NavMeshAgent navMeshAgent;

        [Tooltip("Automatically disable all Animator components when enabling the ragdoll.")]
        [SerializeField] private bool autoDisableAnimator = true;

        [Tooltip("Apply NavMeshAgent's velocity to the ragdoll's rigidbodies.")]
        [SerializeField] private bool applyMomentum = true;

        [Tooltip("Multipler for velocities applied from NavMeshAgent")]
        [SerializeField] private float velocityMultiplier = 1f;

        void Start()
        {
            animators = GetComponentsInChildren<Animator>();
            rigidbodies = GetComponentsInChildren<Rigidbody>();
            colliders = GetComponentsInChildren<Collider>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            // Initially disable the ragdoll effect
            SetRagdollState(false);
        }

        public void SetRagdollState(bool state)
        {
            // Enable/Disable all Animators based on the autoDisableAnimator flag
            if (autoDisableAnimator)
            {
                foreach (var animator in animators)
                {
                    animator.enabled = !state;
                }
            }

            // Apply momentum from NavMeshAgent (if enabled and agent exists)
            if (applyMomentum && navMeshAgent != null && state)
            {
                ApplyMomentum(navMeshAgent.velocity);
            }

            // Enable/Disable Rigidbodies and Colliders for the ragdoll
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = !state;
            }

            foreach (var col in colliders)
            {
                col.enabled = state;
            }

            // Assuming the main collider and Rigidbody of the character should have the opposite state
            GetComponent<Collider>().enabled = !state;
            GetComponent<Rigidbody>().isKinematic = state;
        }

        private void ApplyMomentum(Vector3 velocity)
        {
            foreach (var rb in rigidbodies)
            {
                rb.velocity = velocity * velocityMultiplier;
            }
        }

        // Call this method to enable the ragdoll
        public void EnableRagdoll()
        {
            SetRagdollState(true);
        }

        // Call this method to disable the ragdoll and return to the animated state
        public void DisableRagdoll()
        {
            SetRagdollState(false);
        }
    }
}