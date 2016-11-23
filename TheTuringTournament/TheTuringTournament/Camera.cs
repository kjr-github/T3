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
    public class Camera
    {


        Matrix viewMatrix;
        Matrix projectionMatrix;


        public Matrix terrainMatrix;

        Vector3 position;
        Vector3 direction;
        Vector3 movement;
        Vector3 rotation;

        public Camera(Vector3 position, Vector3 direction, Vector3 movement, Vector3 landscapePosition)
        {
            this.position = position;
            this.direction = direction;
            this.movement = movement;
            rotation = movement * 0.02f;

            viewMatrix = Matrix.CreateLookAt(position, direction, Vector3.Up);

            projectionMatrix = Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f);

            terrainMatrix = Matrix.CreateTranslation(landscapePosition);


        }

        public Matrix getView()
        {
            return viewMatrix;
        }

        public Matrix getWorld()
        {
            return terrainMatrix;
        }

        public Matrix getProjection()
        {
            return projectionMatrix;
        }
       

        int blah = 0;

        public void Update()
        {
            Vector3 tempMovement = Vector3.Zero;
            Vector3 tempRotation = Vector3.Zero;
            //left

            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.A))
            {
                //camera.Update(1);
                tempMovement.X = +movement.X;
            }
            if (key.IsKeyDown(Keys.D))
            {
                // camera.Update(2);
                tempMovement.X = -movement.X;
            }
            if (key.IsKeyDown(Keys.W))
            {
                // camera.Update(3);
                tempMovement.Y = -movement.Y;
            }
            if (key.IsKeyDown(Keys.S))
            {
                // camera.Update(4);
                tempMovement.Y = +movement.Y;
            }
            if (key.IsKeyDown(Keys.Down))
            {
                // camera.Update(5);
                tempMovement.Z = -movement.Z;
            }
            if (key.IsKeyDown(Keys.Up))
            {
                // camera.Update(6);
                tempMovement.Z = +movement.Z;
            }
            if (key.IsKeyDown(Keys.Q))
            {
                //camera.Update(7);
                tempRotation.Y = -rotation.Y;
            }
            if (key.IsKeyDown(Keys.E))
            {
                // camera.Update(8);
                tempRotation.Y = +rotation.Y;
            }
            if (key.IsKeyDown(Keys.G))
            {
                //camera.Update(9);
                tempRotation.X = -rotation.X;
            }
            if (key.IsKeyDown(Keys.T))
            {
                //camera.Update(10);
                tempRotation.X = +rotation.X;
            }
            if (key.IsKeyDown(Keys.Space))
            {
                //camera.Update(11);
                //WHAT?
                viewMatrix[0, 0] = 1f;
                viewMatrix[0, 1] = 0f;
                viewMatrix[0, 2] = 0.025f;
                viewMatrix[0, 3] = 0.0f;

                viewMatrix[1, 0] = 0.0f;
                viewMatrix[1, 1] = 1f;
                viewMatrix[1, 2] = 0.5f;
                viewMatrix[1, 3] = 0.0f;

                viewMatrix[2, 0] = -0.029f;
                viewMatrix[2, 1] = -0.53f;
                viewMatrix[2, 2] = 0.85f;
                viewMatrix[2, 3] = 0.0f;



                viewMatrix[3, 0] = -347f;
                viewMatrix[3, 1] = 101f;
                viewMatrix[3, 2] = -260f;
                viewMatrix[3, 3] = 1.0f;
            }
            if (key.IsKeyDown(Keys.LeftShift))
            {
                //camera.Update(12);
                viewMatrix[0, 0] = -1f;
                viewMatrix[0, 1] = 0f;
                viewMatrix[0, 2] = 0.0f;
                viewMatrix[0, 3] = 0.0f;

                viewMatrix[1, 0] = 0.0f;
                viewMatrix[1, 1] = 1f;
                viewMatrix[1, 2] = 0.5f;
                viewMatrix[1, 3] = 0.0f;

                viewMatrix[2, 0] = 0f;
                viewMatrix[2, 1] = 0.5f;
                viewMatrix[2, 2] = -0.85f;
                viewMatrix[2, 3] = 0.0f;



                viewMatrix[3, 0] = 360f;
                viewMatrix[3, 1] = 90f;
                viewMatrix[3, 2] = -215f;
                viewMatrix[3, 3] = 1.0f;
            }

          
            //move camera to new position
            viewMatrix = viewMatrix * Matrix.CreateRotationX(tempRotation.X) * Matrix.CreateRotationY(tempRotation.Y) * Matrix.CreateTranslation(tempMovement);
            //update position

          

            position += tempMovement;
            direction += tempRotation;



        }





        public void SetEffects(BasicEffect basicEffect)
        {
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
            basicEffect.World = terrainMatrix;
        }

        public void Draw(Terrain terrain)
        {
           // terrain.basicEffect.Apply();
            SetEffects(terrain.basicEffect);
            foreach (EffectPass pass in terrain.basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                terrain.Draw();
             //   pass.End();
            }
            //terrain.basicEffect.End();
        }



    }
}
