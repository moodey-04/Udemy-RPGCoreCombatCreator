using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navmeshAgent = null;
        Animator animator = null;
        ActionScheduler actionScheduler = null;

        private void Awake() {
            navmeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        //start movement
        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        //move to mouse point
        public void MoveTo(Vector3 destination)
        {
            navmeshAgent.destination = destination;
            navmeshAgent.isStopped = false;
        }
        
        //cancel movement IAction
        public void Cancel()
        {
            navmeshAgent.isStopped = true;
        }

        //movement animation
        void UpdateAnimator()
        {
            Vector3 velocity = navmeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }

}
