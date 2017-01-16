﻿using UnityEngine;
using System.Collections;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] private float _flySpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider2D;
    private bool isFlying = false, isHighJump = false, isDeadly = false;
    private int low = 0;
    private PlayerController _playerControl;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isFlying)
            StartCoroutine(FlyPowerUp());
        if (isHighJump)
            StartCoroutine(HighJumpPowerUp());
        if (isDeadly)
            StartCoroutine(DeadlyPowerUp());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "PowerUpFly")
        { StopAllCoroutines(); PowerUpAction(PowerUpState.fly); Destroy(other.gameObject); ChangeSprite(_sprites[0]); }
        if (other.tag == "PowerUpHighJump")
        { StopAllCoroutines(); PowerUpAction(PowerUpState.high_jump); Destroy(other.gameObject); ChangeSprite(_sprites[1]); }
        if (other.tag == "PowerUpDeadly")
        { StopAllCoroutines(); PowerUpAction(PowerUpState.deadly); Destroy(other.gameObject); ChangeSprite(_sprites[2]); }
    }

    IEnumerator ChangeSpriteBack(Sprite sprite)
    {
        yield return new WaitForSeconds(5f);
        _spriteRenderer.sprite = sprite;
    }

    IEnumerator HighJumpPowerUp()
    {
        _playerControl.JumpSpeed = 15;
        yield return new WaitForSeconds(5f);
        _playerControl.JumpSpeed = 8.5f;
        isHighJump = false;
        StopCoroutine(HighJumpPowerUp());
    }

    IEnumerator DeadlyPowerUp()
    {
        yield return 0;
    }

    IEnumerator FlyPowerUp()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _collider2D.enabled = false;
        _rigidbody2D.AddRelativeForce(Vector2.up * _flySpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(5f);
        isFlying = false;
        _collider2D.enabled = true;
        StopCoroutine(FlyPowerUp());
    }

    void PowerUpAction(PowerUpState state)
    {
        if (state == PowerUpState.high_jump)
            isHighJump = true; 
        if (state == PowerUpState.fly)
            isFlying = true;
        if (state == PowerUpState.deadly)
            isDeadly = true;
    }

    void ChangeSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        StartCoroutine(ChangeSpriteBack(_sprites[3]));
    }

    enum PowerUpState
    {
        fly,
        high_jump,
        deadly
    }
}
