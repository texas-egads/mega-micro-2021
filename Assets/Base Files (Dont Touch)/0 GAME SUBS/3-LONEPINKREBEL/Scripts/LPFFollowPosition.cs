using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LONEPINKFROG
{
    public class LPFFollowPosition : MonoBehaviour
    {

        //This script is attached to the smoke particle, and causes it to follow the position of the player character.
        //The smoke particle is not a child or component of the player character, because if it were, it would also follow the player's rotation. The particle should ALWAYS move upwards.

        public Transform followPosition;
        void Update(){gameObject.transform.position = followPosition.position;}
    }
}
