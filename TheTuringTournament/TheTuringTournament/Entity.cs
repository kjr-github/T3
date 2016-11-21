
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

        Model myModel;
        Vector3 location;


       // String model_address = "Models\\pylon";

        public Entity(Game1 g, Vector3 start_location, String model_address)
        {
            game = g;
            aspectRatio = game.aspectRatio;
            camera = game.camera;

            location = start_location;

            myModel = game.Content.Load<Model>(model_address);


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

                    effect.World = Matrix.Identity * Matrix.CreateTranslation(location);
                    effect.View = camera.getView();

                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;

                    // 1.0f, and 1000.0f are the clipping frame parameters, currently copied from Terrain class, I believe

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView, aspectRatio, 1.0f, 1000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }


            
        }



    }

    class Tower : Entity
    {

        public Tower(Game1 g, Vector3 start_location, String model_address) : base(g, start_location, model_address)
        {

        }
    }
}