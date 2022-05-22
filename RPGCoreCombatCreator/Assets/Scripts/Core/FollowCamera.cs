using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] GameObject _player = null;

        private void LateUpdate()
        {
            this.transform.position = _player.transform.position;
        }

    }
}



    
