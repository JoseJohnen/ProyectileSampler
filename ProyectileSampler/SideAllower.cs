using System.Collections.Generic;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System.Linq;
using System;

namespace ProyectileSampler
{
    public class SideAllower : SyncScript
    {
        // Declared public member fields and properties will show in the game studio
        public Prefab missile;
        public static List<Pair<DateTime,Entity>> L_Proyectile = new List<Pair<DateTime,Entity>>();

        public override void Start()
        {
            // Initialization of the script.
        }

        public override void Update()
        {
            if (Input.HasKeyboard)
            {
                if (Input.IsKeyDown(Keys.O))
                {
                    this.Entity.Transform.Position += new Vector3(-0.5f, 0, 0);
                }
                else if (Input.IsKeyDown(Keys.P))
                {
                    this.Entity.Transform.Position += new Vector3(0.5f, 0, 0);
                }
            }

            if (Input.HasMouse)
            {
                if (Input.IsMouseButtonDown(Stride.Input.MouseButton.Right))
                {
                    if (missile != null)
                    {
                        List<Entity> l_entitys = missile.Instantiate();
                        Entity entity = l_entitys[0];
                        entity.Transform.Position += (this.Entity.Transform.Position + new Vector3(0, 0, -0.5f));
                        L_Proyectile.Add(new Pair<DateTime, Entity>() { Item1 = DateTime.Now, Item2 = entity });
                        // Add the bullet to the scene
                        SceneSystem.SceneInstance.RootScene.Entities.Add(entity);
                    }
                }
            }

            if (L_Proyectile.Count > 0)
            {
                foreach (Pair<DateTime, Entity> item in L_Proyectile.Reverse<Pair<DateTime, Entity>>())
                {
                    if(DateTime.Now - item.Item1 > new TimeSpan(0,0,0,0,50))
                    {
                        item.Item2.Transform.Position += new Vector3(0,0,-0.5f);
                        item.Item1 = DateTime.Now;
                    }
                }
            }
        }

    }
}
