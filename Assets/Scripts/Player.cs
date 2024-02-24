using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed = 2.0f;

    private CharacterController _controller;
    private Animator _animator;

    private Vector3 _playerVelocity;
    private float gravityValue;

    private void Start()
    {
        gravityValue = Physics.gravity.y;
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        // start walk animation if either walking or rotating
        _animator.SetBool("Walking", horizontal + vertical != 0);

        // rotate / move
        transform.Rotate(0f, horizontal, 0f);
        _controller.Move(vertical * Time.deltaTime * playerSpeed * transform.forward);

        // make sure player stays on ground
        var groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
