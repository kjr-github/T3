
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheTuringTournament
{
    class Entity
    {

        Game1 game;
        float aspectRatio;
        Camera camera;

        public Model myModel;
        Vector3 location;

        public float scale;

        public float modelRotationX;
        public float modelRotationY;


       // String model_address = "Models\\pylon";

        public Entity(Game1 g, Vector3 start_location)
        {
            game = g;
            aspectRatio = game.aspectRatio;
            camera = game.camera;

            location = start_location;

            //myModel = game.Content.Load<Model>(model_address);

            
        }


        public void Draw()
        {


            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in myModel.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = Matrix.Identity * Matrix.CreateScale(scale)* Matrix.CreateRotationX(modelRotationX) *Matrix.CreateRotationY(modelRotationY)* Matrix.CreateTranslation(location);
                    effect.View = camera.getView();
                    effect.Projection = camera.getProjection();
                   
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }


            
        }



    }

    class Tower : Entity
    {

        public Tower(Game1 g, Vector3 start_location) : base(g, start_location)
        {

            myModel = g.Content.Load<Model>("Models\\test_tower");
            modelRotationX = (float)(Math.PI / 2);
            modelRotationY = (float)(Math.PI / 2);
            scale = 5;
        }
    }
}