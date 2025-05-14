using System;

public interface ISmartDevice
{
    string Name { get; set; }
    string Location { get; set; }
    bool IsOn { get; set; }

    void TurnOn();
    void TurnOff();
    void Configure();
}

public abstract class SmartDevice : ISmartDevice
{
    public string Name { get; set; }
    public string Location { get; set; }
    public bool IsOn { get; set; }

    public SmartDevice(string name, string location)
    {
        Name = name;
        Location = location;
        IsOn = false;
    }

    public virtual void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name} in {Location} is now ON.");
    }

    public virtual void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name} in {Location} is now OFF.");
    }

    public abstract void Configure();
}

public class SmartLight : SmartDevice
{
    public int Brightness { get; set; }

    public SmartLight(string name, string location, int brightness)
        : base(name, location)
    {
        Brightness = brightness;
    }

    public override void Configure()
    {
        Console.Write($"Set brightness for {Name}: ");
        if (int.TryParse(Console.ReadLine(), out int newBrightness))
        {
            Brightness = newBrightness;
            Console.WriteLine($"Brightness set to {Brightness}%");
        }
        else
        {
            Console.WriteLine("Invalid input. Brightness not changed.");
        }
    }
}

public class SmartAirConditioner : SmartDevice
{
    public int Temperature { get; set; }

    public SmartAirConditioner(string name, string location, int temperature)
        : base(name, location)
    {
        Temperature = temperature;
    }

    public override void Configure()
    {
        Console.Write($"Set temperature for {Name}: ");
        if (int.TryParse(Console.ReadLine(), out int newTemperature))
        {
            Temperature = newTemperature;
            Console.WriteLine($"Temperature set to {Temperature}°C");
        }
        else
        {
            Console.WriteLine("Invalid input. Temperature not changed.");
        }
    }
}

public class SmartHomeControlPanel
{
    private List<ISmartDevice> _devices = new List<ISmartDevice>();

    public void AddDevice(ISmartDevice device)
    {
        _devices.Add(device);
        Console.WriteLine($"Added {device.Name} to {device.Location}");
    }

    public void ListDevices()
    {
        Console.WriteLine("\n--- Smart Devices ---");
        for (int i = 0; i < _devices.Count; i++)
        {
            var device = _devices[i];
            Console.WriteLine($"{i + 1}. {device.Name} in {device.Location} - Status: {(device.IsOn ? "ON" : "OFF")}");
        }
    }

    public void TurnOnAll()
    {
        Console.WriteLine("\nTurning on all devices...");
        foreach (var device in _devices)
        {
            device.TurnOn();
        }
    }

    public void TurnOffAll()
    {
        Console.WriteLine("\nTurning off all devices...");
        foreach (var device in _devices)
        {
            device.TurnOff();
        }
    }

    public void ConfigureDevice()
    {
        ListDevices();
        Console.Write("Select device number to configure: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _devices.Count)
        {
            _devices[choice - 1].Configure();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
}

class Program
{
    static void Main()
    {
        SmartHomeControlPanel panel = new SmartHomeControlPanel();

        while (true)
        {
            Console.WriteLine("\n--- Smart Home Control Panel ---");
            Console.WriteLine("1. Add Smart Light");
            Console.WriteLine("2. Add Smart Air Conditioner");
            Console.WriteLine("3. List Devices");
            Console.WriteLine("4. Turn On All Devices");
            Console.WriteLine("5. Turn Off All Devices");
            Console.WriteLine("6. Configure Device");
            Console.WriteLine("7. Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter light name: ");
                    string lightName = Console.ReadLine();
                    Console.Write("Enter location: ");
                    string lightLocation = Console.ReadLine();
                    Console.Write("Enter brightness (0-100): ");
                    if (int.TryParse(Console.ReadLine(), out int brightness))
                    {
                        panel.AddDevice(new SmartLight(lightName, lightLocation, brightness));
                    }
                    else
                    {
                        Console.WriteLine("Invalid brightness value.");
                    }
                    break;

                case "2":
                    Console.Write("Enter air conditioner name: ");
                    string acName = Console.ReadLine();
                    Console.Write("Enter location: ");
                    string acLocation = Console.ReadLine();
                    Console.Write("Enter temperature: ");
                    if (int.TryParse(Console.ReadLine(), out int temp))
                    {
                        panel.AddDevice(new SmartAirConditioner(acName, acLocation, temp));
                    }
                    else
                    {
                        Console.WriteLine("Invalid temperature value.");
                    }
                    break;

                case "3":
                    panel.ListDevices();
                    break;

                case "4":
                    panel.TurnOnAll();
                    break;

                case "5":
                    panel.TurnOffAll();
                    break;

                case "6":
                    panel.ConfigureDevice();
                    break;

                case "7":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}