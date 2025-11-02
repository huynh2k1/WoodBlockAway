using UnityEngine;

public class HingeSetup : MonoBehaviour
{

    public void ConfigureHingeJoint(HingeJoint hinge, Rigidbody connectedBody, Vector3 anchorPoint)
    {
        hinge.connectedBody = connectedBody;
        hinge.anchor = transform.InverseTransformPoint(anchorPoint);
        hinge.axis = Vector3.forward; // Trục xoay

        // Cấu hình giới hạn mềm (soft limits)
        JointLimits limits = new JointLimits();
        limits.min = -90f;
        limits.max = 90f;
        limits.bounciness = 0.1f;
        limits.bounceMinVelocity = 0.1f;
        hinge.limits = limits;
        hinge.useLimits = true;

        // Spring để có cảm giác "snappy"
        JointSpring spring = new JointSpring();
        spring.spring = 5f;
        spring.damper = 1f;
        hinge.spring = spring;
        hinge.useSpring = true;
    }
}
