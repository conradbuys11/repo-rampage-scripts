using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HURTBOX STATES
public class HurtboxState : FSGDN.StateMachine.State
{
    protected Hurtbox HurtboxMachine()
    {
        return (Hurtbox)machine;
    }
}

public class HurtboxOpenState : HurtboxState
{
    public override void Enter()
    {
        base.Enter();
        HurtboxMachine().ChangeGizmoColor(HurtboxMachine().openColor);
    }
}

public class HurtboxHitState : HurtboxState
{
    public override void Enter()
    {
        base.Enter();
        HurtboxMachine().ChangeGizmoColor(HurtboxMachine().hitColor);
    }
}



//CHARACTER (PUSHBOX) STATES
public class CharacterState : FSGDN.StateMachine.State
{
    protected PlayerPushbox CharacterMachine()
    {
        return (PlayerPushbox)machine;
    }

    protected EnemyBase EnemyMachine()
    {
        return (EnemyBase)machine;
    }
}

public class CharacterIdleState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        CharacterMachine().ResetAnimTriggers();
    }

    public override void Execute()
    {
        base.Execute();
        CharacterMachine().Idle();
    }
}

public class CharacterStunnedState : CharacterState
{

}

public class CharacterRecoilState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        CharacterMachine().myHurtbox.ChangeState<HurtboxHitState>();
        CharacterMachine().DisableAllHitboxes();
        CharacterMachine().ResetAnimTriggers();
    }

    public override void Exit()
    {
        base.Exit();
        CharacterMachine().myHurtbox.ChangeState<HurtboxOpenState>();
    }

}

public class CharacterAttackingState : CharacterState
{
    public override void Execute()
    {
        base.Execute();
        CharacterMachine().Attacking();
    }
    public override void Exit()
    {
        base.Exit();
        CharacterMachine().ResetAnimTriggers();
    }
}

public class CharacterJumpState : CharacterState
{

}

public class CharacterBlockState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        CharacterMachine().myHurtbox.ChangeState<HurtboxHitState>();
    }
    //public override void Execute()
    //{
    //    base.Execute();
    //    CharacterMachine().Block();
    //}
    public override void Exit()
    {
        base.Exit();
        CharacterMachine().myHurtbox.ChangeState<HurtboxOpenState>();
    }
}

public class CharacterMovementState : CharacterState
{

}


public class PlayerKOState : CharacterState
{
    
}



public class EnemyIdleState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        EnemyMachine().myRenderer.material.color = EnemyMachine().idleColor;
    }
    public override void Execute()
    {
        base.Execute();
        EnemyMachine().MoveTowardsIdleLocation();
    }
}

public class EnemyRecoilState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        EnemyMachine().myRenderer.material.color = EnemyMachine().recoilStunColor;
        EnemyMachine().myHurtbox.ChangeState<HurtboxHitState>();
    }

    public override void Exit()
    {
        base.Exit();
        EnemyMachine().myHurtbox.ChangeState<HurtboxOpenState>();
    }
}

public class EnemyKOState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        EnemyMachine().myRenderer.material.color = EnemyMachine().deadColor;
        EnemyMachine().myHurtbox.ChangeState<HurtboxHitState>();
    }
}

public class EnemyMovementState : CharacterState
{

}

public class EnemyAttackingState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        EnemyMachine().myRenderer.material.color = EnemyMachine().prepAttackColor;
    }
}

public class EnemyBlockState : CharacterState
{

    
}

public class EnemyPursueState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        EnemyMachine().myRenderer.material.color = EnemyMachine().pursueColor;
    }
    public override void Execute()
    {
        base.Execute();
        EnemyMachine().MoveTowardsPlayer();
    }
}

public class EnemyStunnedState : CharacterState
{
    public override void Enter()
    {
        base.Enter();
        EnemyMachine().myRenderer.material.color = EnemyMachine().recoilStunColor;
    }
}
