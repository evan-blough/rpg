using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : NonPlayerCharacter
{
    bool canEnterBattle;
    bool isInBattle;
    public List<GameObject> encounterFormation;
    private void Start()
    {
        cam = GameManager.instance.cam.transform;
        canEnterBattle = false;
    }

    private void Update()
    {
        if (!canEnterBattle) { StartCoroutine(ActivateHitbox()); }

        sprite.gameObject.transform.rotation = Camera.main.transform.rotation;

        Move();
        var angleDif = CameraHandler.AngleNormalization(sprite.transform.eulerAngles.y);

        animDirection = GetDirection(transform.eulerAngles.y - angleDif);
        animator.SetFloat("Direction", animDirection);

        if (animDirection <= 7 && animDirection >= 5)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }
    public override void Move()
    {

    }

    public IEnumerator ActivateHitbox()
    {
        yield return new WaitForSeconds(1.5f);

        canEnterBattle = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() && canEnterBattle)
        {
            canEnterBattle = false;
            GameManager.instance.StartCoroutine(GameManager.instance.StartBattle(this));
        }
    }
}
