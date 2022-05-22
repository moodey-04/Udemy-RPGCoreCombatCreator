using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5;

        Fighter fighter;
        GameObject player;
       
        void Start()
        {
            fighter = GetComponent<Fighter>();
             player = GameObject.FindWithTag("Player");
        }
       

        void Update()
        {
            if(IsAttackRangeOfPlayer() && fighter.IsCanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        bool IsAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position,this.transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}

