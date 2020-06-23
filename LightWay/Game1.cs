﻿using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using LightWay;
using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LightWay
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EntityController entityController;

        private double keyUpdateTimer = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
           // Window.ClientSizeChanged += OnResize;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            entityController = new EntityController(GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Body b = BodyFactory.CreateRectangle(entityController.physicsWorld, ConvertUnits.ToSimUnits((Grid.gridPixelSize)), ConvertUnits.ToSimUnits((Grid.gridPixelSize)), 20f, new Vector2(0, 0), 0, BodyType.Dynamic);
            b.Friction = 0.1f;
            b.Restitution = 0f;
     
            entityController.CreateEntity(new PositionC(0, 200,b), new ControllableC(), new TextureC(GraphicsDevice,new Vector2(Grid.gridPixelSize,Grid.gridPixelSize),Color.Black), new VelocityC());
            //entityController.CreateEntity(new PositionC(50, 50, BodyFactory.CreateBody(entityController.physicsWorld, new Vector2(0, 0), 0, BodyType.Static)), new TextureC(GraphicsDevice, new Vector2(50, 50), Color.Black));
            // entityController.CreateEntity(new PositionC(new Vector2(0, 300)), new TextureC(GraphicsDevice, new Vector2(300, 100),Color.Green),new ColliderC(new Rectangle(0,300,300,100)));
            //entityController.CreateEntity(new PositionC(new Vector2(300, 200)), new TextureC(GraphicsDevice, new Vector2(100, 300), Color.Green), new ColliderC(new Rectangle(300, 200, 100, 300)));
            entityController.CreateEntity(new CameraC());
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyUpdateTimer -= gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if(keyUpdateTimer < -.1f)
            {
                Input.UpdateKeys();
                keyUpdateTimer = 0;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Input.getGamePadKey() || Input.getKeyBoardKey(Keys.Escape))
                Exit();
            entityController.GeneralUpdate(gameTime);
           
            //base.IsMouseVisible = true;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            entityController.RenderingUpdate(gameTime);
            // TODO: Add your drawing code here

            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = Color.Black;
            texture.SetData(c);
            spriteBatch.Begin();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
