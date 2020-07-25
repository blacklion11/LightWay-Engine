﻿using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightWay.Engine.ECS.Systems
{
    class RenderSystem
    {
        public SpriteBatch spriteBatch { get; private set; }

        private TextureC TextureC;
        private Texture2D Texture;
        private Vector2 Pos;
        GraphicsDevice graphicsDevice;
        EntityController entityController;

        List<Entity> compatableEntitys = new List<Entity>();
        public RenderSystem(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice, EntityController entityController)
        {
            this.graphicsDevice = graphicsDevice;
            this.entityController = entityController;
            this.spriteBatch = spriteBatch;
        }
        public void Update(GameTime gameTime, ComponentIndexPool CIP)
        {
            CameraC camera = ((CameraC)CIP.GetAll(typeof(CameraC)).First().Value);
            compatableEntitys = entityController.EntitesThatContainComponents(entityController.GetAllEntityWithComponent<PositionC>(), typeof(TextureC), typeof(ForgroundC));
            spriteBatch.Begin(SpriteSortMode.Texture,null,null,null,null,null,camera.matrix);
            ProcessEntity();
            spriteBatch.End();
        }
        public void ProcessEntity()
        {
            foreach (Entity e in compatableEntitys)
            {
                TextureC = e.GetComponent<TextureC>();
                Texture = TextureC.Texture;
                Pos = e.GetComponent<PositionC>();

                float width = Texture.Width * TextureC.scale.X;
                float height = Texture.Height * TextureC.scale.Y;
                spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, (int)width, (int)height), Color.White);
            }
            
        }
    }

}
