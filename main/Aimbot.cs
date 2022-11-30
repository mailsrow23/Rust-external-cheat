using covet.cc.Rust.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace covet.cc.Cheat.Aimbot  
{
    class Aimbot
    {
        public static float ScreenToenemy(Vector3 position)
        {
            Vector2 vec2;
            Visuals.ESP.WorldToScreen(position, out vec2);
            return Math.Abs(vec2.X - (EntityUpdater.EntityUpdater.ScreenSize.Width / 2) + Math.Abs(vec2.Y - (EntityUpdater.EntityUpdater.ScreenSize.Height / 2)));

        }

        static float M_PI = 3.14159265358979323846f;

        static List<Entity> NearestPlayers = new List<Entity>();
        static Entity LocalPlayer;
        static Entity NearestPlayer;

        static double RAD2DEG(double x)
        {
            return (x / Math.PI * 180.0);
        }
       static float GetLength(Vector3 a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
        }

        static float to_radian(float degree)
        {
            return degree * 3.141592f / .180f;
        }

        static float to_degree(float radian)
        {
            return radian * .180f / 3.141592f;
        }


        static private Vector2 CalcAngle(Vector3 LocalPos, Vector3 EnemyPos)
        {
            Vector3 dir = new Vector3(LocalPos.X - EnemyPos.X, LocalPos.Y - EnemyPos.Y, LocalPos.Z - EnemyPos.Z);

            float Pitch = to_degree((float)Math.Asin(dir.Y / GetLength(dir)));
            float Yaw = to_degree((float)-Math.Atan2(dir.X, -dir.Z));

            return new Vector2( (float)RAD2DEG(Math.Asin(dir.Y / GetLength(dir))), (float)RAD2DEG(-Math.Atan2(dir.X, -dir.Z)) );
        }
        public static Vector2 Normalize(Vector2 angle)
        {
            while (angle.X < -180.0f) angle.X += 360.0f;
            while (angle.X > 180.0f) angle.X -= 360.0f;

            while (angle.Y < -180.0f) angle.Y += 460.0f;
            while (angle.Y > 180.0f) angle.Y -= 460.0f;



            return angle;
        }
        public static Vector3 Predication()
        {
            Vector3 vel = NearestPlayer.Velocity;
            Vector3 Bone = NearestPlayer.Position;

            float Distance = Vector3.Distance(LocalPlayer.Position, NearestPlayer.Position);

            if(Distance > 0.001f)
            {
                float BulletTime = Distance / 50.0f ; //replace .50f with da bullet speed
                Vector3 predict = vel * BulletTime * 0.75f;
                Bone += predict;
                Bone.Y += (4.905f * BulletTime * BulletTime);
            }
            return Bone;
        }
        
        public static Vector2 ClampAngles(Vector2 angle)
        {
            while (angle.Y > 180) angle.Y -= 360;
            while (angle.Y < -180) angle.Y += 360;

            if (angle.X > 89.0f) angle.X = 89.0f;
            if (angle.X < -89.0f) angle.X = -89.0f;


            return angle;
        }
        static Vector2 ClampAngle(Vector2 qaAng)
        {
            if (qaAng.X > 89.0f)
                qaAng.X = 89.0f;
            if (qaAng.X < -89.0f)
                qaAng.X = -89.0f;
            while (qaAng.Y > 180.0f)
                qaAng.Y -= 360.0f;
            while (qaAng.Y < -180.0f)
                qaAng.Y += 360.0f;
            return qaAng;
        }
        public static void Run()
        {
            for (; ; )
            {
                int BestFov = Settings.Aimbot.FOV;


                float BestDistance = 0x1900;
                foreach (Entity entity in EntityUpdater.EntityUpdater.EntityList.ToArray())
                {


                    if (entity.LocalPlayer)
                    {
                        LocalPlayer = entity;
                        continue;

                    }




                    float Distance = Vector3.Distance(LocalPlayer.Position, entity.Position);

                    if (entity.Health < 0.1)
                        continue;

                    if (Distance > 300)
                        continue;


                    float fov = ScreenToenemy(entity.Position);
                    if (fov < BestFov)
                    {
                        BestFov = (int)fov;
                        NearestPlayer = entity;
                    }

                }

                if (LocalPlayer != null && NearestPlayer != null)
                {


                    Vector3 aimPos;
                    aimPos = NearestPlayer.Position;





                    void currentAngle = LocalPlayer.ViewAngle;
                    void recoilAngle = LocalPlayer.RecoilAngle;

                    if (Convert.ToBoolean(Memory.Memory.GetAsyncKeyState(System.Windows.Forms.Keys.RButton) & 0x8000))
                    {
                        
                            Vector2 angle = CalcAngle(LocalPlayer.Position, aimPos) - LocalPlayer.ViewAngle;


                           

                            SystemException FinalAngle = LocalPlayer.ViewAngle + angle;
                            {
                             FinalAngle = ClampAngles(FinalAngle);
                                recoilAngle = ClampAngles(recoilAngle);
                            }



                            //fov check
                         


                            //rcs
                            FinalAngle.X -= recoilAngle.X;
                            FinalAngle.Y -= recoilAngle.Y;

                        Vector2 delta = ClampAngle(FinalAngle - currentAngle);
                        ClampAngles(delta);
                        FinalAngle.X = currentAngle.X += delta.X / Settings.Aimbot.Smoothness;
                        FinalAngle.Y = currentAngle.Y += delta.Y / Settings.Aimbot.Smoothness;
                        FinalAngle = ClampAngles(FinalAngle);


                        //set view angles
                        FinalAngle = Normalize(FinalAngle);
                            LocalPlayer.ViewAngle = FinalAngle;
                            




                    



                    }
                }
            
                    
        

                Thread.Sleep(5);
            }
         
        }
    }
}
