using TMPro;
using UnityEngine;

public class Rotation_Weapon : MonoBehaviour
{
    public GameObject weapons;
    public TextMeshPro weaponsName;
    int count = 0;

    Animator animator;

    public ParticleSystem[] effect;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Invoke("StartAnimation", 3.0f);
        effect[0].Play();
        effect[1].Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        weapons.transform.Rotate(Vector3.up * Time.deltaTime * 60);
    }

    void ChangeWeapons()
    {
        weapons.transform.GetChild(count).gameObject.SetActive(false);

        count++;

        weapons.transform.GetChild(count).gameObject.SetActive(true);
        weaponsName.text = weapons.transform.GetChild(count).gameObject.name;
    }

    void StopAnimation()
    {
        if(count >19) animator.enabled = false;
    }

    void StartAnimation()
    {
        animator.enabled = true;
    }
}
