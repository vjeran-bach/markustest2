using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    public float fRoomSizeX;
    public float fRoomSizeY;

    public GameObject GoRightBorderBlock;
    public GameObject GoBottomBorderBlock;

    public float fRightPos;
    public float fTopPos;

    public RoomObject(GameObject GoRoomPivot)
    {
        //for each block of the room, check if it is on the top border or on the right border
        foreach (Transform child in GoRoomPivot.transform)
        {
            float fFindRightPos = -1;
            if (child.position.x > fFindRightPos)
            {
                fFindRightPos = child.position.x;
                fRightPos = child.position.x;
                GoRigthBorderBlock = child.gameObject;
            }

            float fFindTopPos = 1;
            if (child.position.y > fFindTopPos)
            {
                fFindTopPos = child.position.y;
                fTopPos = child.position.y;
                GoTopBorderBlock = child.gameObject;
            }
        }

        //adjust the size based on the borderblocks
        fRoomSizeX = fRightPos;
        fRoomSizeY = fTopPos;
    }
}