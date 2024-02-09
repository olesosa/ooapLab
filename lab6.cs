namespace ooap6
{
    public abstract class MapComponent
    {
        protected int x;
        protected int y;
        public MapComponent(string Name, int x, int y)
        {
            this.Name = Name;
            this.x = x;
            this.y = y;
        }
        public string Name { get; set; }
        public abstract void Draw();
        public abstract MapComponent FindChild(string Name);
        public abstract void AddComponent(MapComponent component);
        public abstract void RemoveComponent(MapComponent component);
    }

    public class MapComposite : MapComponent
    {
        private List<MapComponent> components = new List<MapComponent>();
        public MapComposite(string Name, int x, int y) : base(Name, x, y) { }

        public override void Draw()
        {
            Console.WriteLine($"Composite {Name}: {x} {y}");

            foreach (var component in components)
            {
                component.Draw();
            }
        }

        public override MapComponent FindChild(string Name)
        {
            foreach (var component in components)
            {
                var found = component.FindChild(Name);
                if (found != null)
                {
                    return found;
                }
                else
                {
                    throw new Exception("Not Found!");
                }
            }

            return null;
        }

        public override void AddComponent(MapComponent component)
        {
            components.Add(component);
        }

        public override void RemoveComponent(MapComponent component)
        {
            components.Remove(component);
        }
    }

    public class MapPart : MapComponent
    {
        public MapPart(string Name, int x, int y) : base(Name, x, y) { }
        public override void Draw()
        {
            Console.WriteLine($"City {Name}: {x} {y}");
        }

        public override MapComponent FindChild(string Name)
        {
            if (this.Name == Name)
            {
                return this;
            }
            else
            {
                throw new Exception("Not Found!");
            }
        }
        public override void AddComponent(MapComponent component)
        {
            throw new NotImplementedException();
        }

        public override void RemoveComponent(MapComponent component)
        {
            throw new NotImplementedException();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            MapPart lviv = new MapPart("Lviv", 1, 2);
            MapPart lSide = new MapPart("Left Side", 1, 2);
            MapPart rSide = new MapPart("Right Side", 1, 2);
            MapComposite kyiv = new MapComposite("Kyiv", 3, 4);
            MapComposite ukraine = new MapComposite("Ukraine", 5, 6);

            kyiv.AddComponent(lSide);
            kyiv.AddComponent(rSide);

            ukraine.AddComponent(lviv);
            ukraine.AddComponent(kyiv);

            ukraine.Draw();

            Console.WriteLine();
            var city = ukraine.FindChild("Lviv");
            city.Draw();
        }
    }
}
