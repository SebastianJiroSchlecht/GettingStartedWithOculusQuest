using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    public float recoil = 1.0f;

    public Transform barrel = null;
    public GameObject projectilePrefab = null;


    private XRGrabInteractable interactable = null;
    private Rigidbody rigidbody = null;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        interactable.onActivate.AddListener(Fire);
    }

    private void OnDisable()
    {
        interactable.onActivate.RemoveListener(Fire);
    }

    private void Fire(XRBaseInteractor interactor)
    {
        CreateProjectile();
        ApplyRecoil();
    }

    private void CreateProjectile()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch();
    }

    private void ApplyRecoil()
    {
        rigidbody.AddRelativeForce(Vector3.right * recoil, ForceMode.Impulse);
    }
}
