using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtension
{
    /// <summary>
    /// <para/> 리지드바디에 가해진 모든 힘 초기화
    /// </summary>
    public static void Ex_ResetAllForces(this Rigidbody rb)
    {
        var constraints = rb.constraints;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = constraints;
    }
}
