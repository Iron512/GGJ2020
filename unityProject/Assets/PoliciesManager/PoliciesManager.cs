using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliciesManager : MonoBehaviour
{
    Policy CurrentPolicy;

    public void onPolicyChange(Policy NewPolicy)
    {
        CurrentPolicy.onPolicyEnd();
        NewPolicy.onPolicyStart();
    }
}
