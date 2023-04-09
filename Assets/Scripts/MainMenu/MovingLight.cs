using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MovingLight : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _movingRange = 40f;
        
        private bool _moveRight = true;
        private float _leftEdge;
        private float _rightEdge;

        private void Start()
        {
            _rightEdge = transform.localPosition.x + _movingRange / 2;
            _leftEdge = transform.localPosition.x - _movingRange / 2;
        }

        void Update()
        {
            LeftRightMovement();
        }
        

        private void LeftRightMovement()
        {
            if (transform.localPosition.x > _rightEdge)
            {
                _moveRight = false;
            }

            if (transform.localPosition.x < _leftEdge)
            {
                _moveRight = true;
            }

            if (_moveRight)
            {
                transform.position = new Vector3(transform.position.x + _moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - _moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
        }
    }

