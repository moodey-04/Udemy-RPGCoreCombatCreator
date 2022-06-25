using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5;

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        Vector3 guardPosition;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();
            guardPosition = this.transform.position;
        }


        void Update()
        {
            if (health.IsDead())
                return;

            if (IsAttackRangeOfPlayer() && fighter.IsCanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                mover.StartMoveAction(guardPosition);
            }
        }

        bool IsAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        bool IsAttackRangeOfPlayerGreater()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
            return distanceToPlayer > chaseDistance;
        }

        //called by unity
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

