using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _coinCount = 0;
    [SerializeField] private float _powerUpLenght;
    [SerializeField] private bool _isPowerUp;
    [SerializeField] private bool _isDead;
    [SerializeField] Sprite _defaultSprite;
    [SerializeField] Sprite _powerUpSprite;
    [SerializeField] ParticleSystem _starHitFX;
    [SerializeField] ParticleSystem _powerUpWindFX;
    [SerializeField] SoundController _soundController;

    private const string _powerUpAnimationTrigger = "isPowerUp";
    private const string _moveAnimationSpeed = "moveSpeed";
    private SpriteRenderer _spriteRenderer;
    private Animator _playerAnimator;

    private float _endPowerUpTime;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> CoinChanged;
    public event UnityAction Died;
    public event UnityAction PowerUp;
    public event UnityAction PowerDown;
    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public int CoinCount => _coinCount;
    public bool IsPowerUp => _isPowerUp;
    public bool IsDead => _isDead;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<Animator>();

        _spriteRenderer.sprite = _defaultSprite;
        HealthChanged?.Invoke(_health);
        _isPowerUp = false;
        _isDead = false;
        _powerUpWindFX.Stop();
    }

    private void FixedUpdate()
    {
        if (_isPowerUp && (Time.time >= _endPowerUpTime))
        {
            _isPowerUp = false;
            PowerDown?.Invoke();
            _spriteRenderer.sprite = _defaultSprite;
            _playerAnimator.SetBool(_powerUpAnimationTrigger, false);
            _playerAnimator.SetFloat(_moveAnimationSpeed, 1f);
            _powerUpWindFX.Stop();
        }
    }

    public void ApplyDamage(int damage)
    {
        if (!_isPowerUp)
        {
            _health -= damage;
            HealthChanged?.Invoke(_health);
            _soundController.PlayTakeDamageSound();

            if (_health <= 0)
            {
                Die();
            }
        }
        else
        {
            _starHitFX.Play();
            _soundController.PlayDestroyEnemySound();
        }
    }

    public void HealthUp(int hpValue)
    {
        if (_health < _maxHealth)
        {
            _health += hpValue;
            if(_health > _maxHealth)
            {
                _health = _maxHealth;
            }

            HealthChanged?.Invoke(_health);
            _soundController.PlayHealthUpSound();
        }
    }

    public void Die()
    {
        _isDead = true;
        Died?.Invoke();
        _soundController.StopBackgroundMusic();
        _soundController.PlayGameOverSound();
    }

    public void ChangeCoinCount(int coinValue)
    {
        _coinCount += coinValue;
        CoinChanged?.Invoke(_coinCount);
        _soundController.PlayCoinUpSound();
    }

    public void GetPowerUp()
    {
        _endPowerUpTime = Time.time + _powerUpLenght;
        _isPowerUp = true;
        PowerUp?.Invoke();
        _spriteRenderer.sprite = _powerUpSprite;
        _playerAnimator.SetBool(_powerUpAnimationTrigger, true);
        _playerAnimator.SetFloat(_moveAnimationSpeed, 1.5f);
        _powerUpWindFX.Play();
        _soundController.PlayPowerUpSound();
    }
}
