using UnityEngine;

public class VehicleMovement
{
    private Rigidbody subjectRigidbody;
    private Transform subjectTransform;

    public VehicleMovement(Rigidbody subjectRigidbody, Transform subjectTransform)
    {
        this.subjectRigidbody = subjectRigidbody;
        this.subjectTransform = subjectTransform;
    }

    public void Update(float rotationInput, float moveSpeed, float rotationSpeed)
    {
        // Движение вперед и поворот

        subjectRigidbody.MovePosition(subjectRigidbody.position + subjectTransform.forward * moveSpeed * Time.deltaTime);
        Vector3 euler = Vector3.up * rotationInput * rotationSpeed * Time.deltaTime;
        Quaternion rhs = Quaternion.Euler(euler);
        Quaternion b = subjectRigidbody.rotation * rhs;
        subjectRigidbody.MoveRotation(Quaternion.Slerp(subjectRigidbody.rotation, b, 50f * Time.deltaTime));
    }
}
