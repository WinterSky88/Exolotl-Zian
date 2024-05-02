using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{
    /// <summary>
    /// This action directs the CharacterHorizontalMovement ability to move in the direction of the target.
    /// </summary>
    [AddComponentMenu("Corgi Engine/Character/AI/Actions/AI Action Move")]
    // [RequireComponent(typeof(CharacterHorizontalMovement))]
    public class AIActionMove : AIAction
    {
        protected Character _character;
        protected CharacterHorizontalMovement _characterHorizontalMovement;
        protected Vector2 _initialDirection;

        public enum SetDirections { Default, Right, Left, FaceTarget, FaceAwayFromTarget}
        public SetDirections setDirection;

        /// <summary>
        /// On init we grab our CharacterHorizontalMovement ability
        /// </summary>
        public override void Initialization()
        {
            _character = GetComponentInParent<Character>();
            _characterHorizontalMovement = _character?.FindAbility<CharacterHorizontalMovement>();
        }

        /// <summary>
        /// On PerformAction we move
        /// </summary>
        public override void PerformAction()
        {
            Move();
        }

        /// <summary>
        /// Moves the character in the decided direction
        /// </summary>
        protected virtual void Move()
        {
            if (_brain.Target == null)
            {
                _characterHorizontalMovement.SetHorizontalMove(0f);
                return;
            }

            _characterHorizontalMovement.SetHorizontalMove(_initialDirection.x);
        }

        protected virtual void SetInitialDirection()
        {
            if (setDirection == SetDirections.Default)
            {
                _initialDirection = _character.IsFacingRight ? Vector2.right : Vector2.left;
                return;
            }
            if (setDirection == SetDirections.Right)
            {
                _initialDirection = Vector2.right;
                return;
            }
            if (setDirection == SetDirections.Left)
            {
                _initialDirection = Vector2.left;
                return;
            }

            if (_brain.Target == null) return;

            if (setDirection == SetDirections.FaceTarget)
            {
                if (this.transform.position.x < _brain.Target.position.x)
                {
                    _initialDirection = Vector2.right;
                }
                else
                {
                    _initialDirection = Vector2.left;
                }
                return;
            }
            if (setDirection == SetDirections.FaceAwayFromTarget)
            {
                if (this.transform.position.x < _brain.Target.position.x)
                {
                    _initialDirection = Vector2.left;
                }
                else
                {
                    _initialDirection = Vector2.right;
                }
                return;
            }
        }

        /// <summary>
        /// When entering the state we reset our movement.
        /// </summary>
        public override void OnEnterState()
        {
            base.OnEnterState();
            if (_characterHorizontalMovement == null)
            {
                Initialization();
            }
            _characterHorizontalMovement.SetHorizontalMove(0f);

            SetInitialDirection();
        }

        /// <summary>
        /// When exiting the state we reset our movement.
        /// </summary>
        public override void OnExitState()
        {
            base.OnExitState();
            _characterHorizontalMovement?.SetHorizontalMove(0f);
        }
    }
}