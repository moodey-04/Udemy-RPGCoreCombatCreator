using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover = null;
        Fighter fighter = null;

        private void Awake() {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if(InteractWithCombat()) 
                return;

            if(InteractWithMovement())
                return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target==null)
                    continue;

                if(!fighter.IsCanAttack(target.gameObject))
                    continue;

                if(Input.GetMouseButtonDown(0))
                    fighter.Attack(target.gameObject);

                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                // Debug.DrawRay(ray.origin, ray.direction * 100);
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }

                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
