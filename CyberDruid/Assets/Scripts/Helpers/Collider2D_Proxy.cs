using System;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    /// <summary>
    /// Allows you to subscribe to the events OnPlayerStayInCollider and OnPlayerExitCollider for multiple colliders in the same game object.
    /// </summary>
    /// <remarks>
    /// From https://discussions.unity.com/t/having-more-than-one-collider-in-a-gameobject/32067/9
    /// </remarks>
    public class Collider2D_Proxy : MonoBehaviour
    {
        public Action<Collision2D> OnCollisionEnter2D_Action;
        public Action<Collider2D> OnPlayerStayInCollider;
        public Action<Collider2D> OnPlayerExitCollider;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Player")
            {
                OnPlayerStayInCollider?.Invoke(collider);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Player")
            {
                OnPlayerStayInCollider?.Invoke(collider);
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Player")
            {
                OnPlayerExitCollider?.Invoke(collider);
            }
        }
    }
}
