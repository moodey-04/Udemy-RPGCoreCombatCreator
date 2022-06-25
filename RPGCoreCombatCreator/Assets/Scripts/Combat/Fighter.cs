using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2;
        [SerializeField] float timeBetweenAttack = 1;
        float timeSinceLastAttack = Mathf.Infinity;

        [SerializeField] float weaponDamage = 5;

        Health target;
        Mover mover = null;
        ActionScheduler actionScheduler = null;
        Animator animator = null;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            //if target die stop attack
            if (target.IsDead())
                return;

            //move to target
            if (!GetIsInRange())
                mover.MoveTo(target.transform.position);
            else //distance and atk target
            {
                AttackBehaviour();
                mover.Cancel();
            }
        }

        //attack
        private void AttackBehaviour()
        {
            this.transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                //This will trigger the Hit() events
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("cancelAttack");
            animator.SetTrigger("attack");
        }

        public bool IsCanAttack(GameObject target)
        {
            if (target == null)
                return false;

            Health targetHealth = target.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead();
        }

        //Animation Event
        void Hit()
        {
            if (target != null)
                target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        //set target
        public void Attack(GameObject target)
        {
            actionScheduler.StartAction(this);
            this.target = target.GetComponent<Health>();
        }

        //cancel attack IAction
        public void Cancel()
        {
            TriggerCancelAttack();
            target = null;
        }

        private void TriggerCancelAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("cancelAttack");
        }


    }
}