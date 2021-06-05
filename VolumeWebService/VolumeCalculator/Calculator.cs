using System;

namespace VolumeWebService.VolumeCalculator
{
    public class Calculator
    {
        public double CalculateVolumeCylinder(decimal height, decimal radius)
        {
            return (Math.PI * (Math.Pow((double) radius, 2)) * (double) height);
        }

        public double CalculateVolumeCone(decimal height, decimal radius)
        {
            return ((Math.PI * (Math.Pow((double) radius, 2)) * (double) height) * 0.3333);
        }
    }
}