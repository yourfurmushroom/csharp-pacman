﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace endOfTerm
{
    class AStar
    {
        public static int[,] map = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,1,1},
            {1,1,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,0,0,0,0,1,1,0,1,1,1,0,0,0,0,0,0,0,0,1,1,1,0,1,1,0,0,0,0,1,1},
            {1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,0,0,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,0,1,1,1,0,0,0,0,0,0,0,0,1,1,1,0,1,1,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,1,1,0,1,1,1,0,0,0,0,0,0,0,0,1,1,1,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,0,1,1,1,1,1,1,0,0,1,1,1,1,1,1,0,1,1,1,1,1,1,1,1},
            {1,1,0,0,0,0,1,1,0,1,1,1,0,0,0,0,0,0,0,0,1,1,1,0,1,1,0,0,0,0,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,1,1},
            {1,1,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        };

        public static Vector2 AIMoving(Vector2 monsterPosition,Vector2 playerPosition)
        {
            List<Vector2> path=new List<Vector2>();
            List<Vector2> openSet=new List<Vector2>();
            List<Vector2> closeSet=new List<Vector2>();
            Vector2 currentPosition = new Vector2(monsterPosition.x / 27, monsterPosition.y / 25);
            openSet.Add(currentPosition);
            Debug.WriteLine(openSet[0].x + " " + openSet[0].y);

            while (openSet.Count!=0)
            {
                    Debug.WriteLine(openSet[0].x+" "+ openSet[0].y);
                foreach (var point in openSet.ToList())
                {
                    if (map[point.y - 1, point.x] == 1 && point.y-1>0) openSet.Add(new Vector2(point.x, point.y - 1));
                    if (map[point.y +1, point.x] == 1 && point.y+1<28) openSet.Add(new Vector2(point.x, point.y + 1));
                    if (map[point.y , point.x-1] == 1 && point.x-1>0) openSet.Add(new Vector2(point.x-1, point.y));
                    if (map[point.y , point.x+1] == 1 && point.x+1<32) openSet.Add(new Vector2(point.x+1, point.y));
                    if (point != currentPosition) closeSet.Add(point);
                    openSet.Remove(point);
                }

            }
            return new Vector2(openSet[0].x*27,openSet[0].y*25);
        }
    }
}