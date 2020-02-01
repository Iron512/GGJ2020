using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliciesManager : MonoBehaviour
{
    Policy CurrentPolicy = null;
    public void onPolicyChange(Policy NewPolicy)
    {
        if (CurrentPolicy != null)
        {
            CurrentPolicy.onPolicyEnd();
        }
        NewPolicy.onPolicyStart();
    }
}
