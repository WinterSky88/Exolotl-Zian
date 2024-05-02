using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
    [AddComponentMenu("Corgi Engine/Character/AI/Decisions/AI Decision Detect Wall")]
    public class AIDecisionDetectWall : AIDecision
    {
        [Tooltip("Whether to use a custom layermask, or simply use the platform mask defined at the character level")]
        public bool UseCustomLayermask = false;
        /// if using a custom layer mask, the list of layers considered as obstacles by this AI
        [Tooltip("if using a custom layer mask, the list of layers considered as obstacles by this AI")]
        [MMCondition("UseCustomLayermask", true)]
        public LayerMask ObstaclesLayermask = LayerManager.ObstaclesLayerMask;
        /// the length of the horizontal raycast we should use to detect obstacles that may cause a direction change
		[Tooltip("the length of the horizontal raycast we should use to detect obstacles that may cause a direction change")]
        [MMCondition("UseCustomLayermask", true)]
        public float ObstaclesDetectionRaycastLength = 0.5f;
        /// the origin of the raycast (if casting against the same layer this object is on, the origin should be outside its collider, typically in front of it)
        [Tooltip("the origin of the raycast (if casting against the same layer this object is on, the origin should be outside its collider, typically in front of it)")]
        [MMCondition("UseCustomLayermask", true)]
        public Vector2 ObstaclesDetectionRaycastOrigin = new Vector2(0.5f, 0f);

        // private stuff
        protected CorgiController _controller;
        protected Character _character;
        protected Health _health;
        protected CharacterHorizontalMovement _characterHorizontalMovement;
        protected Vector2 _direction;
        protected Vector2 _startPosition;
        protected Vector2 _initialDirection;
        protected Vector3 _initialScale;
        protected float _distanceToTarget;
        protected Vector2 _raycastOrigin;
        protected RaycastHit2D _raycastHit2D;
        protected Vector2 _obstacleDirection;

        /// <summary>
        /// On init we grab all the components we'll need
        /// </summary>
        public override void Initialization()
        {
            // we get the CorgiController2D component
            _controller = GetComponentInParent<CorgiController>();
            _character = GetComponentInParent<Character>();
            _characterHorizontalMovement = _character?.FindAbility<CharacterHorizontalMovement>();
            _health = _character.CharacterHealth;
            // initialize the start position
            _startPosition = transform.position;
            // initialize the direction
            _direction = _character.IsFacingRight ? Vector2.right : Vector2.left;

            _initialDirection = _direction;
            _initialScale = transform.localScale;
        }

        public override bool Decide()
        {
            return CheckForWalls();
        }

        protected bool CheckForWalls()
        {
            if (UseCustomLayermask)
            {
                return DetectObstaclesCustomLayermask();
            }
            else
            {
                return DetectObstaclesRegularLayermask();
            }
        }

        protected bool DetectObstaclesRegularLayermask()
        {
            return (_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight);
        }

        /// <summary>
        /// Returns true if an obstacle is in front of the character, using a custom layer mask
        /// </summary>
        /// <returns></returns>
        protected bool DetectObstaclesCustomLayermask()
        {
            if (_character.IsFacingRight)
            {
                _raycastOrigin = transform.position + (_controller.Bounds.x / 2 + ObstaclesDetectionRaycastOrigin.x) * transform.right + ObstaclesDetectionRaycastOrigin.y * transform.up;
                _obstacleDirection = Vector2.right;
            }
            else
            {
                _raycastOrigin = transform.position - (_controller.Bounds.x / 2 + ObstaclesDetectionRaycastOrigin.x) * transform.right + ObstaclesDetectionRaycastOrigin.y * transform.up;
                _obstacleDirection = Vector2.left;
            }

            _raycastHit2D = MMDebug.RayCast(_raycastOrigin, _obstacleDirection, ObstaclesDetectionRaycastLength, ObstaclesLayermask, Color.gray, true);

            return _raycastHit2D;
        }
    }
}