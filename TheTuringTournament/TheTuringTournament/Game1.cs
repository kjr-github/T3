using System;
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


        public Camera camera;
        Terrain landscape;

        private SpriteFont font;

        Matrix cam_mat;



        VertexPositionTexture[] floorVerts;
        Texture2D floorTexture;
        BasicEffect floorEffect;

        // This is the model instance that we'll load
        // our XNB into:
        Model model;
        Model drone;
        Model pylon;

        //Game Entities
        Tower tower_1;
        String tower_model = "Models\\test_tower";

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

            camera = new Camera(new Vector3(-100, 0, 0), Vector3.Zero, new Vector3(2, 2, 2), new Vector3(0, -100, 256));

            landscape = new Terrain(GraphicsDevice);



            floorVerts = new VertexPositionTexture[6];
            /*
            floorVerts[0].Position = new Vector3(-20, -20, 0);
            floorVerts[1].Position = new Vector3(-20, 20, 0);
            floorVerts[2].Position = new Vector3(20, -20, 0);
            */

            // 400x300 size rectangle

            floorVerts[0].Position = new Vector3(120, -85, -200); //front left
            floorVerts[1].Position = new Vector3(420, -85, -200); //back left
            floorVerts[2].Position = new Vector3(120, -85, 200);


            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[4].Position = new Vector3(420, -85, 200);
            floorVerts[5].Position = floorVerts[2].Position;

            int repetitions = 20;
            // New code:
            floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            floorVerts[1].TextureCoordinate = new Vector2(0, repetitions);
            floorVerts[2].TextureCoordinate = new Vector2(repetitions, 0);

            floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            floorVerts[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;


            floorEffect = new BasicEffect(graphics.GraphicsDevice);


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
            model = Content.Load<Model>("Models\\enemy");
            pylon = Content.Load<Model>("Models\\pylon");
            drone = Content.Load<Model>("Models\\myTurret2");


            Vector3 bleb = new Vector3(100, 20, 120);

           tower_1 = new Tower(this, bleb, tower_model);

           

            //checkerboardTexture = this.Content.Load<Texture2D>("Textures\\checkerboard");

            landscape.SetHeightMapData(Content.Load<Texture2D>("Textures\\wellington3"), Content.Load<Texture2D>("Textures\\checkerboard"));
            font = Content.Load<SpriteFont>("Score");



            floorTexture = Content.Load<Texture2D>("Textures\\checkerboard2");


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

           
            Vector3 bleb = new Vector3(1,60,1);
            Vector3 blab = new Vector3(1, 1, 1);

            DrawFloor();

            DrawModel(drone, blab);
            //DrawModel(pylon, bleb);

            tower_1.Draw();

            DrawText();


            base.Draw(gameTime);
        }







        void DrawFloor()
        {

            floorEffect.World = Matrix.Identity;
            floorEffect.View = camera.getView();

            
            floorEffect.Projection = camera.getProjection();


            floorEffect.TextureEnabled = true;
            floorEffect.Texture = floorTexture;


            foreach (var pass in floorEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                    // We’ll be rendering two trinalges
                    PrimitiveType.TriangleList,
                    // The array of verts that we want to render
                    floorVerts,
                    // The offset, which is 0 since we want to start 
                    // at the beginning of the floorVerts array
                    0,
                    // The number of triangles to draw
                    2);
            }
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
                    // We want the aspect ratio of our display to match
                    // the entire screen's aspect ratio:
                    float aspectRatio =
                        graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
                    // Field of view measures how wide of a view our camera has.
                    // Increasing this value means it has a wider view, making everything
                    // on screen smaller. This is conceptually the same as "zooming out".
                    // It also 
                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;

                    // 1.0f, and 1000.0f are the clipping frame parameters, currently copied from Terrain class, I believe

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
            // spriteBatch.DrawString(font, "Ship Integrity: " + health, new Vector2(50, 50), Color.Black);
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
