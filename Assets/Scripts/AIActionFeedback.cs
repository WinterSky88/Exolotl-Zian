using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.CorgiEngine
{
    [AddComponentMenu("Corgi Engine/Character/AI/Actions/AI Action Feedback")]
    public class AIActionFeedback : AIAction
    {
        [Tooltip("the feedbacks to play when the action starts")]
        public MMFeedbacks ActionStartFeedbacks;
        
        public override void Initialization()
        {
            
        }

        public override void PerformAction()
        {
            
        }
        
        public override void OnEnterState()
        {
            base.OnEnterState();
            ActionStartFeedbacks.PlayFeedbacks();
        }
        
        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}