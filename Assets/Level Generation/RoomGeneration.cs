using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public GameObject GoStartRoom;
    public RoomObject RStartRoom;

    public float fStartRoomSizeX;
    public float fStartRoomSizeY;
    public float fStartRoomSizeXY;
    public int iStartBlockCount;

    // Start is called before the first frame update
    void Start()
    {
        RStartRoom = new RoomObject(GoStartRoom);
    }

    // Update is called once per frame
    void Update()
    {
        fStartRoomSizeX = RStartRoom.fRoomSizeX;
        fStartRoomSizeY = RStartRoom.fRoomSizeY;
        fStartRoomSizeXY = RStartRoom.fRoomSizeXY;
        iStartBlockCount = RStartRoom.iBlockCount;
    }
}
