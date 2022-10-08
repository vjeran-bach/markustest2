using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [SerializeField] public GameObject GoStartRoom;
    [SerializeField] public RoomObject RStartRoom;

    [SerializeField] public float fStartRoomSizeX;
    [SerializeField] public float fStartRoomSizeY;

    // Start is called before the first frame update
    void Start()
    {
        RStartRoom = new Room(GoStartRoom);
    }

    // Update is called once per frame
    void Update()
    {
        fStartRoomSizeX = RStartRoom.fRoomSizeX;
        fStartRoomSizeY = RStartRoom.fRoomSizeY;
    }
}
