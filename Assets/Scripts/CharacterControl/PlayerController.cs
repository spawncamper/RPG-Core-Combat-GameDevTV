using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        //   CharacterMovement characterMovement;
        Fighter fighter;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (AttackOnClick()) return;   // InteractWithCombat()   
            if (MoveOnClick()) return;  // InteractWithMovement()
        }

        private bool AttackOnClick() 
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool MoveOnClick()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    GetComponent<CharacterMovement>().StartMovement(hit.point);  //StartMoveAction()
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