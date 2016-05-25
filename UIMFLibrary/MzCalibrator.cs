﻿using System;

namespace UIMFLibrary
{

    /// <summary>
    /// The m/z calibrator.
    /// </summary>
    /// <remarks>
    /// Calibrate TOF to m/z according to formula: mass = (k * (t-t0))^2
    /// </remarks>
    public class MzCalibrator
    {
        #region Fields

        private double binWidth;

        private double TenthsOfNanoSecondsPerBin
        {
            get { return this.binWidth * 10; }
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MzCalibrator"/> class.
        /// </summary>
        /// <param name="k">
        /// k
        /// </param>
        /// <param name="t0">
        /// t0
        /// </param>
        /// <param name="binWidthNs">
        /// bin width, in nanoseconds
        /// </param>
        /// <remarks>
        /// mass = (k * (t-t0))^2
        /// </remarks>
        public MzCalibrator(double k, double t0, double binWidthNs = 1)
        {
            this.K = k;
            this.T0 = t0;
            this.binWidth = binWidthNs;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description
        {
            get
            {
                return "mz = (k*(t-t0))^2";
            }
        }

        /// <summary>
        /// Gets or sets the k.
        /// </summary>
        public double K { get; set; }

        /// <summary>
        /// Gets or sets the t 0.
        /// </summary>
        public double T0 { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Convert m/z to TOF value
        /// </summary>
        /// <param name="mz">
        /// mz
        /// </param>
        /// <returns>
        /// TOF value<see cref="int"/>.
        /// </returns>
        public virtual int MZtoTOF(double mz)
        {
            var r = Math.Sqrt(mz);
            return (int)Math.Round(((r / this.K) + this.T0));
        }

        /// <summary>
        /// Convert TOF value to m/z
        /// </summary>
        /// <param name="TOFValue">
        /// The tof value
        /// </param>
        /// <returns>
        /// m/z<see cref="double"/>.
        /// </returns>
        public virtual double TOFtoMZ(double TOFValue)
        {
            var r = this.K * (TOFValue - this.T0);
            return Math.Pow(r, 2);
        }

        /// <summary>
        /// Convert m/z to bin number
        /// </summary>
        /// <param name="mz">m/z</param>
        /// <returns>bin number</returns>
        public double MZtoBin(double mz)
        {
            // TODO: Add TOFCorrectionTime?
            return this.TOFtoBin(this.MZtoTOF(mz));
        }

        /// <summary>
        /// Convert bin to m/z
        /// </summary>
        /// <param name="bin">bin number</param>
        /// <returns>m/z</returns>
        public double BinToMZ(double bin)
        {
            return this.TOFtoMZ(this.BinToTOF(bin));
        }

        /// <summary>
        /// Convert from a bin number to a TOF value
        /// </summary>
        /// <param name="bin"></param>
        /// <returns></returns>
        public double BinToTOF(double bin)
        {
            return bin / this.binWidth * this.TenthsOfNanoSecondsPerBin;
        }

        /// <summary>
        /// Convert from a TOF value to bin number
        /// </summary>
        /// <param name="TOF"></param>
        /// <returns></returns>
        public double TOFtoBin(double TOF)
        {
            return TOF * this.binWidth / this.TenthsOfNanoSecondsPerBin;
        }

        #endregion
    }

    public class MzCalibratorFtms : MzCalibrator
    {
        public MzCalibratorFtms(double k, double t0, double binWidthNs = 1) : base(k, t0, binWidthNs)
        {
        }

        public override int MZtoTOF(double mz)
        {
            var r = Math.Sqrt(mz);
            var multipledR = ((r/this.K) + this.T0)*1e4;
            return (int) multipledR;
        }

        public override double TOFtoMZ(double TOFValue)
        {
            var r = this.K * (TOFValue - this.T0);
            r /= 1e4;
            return Math.Pow(r, 2);
        }
    }
}
