﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.Tools
{
    public class Raycast
    {


        public static bool RayVsRect(Point origin, Vector2 direction, Rectangle target, ref Vector2 contact_point, ref Vector2 contact_normal, ref float t_hit_near)
        {
            Vector2 t_near = new Vector2((target.X - origin.X) / direction.X, (target.Y - origin.Y) / direction.Y);
            Vector2 t_far = new Vector2((target.X + target.Width - origin.X) / direction.X, (target.Y + target.Height - origin.Y) / direction.Y);

            if (t_near.X > t_far.X)
            {
                Vector2 temp_near = new Vector2(t_far.X,t_near.Y);
                Vector2 temp_far = new Vector2(t_near.X, t_far.Y);
                t_near = temp_near;
                t_far = temp_far;
            }
            if (t_near.Y > t_far.Y)
            {
                Vector2 temp_near = new Vector2(t_near.X, t_far.Y);
                Vector2 temp_far = new Vector2(t_far.X, t_near.Y); 
                t_near = temp_near;
                t_far = temp_far;
            }

            if (t_near.X > t_far.Y || t_near.Y > t_far.X) return false;

            t_hit_near = Math.Max(t_near.X, t_near.Y);
            float t_hit_far = Math.Max(t_far.X, t_far.Y);

            //stopping collisions in the negitive direction
            if (t_hit_far < 0) return false;

            contact_point = origin.ToVector2() + t_hit_near * direction;

            if (t_near.X > t_near.Y)
                if (direction.X < 0)
                    contact_normal = new Vector2(1, 0);
                else
                    contact_normal = new Vector2(-1, 0);
            else if (t_near.X < t_near.Y)
                if (direction.Y < 0)
                    contact_normal = new Vector2(0, 1);
                else
                    contact_normal = new Vector2(0, -1);

            if(t_hit_near < 1)return true;

            return false;
        }

        public static bool DynamicRectVsRect(Rectangle main,Vector2 mainVel,Rectangle target,Vector2 targetVel, ref Vector2 contact_point, ref Vector2 contact_normal, ref float contact_time, GameTime gameTime)
        {
            if (mainVel.X == 0 && mainVel.Y == 0)
                return false;

            Rectangle expTarget =  new Rectangle();
            expTarget.Location = target.Location - new Point(main.Size.X / 2,main.Size.Y/2);
            expTarget.Size = target.Size + main.Size;

            if(RayVsRect(main.Location + new Point(main.Size.X / 2, main.Size.Y / 2), new Vector2((float)(mainVel.X* gameTime.ElapsedGameTime.TotalMilliseconds), (float)(mainVel.Y * gameTime.ElapsedGameTime.TotalMilliseconds)), expTarget,ref contact_point,ref contact_normal,ref contact_time))
            {
                if (contact_time <= 1)
                    return true;
            }

            return false;
        }

    }
}
