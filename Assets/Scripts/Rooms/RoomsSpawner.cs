﻿using UnityEngine;

public class RoomsSpawner : MonoBehaviour {

    [SerializeField] private GameObject[] _roomsObjects;
    private string[] _rooms = new[] { "room_standart", "room_flashlight", "room_standart1", "room_standart2", "room_standart3" };
    private Vector3 _lastSpawnVector;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "DeadlyPlayer" || other.tag == "FlyPlayer")
        {
            CreateRooms(Random.Range(0, _rooms.Length));
            Destroy(gameObject);
        }
    }

    private void CreateRooms(int v)
    {
        Instantiate(Resources.Load(_rooms[v]), new Vector3(0, transform.position.y + 8f), Quaternion.identity);
    }
}