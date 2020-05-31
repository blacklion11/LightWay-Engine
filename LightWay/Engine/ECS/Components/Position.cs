﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class Position : IComponent
    {
        public Vector2 position;
        public Type type { get; private set; }

        public Position(Vector2 position)
        {
            this.position = position;
            this.type = this.GetType();
        }
    }
}
