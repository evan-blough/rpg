using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : NonPlayerCharacterHandler
{
    bool canEnterBattle;
    bool isInBattle;
    bool isChasing;
    public List<GameObject> encounterFormation;
    Vector3 preChasePoint;
    private void Start()
    {
        cam = SceneManager.instance.cam.transform;
        canEnterBattle = false;
        StartCoroutine(WaitToStart());
    }

    private void Update()
    {
        if (!canEnterBattle) { StartCoroutine(ActivateHitbox()); }

        SetMovement();
        VerticalMovement();
        Move();
        AnimationStateCheck();
    }

    public IEnumerator ActivateHitbox()
    {
        yield return new WaitForSeconds(1.5f);

        canEnterBattle = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacterHandler>() && canEnterBattle)
        {
            canEnterBattle = false;
            SceneManager.instance.StartCoroutine(SceneManager.instance.StartBattle(this));
        }
    }
}
