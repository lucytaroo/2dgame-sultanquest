using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraController : MonoBehaviour
    {
        public float damping = 1.5f;
        public Vector2 offset = new Vector2(10f, 10f);
        public bool faceLeft;
        private Transform player;
        private int lastX;

        // Start is called before the first frame update
        void Start()
        {
            offset = new Vector2(Mathf.Abs(offset.x), offset.y);
            FindPlayer(faceLeft);
        }

        public void FindPlayer(bool playerFaceLeft)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            lastX = Mathf.RoundToInt(player.position.x);
            float screenHeight = 2.0f * Camera.main.orthographicSize;
            offset.y = 7.5f;
            if (playerFaceLeft)
            {
                float cameraY = player.position.y - (screenHeight / 2.0f) + offset.y; // Increase offset.y for higher position
                transform.position = new Vector3(player.position.x - offset.x, cameraY, transform.position.z);
            }
            else
            {
                float cameraY = player.position.y - (screenHeight / 2.0f) + offset.y; // Increase offset.y for higher position
                transform.position = new Vector3(player.position.x + offset.x, cameraY, transform.position.z);
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (player)
            {
                int currentX = Mathf.RoundToInt(player.position.x);
                if (currentX > lastX) faceLeft = false;
                else if (currentX < lastX) faceLeft = true;
                lastX = Mathf.RoundToInt(player.position.x);

                Vector3 target;
                if (faceLeft)
                {
                    float screenHeight = 2.0f * Camera.main.orthographicSize;
                    float cameraY = player.position.y - (screenHeight / 2.0f) + offset.y;
                    target = new Vector3(player.position.x - offset.x, cameraY, transform.position.z);
                }
                else
                {
                    float screenHeight = 2.0f * Camera.main.orthographicSize;
                    float cameraY = player.position.y - (screenHeight / 2.0f) + offset.y;
                    target = new Vector3(player.position.x + offset.x, cameraY, transform.position.z);
                }

                
                transform.position = target;
            }
        }
    }
}
