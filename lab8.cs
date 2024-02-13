using System.Collections;

namespace ooap8
{
    public abstract class MapComponent
    {
        protected readonly int x;
        protected readonly int y;
        public MapComponent(string Name, int x, int y)
        {
            this.Name = Name;
            this.x = x;
            this.y = y;
        }
        public string Name { get; set; }
        public abstract void Draw();
        public abstract MapComponent FindChild(string Name);
    }

    public class MapComposite : MapComponent
    {
        public List<MapComponent> components { get; set; }
        public MapComposite(string Name, int x, int y) : base(Name, x, y)
        {
            components = new List<MapComponent>();
        }

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

        public void AddComponent(MapComponent component)
        {
            components.Add(component);
        }

        public void RemoveComponent(MapComponent component)
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
    }

    public class BreadthIterator
    {
        private Queue<MapComponent> queue = new Queue<MapComponent>();

        public BreadthIterator(MapComponent root)
        {
            queue.Enqueue(root);
        }

        public MapComponent GetNext()
        {
            MapComponent current = queue.Dequeue();

            if (current is MapComposite composite)
            {
                foreach (var component in composite.components)
                {
                    queue.Enqueue(component);
                }
            }

            return current;
        }

        public bool HasNext()
        {
            return queue.Count > 0;
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

            Console.WriteLine();
            var iterator = new BreadthIterator(ukraine);

            while (iterator.HasNext())
            {
                var component = iterator.GetNext();
                Console.WriteLine($"Component {component.Name}");
            }
        }
    }
}
