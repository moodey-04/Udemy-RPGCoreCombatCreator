using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float hp = 20f;
        bool isDeath = false;

        public bool IsDead()
        {
            return isDeath;
        }

        public void TakeDamage(float damage)
        {
            hp = Mathf.Max(hp - damage, 0);

            if(hp == 0)
                Die();
        }

        private void Die()
        {
            if (isDeath)
                return;

            isDeath = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
