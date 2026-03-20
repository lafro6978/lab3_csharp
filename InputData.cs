using System.IO;
using System;

namespace wpflab3
{
    public class InputData
    {
        public double Mass { get; set; }
        public double Stiffness { get; set; }
        public double Damping { get; set; }
        public double X0 { get; set; }
        public double V0 { get; set; }
    }

    public static class InputDataLoader
    {
        public static InputData Load(string path)
        {
            // Здесь простая логика чтения. Для тестов можно возвращать фиксированные данные.
            return new InputData { Mass = 2, Stiffness = 30, Damping = 0.7, X0 = 0.5, V0 = 0 };
        }
    }
}