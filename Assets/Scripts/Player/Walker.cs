using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [Tooltip("FR, RL, FL, RR")]
    [SerializeField] private WalkerLeg[] _walkerLegs;
    

    private void Update()
    {
        if (GetPriority())
        {
            if (!TryStep(2, 0))
                TryStep(0, 2);
        }
        else
        {
            if (!TryStep(0, 2))
                TryStep(2, 0);
        }

    }

    private bool GetPriority()
    {
        return (Mathf.Max(_walkerLegs[0].LegError, _walkerLegs[1].LegError) > Mathf.Max(_walkerLegs[2].LegError, _walkerLegs[3].LegError));
    }

    private bool TryStep(int shiftOne, int shiftTwo)
    {
        if (AllGrounded(shiftOne) && NeedStep(shiftTwo))
        {
            StartStep(shiftTwo);
            return true;
        }

        return false;
    }

    private void StartStep(int shift)
    {
        _walkerLegs[shift].StartNewStepAction();
        _walkerLegs[shift + 1].StartNewStepAction();
    }

    private bool NeedStep(int shift)
    {
        return (_walkerLegs[shift].NeedStep || _walkerLegs[shift + 1].NeedStep);
    }

    private bool AllGrounded(int shift)
    {
        return (_walkerLegs[shift].Grounded && _walkerLegs[shift + 1].Grounded);
    }


}
