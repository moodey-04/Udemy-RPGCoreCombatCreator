using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover = null;
        Fighter fighter = null;
        Health health = null;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
                return;

            if (InteractWithCombat())
                return;

            if (InteractWithMovement())
                return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                GameObject target = hit.transform.gameObject; //combat target
                if (target == null)
                    continue;

                if (target == this.gameObject)
                    continue;

                if (!fighter.IsCanAttack(target))
                    continue;

                if (Input.GetMouseButtonDown(0))
                    fighter.Attack(target);

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
