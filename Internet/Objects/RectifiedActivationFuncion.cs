namespace AI_Lab4.Objects
{
    
    public class RectifiedActivationFuncion : IActivationFunction
    {
        public double calculateOutput(double input)
        {
            return System.Math.Max(0, input);
        }
    }
}