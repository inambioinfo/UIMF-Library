﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Speed tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UIMFLibrary.UnitTests
{
	using System;
	using System.Diagnostics;

	using NUnit.Framework;

	/// <summary>
	/// The speed tests.
	/// </summary>
    [TestFixture]
	public class SpeedTests
	{
		#region Fields

	    /// <summary>
	    /// Standard non-multiplexed file
	    /// </summary>
	    private const string uimfStandardFile1 = @"\\proto-2\UnitTest_Files\DeconTools_TestFiles\UIMF\Sarc_MS2_90_6Apr11_Cheetah_11-02-19.uimf";

	    #endregion

		#region Public Methods and Operators


        public static void PrintMethodName(System.Reflection.MethodBase methodInfo)
        {
            // Call with PrintMethodName(System.Reflection.MethodBase.GetCurrentMethod());

            var nameSpace = "?";
            var className = "?";

            if (methodInfo.ReflectedType != null)
            {
                nameSpace = methodInfo.ReflectedType.Namespace;
                className = methodInfo.ReflectedType.Name;
            }

            var methodDescriptor = nameSpace + ".";

            if (nameSpace.EndsWith("." + className))
            {
                methodDescriptor += methodInfo.Name;
            }
            else
            {
                methodDescriptor += className + "." + methodInfo.Name;
            }

            Console.WriteLine("\n\n===== " + methodDescriptor + " =====");

        }

		/// <summary>
		/// Summed mass spectrum speed tests.
		/// </summary>
		[Test]
		public void GetSummedMassspectrumSpeedTests()
		{
            PrintMethodName(System.Reflection.MethodBase.GetCurrentMethod());

            int numIterations = 100;

			// int numFramesToSum = 1;
			int numIMSScansToSum = 7;

			using (DataReader dr = new DataReader(uimfStandardFile1))
			{
				GlobalParameters gp = dr.GetGlobalParameters();

				int frameStart = 500;
				int frameStop = frameStart + numIterations;
				int scanStart = 250;
				int scanStop = scanStart + numIMSScansToSum - 1;

				Stopwatch sw = new Stopwatch();
				sw.Start();
				for (int frame = frameStart; frame < frameStop; frame++)
				{
					int[] intensities = null;
					double[] mzValues = null;

					int nonZeros = dr.GetSpectrum(
						frame, 
						frame, 
						DataReader.FrameType.MS1, 
						scanStart, 
						scanStop, 
						out mzValues, 
						out intensities);
				}

				sw.Stop();

				Console.WriteLine("Total time to read " + numIterations + " scans = " + sw.ElapsedMilliseconds);
				Console.WriteLine("Average time (milliseconds) = " + (double)sw.ElapsedMilliseconds / (double)numIterations);
			}
		}

		/// <summary>
		/// Single summed mass spectrum test 1.
		/// </summary>
		[Test]
		public void getSingleSummedMassSpectrumTest1()
		{
            PrintMethodName(System.Reflection.MethodBase.GetCurrentMethod());

			DataReader dr = new DataReader(uimfStandardFile1);
			GlobalParameters gp = dr.GetGlobalParameters();

			int[] intensities = new int[gp.Bins];
			double[] mzValues = new double[gp.Bins];

			// int startFrame = 500;
			// int stopFrame = 502;
			// int startScan = 250;
			// int stopScan = 256;

			// int nonZeros = dr.SumScansNonCached(mzValues, intensities, 0, startFrame, stopFrame, startScan, stopScan);

			// TestUtilities.displayRawMassSpectrum(mzValues, intensities);
		}

		#endregion
	}
}