using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;

namespace Utility
{
    public class Vec
    {
        /// <summary>
        /// Generates a random Vector2 inside the given Rect.
        /// </summary>
        /// <param name="r">Which Rect</param>
        /// <returns>The generated Vector2 </returns>
        public static Vector2 RandomInsideRect(Rect r)
        {
            float x = Random.Range(r.xMin, r.xMax);
            float y = Random.Range(r.yMin, r.yMax);
            return new Vector2(x, y);
        }

        public static Vector2 RandomInCircle(Circle c)
        {
            float x = c.center.x+ Random.Range(-c.radius, c.radius);
            float y = c.center.y + Random.Range(-c.radius , c.radius);
            return new Vector2(x,y);
        }

        public static Vector2 RandomOnCircle(Circle c)
        {
            float f = Random.Range(0f, 360f);
            float x = c.radius * Mathf.Cos(f);
            float y = c.radius * Mathf.Sin(f);
            return new Vector2(x, y)+c.center;
        }

        public static Vector2 RandomVec()
        {
            return new Vector2(-Random.Range(Mathf.Infinity, Mathf.Infinity), -Random.Range(Mathf.Infinity, Mathf.Infinity));
        }

        public static Vector3[] Conv(Vector2[] v2)
        {
            Vector3[] v3 = new Vector3[v2.Length];
            for(int i=0;i<v2.Length; i++)
                v3[i] = new Vector3(v2[i].x, v2[i].y, 0);
            return v3;
        }
    }

    public class Rec
    {
        /// <summary>
        /// Expands the width and height of given rect by given amount.
        /// </summary>
        /// <param name="r">Which Rect</param>
        /// <param name="expandX">Width increase amount</param>
        /// <param name="expandY">Height increase amount</param>
        /// <returns></returns>
        public static void Expand(ref Rect r,float expandX,float expandY)
        {
            Vector2 v = r.center;
            r.width += expandX;
            r.height += expandY;
            r.center=v;
        }
    }

    public class Draw
    {
        /// <summary>
        /// Draw a rect that can only be seen in Play mode.
        /// </summary>
        /// <param name="r">Which Rect</param>
        /// <param name="c">Color of the drawn Rect</param>
        public static void Rec(Rect r,Color c)
        {
            Debug.DrawLine(new Vector3(r.xMin,r.yMin,0), new Vector3(r.xMin, r.yMax,0),c,Mathf.Infinity);
            Debug.DrawLine(new Vector3(r.xMin, r.yMax,0), new Vector3(r.xMax, r.yMax,0),c, Mathf.Infinity);
            Debug.DrawLine(new Vector3(r.xMax, r.yMax,0), new Vector3(r.xMax, r.yMin,0),c, Mathf.Infinity);
            Debug.DrawLine(new Vector3(r.xMax, r.yMin,0), new Vector3(r.xMin, r.yMin,0), c, Mathf.Infinity);
        }
    }

    public class Circle
    {
        public Vector2 center;
        public float radius;

        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public Circle()
        {
            this.center = new Vector2(0, 0);
            this.radius = 0;
        }

        public bool Contains(Vector2 point)
        {
            if ((point - center).magnitude <= radius)
                return true;
            return false;
        }

    }

    public class Sphere
    {
        public Vector3 center;
        public float radius;

        public Sphere(Vector3 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public Sphere()
        {
            this.center = new Vector3(0, 0, 0);
            this.radius = 0;
        }

        public bool Contains(Vector3 point)
        {
            if ((point - center).magnitude <= radius)
                return true;
            return false;
        }

    }


    public class Path
    {
        public Vector3[] waypoint;

        public Path(Vector3[] waypoints)
        {
            this.waypoint = waypoints;
        }

        public Path()
        {
            waypoint = new Vector3[2]
                {
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 0)
                };
        }

        public Vector3[] GetSegments()
        {
            Vector3[] segment = new Vector3[waypoint.Length - 1];
            Debug.Log("1");
            for (int i = 1; i < waypoint.Length; i++)
                segment[i - 1] = waypoint[i] - waypoint[i - 1];
            Debug.Log("2");
            return segment;
        }

        public float GetLenght()
        {
            float f = 0;
            foreach (var v in GetSegments())
                f += v.magnitude;
            return f;
        }
    }

    public class Path2D
    {
        public Vector2[] waypoint;

        public Path2D(Vector2[] waypoints)
        {
            this.waypoint = waypoints;
        }

        public Path2D()
        {
            waypoint = new Vector2[2]
                {
                new Vector2(0, 0),
                new Vector2(0, 0)
                };
        }

        public Vector2[] GetSegments()
        {
            Vector2[] segment = new Vector2[waypoint.Length - 1];
            for (int i = 1; i < waypoint.Length; i++)
                segment[i - 1] = waypoint[i] - waypoint[i - 1];
            return segment;
        }

        public float GetLenght()
        {
            float f = 0;
            foreach (var v in GetSegments())
                f += v.magnitude;
            return f;
        }

        public static Vector2[] RandomInsideRect(Rect r, int segment)
        {
            List<Vector2> v = new List<Vector2>(segment + 1);
            for (int j = 0; j < segment + 1; j++)
                v.Add(Vector2.zero);
            for (int i = 0; i < v.Count; i++)
                    v[i] = Vec.RandomInsideRect(r);
            return v.ToArray();
        }
    }

}

