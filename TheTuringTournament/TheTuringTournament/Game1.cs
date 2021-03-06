﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TheTuringTournament
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public float aspectRatio;
        private SpriteFont font;

        public Camera camera;
        Terrain landscape;
        Matrix cam_mat;

        //Plane textures
        Plane grid_floor;
        Plane[] highlighted = new Plane[6];
        

        //Game Entities
        Tower t1;
        Tower t2;




        // This is the model instance that we'll load
        // our XNB into:
        Model drone;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            camera = new Camera(new Vector3(-100, 0, 0), Vector3.Zero, new Vector3(2, 2, 2), new Vector3(0, -100, 256));

            landscape = new Terrain(GraphicsDevice);


            //Initial position for Tower entity
            Vector3 tower1_start = new Vector3(290, -55, 180);
            t1 = new Tower(this, tower1_start);

            t1.rotate_X((float)(Math.PI / 2));
            t1.rotate_Y((float)(Math.PI / 2));
            
            // Second tower
            Vector3 tower2_start = new Vector3(270, -55, -220);
            t2 = new Tower(this, tower2_start);

            t2.rotate_X((float)(Math.PI / 2));
            t2.rotate_Y(-(float)(Math.PI / 2));





            //Setting up the grid_floor plane
            grid_floor = new Plane(graphics);

            VertexPositionTexture[] floorVerts = new VertexPositionTexture[6];
            // 420x320 size rectangle divided into 21x16 amount of 20x20 squares.
            floorVerts[0].Position = new Vector3(120, -95, -230); //front left
            floorVerts[1].Position = new Vector3(440, -95, -230); //back left
            floorVerts[2].Position = new Vector3(120, -95, 190); // front right              
            floorVerts[4].Position = new Vector3(440, -95, 190); //back right 
            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[5].Position = floorVerts[2].Position;              
            // Mapping the texture
            floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            floorVerts[1].TextureCoordinate = new Vector2(0, 16);
            floorVerts[2].TextureCoordinate = new Vector2(21, 0);
            floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            floorVerts[4].TextureCoordinate = new Vector2(21, 16);
            floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;
            
            grid_floor.setVerts(floorVerts);





            for (int i = 0; i < highlighted.Length; i++)
            {
                highlighted[i] = new Plane(graphics);
            }

            //highlighted = new Plane(graphics);


            floorVerts[0].Position = new Vector3(300, -94.9f, -230); //front left
            floorVerts[1].Position = new Vector3(320, -94.9f, -230); //back left
            floorVerts[2].Position = new Vector3(300, -94.9f, 190); // front right              
            floorVerts[4].Position = new Vector3(320, -94.9f, 190); //back right 
            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[5].Position = floorVerts[2].Position;
            // Mapping the texture
          //  floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            floorVerts[1].TextureCoordinate = new Vector2(0, 1);
           // floorVerts[2].TextureCoordinate = new Vector2(21, 0);
            floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            floorVerts[4].TextureCoordinate = new Vector2(21, 1);
            //floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;

            highlighted[0].setVerts(floorVerts);
            

            floorVerts[0].Position = new Vector3(360, -94.9f, -230); //front left
            floorVerts[1].Position = new Vector3(380, -94.9f, -230); //back left
            floorVerts[2].Position = new Vector3(360, -94.9f, 190); // front right              
            floorVerts[4].Position = new Vector3(380, -94.9f, 190); //back right 
            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[5].Position = floorVerts[2].Position;

            highlighted[1].setVerts(floorVerts);


            floorVerts[0].Position.X = 420;
            //= new Vector3(420, -94.9f, -230); //front left
            floorVerts[1].Position.X = 440;
            //new Vector3(440, -94.9f, -230); //back left
            floorVerts[2].Position.X = 420;
            //new Vector3(420, -94.9f, 190); // front right              
            floorVerts[4].Position.X = 440;
            //= new Vector3(440, -94.9f, 190); //back right 
            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[5].Position = floorVerts[2].Position;

            highlighted[2].setVerts(floorVerts);



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;



            //3D assets
          
            drone = Content.Load<Model>("Models\\myTurret2");



           

            //checkerboardTexture = this.Content.Load<Texture2D>("Textures\\checkerboard");

            landscape.SetHeightMapData(Content.Load<Texture2D>("Textures\\wellington3"), Content.Load<Texture2D>("Textures\\checkerboard"));
            font = Content.Load<SpriteFont>("Score");

            
          
            grid_floor.texture = Content.Load<Texture2D>("Textures\\gridsquare");

            for (int i = 0; i < highlighted.Length; i++)
            {
                highlighted[i].texture = Content.Load<Texture2D>("Textures\\hgridsquare");
            }

            //highlighted.texture = Content.Load<Texture2D>("Textures\\hgridsquare");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            camera.Update();
           

            cam_mat = camera.getView();
            // TODO: Add your update logic here
            //camera.Update(gameTime);
            base.Update(gameTime);
        }




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            //THIS PERTAINS TO AN OLDER VERSION OF MONOGAME
            //GraphicsDevice.BlendState = BlendState.AlphaBlend;
            //GraphicsDevice.BlendState = BlendState.Opaque;
            //GraphicsDevice.BlendState = BlendState.Additive;
            //GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //GraphicsDevice.DepthStencilState = DepthStencilState.None;
            //GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
            //GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;



            camera.Draw(landscape);
         
            grid_floor.Draw(camera.getView(), camera.getProjection());

            for (int i = 0; i < highlighted.Length; i++)
            {
                highlighted[i].Draw(camera.getView(), camera.getProjection());
            }


            //highlighted.Draw(camera.getView(), camera.getProjection());


            
            t1.Draw();
            t2.Draw();


            Vector3 blab = new Vector3(1, 1, 1);
            DrawModel(drone, blab);


            DrawText();


            base.Draw(gameTime);
        }







        void DrawModel(Model myModel, Vector3 tranVector)
        {
            foreach(var mesh in myModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // We could set up custom lights, but this
                    // is the quickest way to get somethign on screen:
                    effect.EnableDefaultLighting();
                    // This makes lighting look more realistic on
                    // round surfaces, but at a slight performance cost:
                    effect.PreferPerPixelLighting = true;

                    // The world matrix can be used to position, rotate
                    // or resize (scale) the model. Identity means that
                    // the model is unrotated, drawn at the origin, and
                    // its size is unchanged from the loaded content file.
                    effect.World = Matrix.Identity * Matrix.CreateTranslation(tranVector);
                    effect.View = camera.getView();
                    effect.Projection = camera.getProjection();
                                       
                }
                // Now that we've assigned our properties on the effects we can
                // draw the entire mesh
                mesh.Draw();

            }
            
        }
  
        

        void DrawText()
        {
            spriteBatch.Begin();
            
            
            spriteBatch.DrawString(font, "Tower 1 " , new Vector2(25, 25), Color.Black);
            spriteBatch.DrawString(font, "  " + t1.getHealth(), new Vector2(25, 45), Color.Black);

            spriteBatch.DrawString(font, "Tower 2 ", new Vector2(1000, 25), Color.Black);
            spriteBatch.DrawString(font, "  " + t2.getHealth(), new Vector2(1000, 45), Color.Black);

            //spriteBatch.DrawString(font, "Ammunition: " + ammo, new Vector2(50, 80), Color.Black);
            //spriteBatch.DrawString(font, "Enemy Durability: " + score, new Vector2(550, 50), Color.Black);
            //spriteBatch.DrawString(font, "Enemy Ammunition: " + orbs, new Vector2(550, 80), Color.Black);

            spriteBatch.DrawString(font, "Free Camera Mode ", new Vector2(550, 80), Color.Green);
            spriteBatch.DrawString(font, "X: " + cam_mat[0,0] + " Y: " + cam_mat[0, 1] + " Z: " + cam_mat[0,2] + "A: " + cam_mat[0,3] , new Vector2(500, 120), Color.Green);
            spriteBatch.DrawString(font, "X: " + cam_mat[1, 0] + " Y: " + cam_mat[1, 1] + " Z: " + cam_mat[1, 2] + "A: " + cam_mat[1, 3], new Vector2(500, 140), Color.Green);
            spriteBatch.DrawString(font, "X: " + cam_mat[2, 0] + " Y: " + cam_mat[2, 1] + " Z: " + cam_mat[2, 2] + "A: " + cam_mat[2, 3], new Vector2(500, 160), Color.Green);
            spriteBatch.DrawString(font, "X: " + cam_mat[3, 0] + " Y: " + cam_mat[3, 1] + " Z: " + cam_mat[3, 2] + "A: " + cam_mat[3, 3], new Vector2(500, 180), Color.Green);

            //spriteBatch.DrawString(font, "I: " + System.String.Format("{0:0,00.000}", cam_direction.X) + " J: " + System.String.Format("{0:0,00.000}", cam_direction.Y) + " Z: " + System.String.Format("{0:0,00.000}", cam_direction.Z), new Vector2(550, 140), Color.Green);

          
            spriteBatch.End();

        }


        


    }
}
