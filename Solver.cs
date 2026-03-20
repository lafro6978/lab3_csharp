using System;

namespace wpflab3
{
    public class SolverResult
    {
        public double[] XValues { get; set; }
        public double[] YValues { get; set; }
        public string Title { get; set; }
    }

    public class Solver
    {
        public SolverResult Solve(InputData data)
        {
            int pointsCount = 100;
            var res = new SolverResult
            {
                XValues = new double[pointsCount],
                YValues = new double[pointsCount],
                Title = "Затухающие колебания"
            };

            for (int i = 0; i < pointsCount; i++)
            {
                res.XValues[i] = i * 0.1;
                res.YValues[i] = Math.Exp(-0.1 * i) * Math.Cos(data.Stiffness * i * 0.1);
            }
            return res;
        }
    }
}