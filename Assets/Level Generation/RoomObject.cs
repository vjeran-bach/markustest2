using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject
{
    public float fRoomSizeX;
    public float fRoomSizeY;
    public float fRoomSizeXY;
    public int iBlockCount;

    public GameObject GoRightBorderBlock;
    public GameObject GoTopBorderBlock;

    public float fRightPos;
    public float fTopPos;

    public RoomObject(GameObject GoRoomPivot)
    {
        //for each block of the room, check if it is on the top border or on the right border
        foreach (Transform child in GoRoomPivot.transform)
        {
            iBlockCount++;
            float fFindRightPos = -1;
            if (child.position.x > fFindRightPos)
            {
                fFindRightPos = child.localPosition.x;
                fRightPos = child.localPosition.x;
                GoRightBorderBlock = child.gameObject;
            }

            float fFindTopPos = -1;
            if (child.position.y > fFindTopPos)
            {
                fFindTopPos = child.localPosition.y;
                fTopPos = child.localPosition.y;
                GoTopBorderBlock = child.gameObject;
            }
        }

        //adjust the size based on the borderblocks
        fRoomSizeX = fRightPos + 1;
        fRoomSizeY = fTopPos + 1;
        fRoomSizeXY = fRoomSizeX * fRoomSizeY;
    }
}