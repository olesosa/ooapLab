namespace ooap5
{
    public abstract class WeaponBase
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public int Year { get; set; }
        public double Range { get; set; }
        public double Power { get; set; }
        public double Weight { get; set; }
    }

    public class Pistol : WeaponBase
    { }
    public class Rifle : WeaponBase
    { }
    public class AutoRifle : WeaponBase
    { }

    public abstract class AccessoryBase
    {
        public double Cost { get; set; }
        public double Weight { get; set; }
    }

    public class Scope : AccessoryBase
    { }
    public class NightVisionDevices : AccessoryBase
    { }
    public class Silencer : AccessoryBase
    { }

    public interface IWeaponLogic
    {
        double GetWeaponSetCost();
        double GetMexWeaponCost();
    }

    public class WeaponFacade : IWeaponLogic
    {
        public WeaponBase Weapon { get; set; }
        public AccessoryBase Accessory { get; set; }
        public List<WeaponBase> Weapons { get; set; }

        public double GetWeaponSetCost()
        {
            double totalCost = 0;

            totalCost += Weapon.Cost;
            totalCost += Accessory.Cost;

            if (Weapon.Year <= 1970)
            {
                totalCost *= 0.8;
            }

            if (Weapon.Weight + Accessory.Weight > 15)
            {
                throw new Exception("Weapon weight is too high");
            }

            return totalCost;
        }

        public double GetMexWeaponCost()
        {
            double maxCost = Weapons.MaxBy(x => x.Cost).Cost;

            return maxCost;
        }
    }

    public class WeaponComponent : WeaponBase, IWeaponLogic
    {
        public List<WeaponBase> Weapons { get; set; }
        public double GetMexWeaponCost()
        {
            double totalCost = 0;

            totalCost += Cost;

            if (Year <= 1970)
            {
                totalCost *= 0.8;
            }

            if (Weight > 15)
            {
                throw new Exception("Weapon weight is too high");
            }

            return totalCost;
        }

        public double GetWeaponSetCost()
        {
            double maxCost = Weapons.MaxBy(x => x.Cost).Cost;

            return maxCost;
        }
    }

    public class WeaponDecorator : WeaponComponent, IWeaponLogic
    {
        public AccessoryBase Accessory { get; set; }
        public double GetMexWeaponCost()
        {
            double totalCost = 0;

            totalCost += Cost;
            totalCost += Accessory.Cost;

            if (Year <= 1970)
            {
                totalCost *= 0.8;
            }

            if (Weight + Accessory.Weight > 15)
            {
                throw new Exception("Weapon weight is too high");
            }

            return totalCost;
        }

        public double GetWeaponSetCost()
        {
            double maxCost = Weapons.MaxBy(x => x.Cost).Cost;

            return maxCost;
        }
    }

    public class Client
    {
        public void PrintWeaponSetCost(IWeaponLogic weapon)
            => Console.WriteLine($"Weapon Set Cost is {weapon.GetWeaponSetCost()}");

        public void PrintMaxWeaponSetCost(IWeaponLogic weapon)
            => Console.WriteLine($"Mex Weapon Cost is {weapon.GetMexWeaponCost()}");
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Pistol pistol = new Pistol() { Cost = 50, Year = 2005, Weight = 5 };
            Scope scope = new Scope() { Cost = 20, Weight = 3 };
            WeaponFacade weaponFacade = new WeaponFacade() { Weapon = pistol, Accessory = scope };
            Client client = new Client();

            client.PrintWeaponSetCost(weaponFacade);
        }
    }
}
