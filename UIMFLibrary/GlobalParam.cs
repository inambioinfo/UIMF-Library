﻿using System;

namespace UIMFLibrary
{
    #region Global Parameter Enum

    /// <summary>
    /// Known global parameters
    /// </summary>
    public enum GlobalParamKeyType
    {
        /// <summary>
        /// Unknown Global Parameter key
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Key: Instrument Name
        /// </summary>
        InstrumentName = 1,

        /// <summary>
        /// Key: Date Data collection started
        /// </summary>
        DateStarted = 2,

        /// <summary>
        /// Key: Number of frames
        /// </summary>
        NumFrames = 3,

        /// <summary>
        /// Key: Time offset
        /// </summary>
        TimeOffset = 4,

        /// <summary>
        /// Key: Bin width
        /// </summary>
        BinWidth = 5,               // Tof-bin size (in nanosecods) or ppm bin size (in parts-per-million)

        /// <summary>
        /// Key: Bins
        /// </summary>
        Bins = 6,

        /// <summary>
        /// Key: TOF Correction Time
        /// </summary>
        TOFCorrectionTime = 7,

        /// <summary>
        /// Key: TOF Intensity type
        /// </summary>
        TOFIntensityType = 8,

        /// <summary>
        /// Key: Dataset type
        /// </summary>
        DatasetType = 9,

        /// <summary>
        /// Key: Prescan TOF Pulses
        /// </summary>
        PrescanTOFPulses = 10,

        /// <summary>
        /// Key: Prescan Accumulations
        /// </summary>
        PrescanAccumulations = 11,

        /// <summary>
        /// Key: Prescan TIC Threshold
        /// </summary>
        PrescanTICThreshold = 12,

        /// <summary>
        /// Key: Prescan Continuous
        /// </summary>
        PrescanContinuous = 13,

        /// <summary>
        /// Key: Prescan Profile
        /// </summary>
        PrescanProfile = 14,

        /// <summary>
        /// Key: Instrument Class
        /// </summary>
        InstrumentClass = 15,       // 0 for TOF; 1 for ppm bin-based

        /// <summary>
        /// Key: PPM Bin Based Start m/z
        /// </summary>
        PpmBinBasedStartMz = 16,    // Only used when InstrumentClass is 1 (ppm bin-based)

        /// <summary>
        /// Key: PPM Bin Base End m/z
        /// </summary>
        PpmBinBasedEndMz = 17       // Only used when InstrumentClass is 1 (ppm bin-based)
    }

    /// <summary>
    /// Instrument Class types
    /// </summary>
    public enum InstrumentClassType
    {
        /// <summary>
        /// TOF-based instrument
        /// </summary>
        TOF = 0,

        /// <summary>
        /// PPM bin based instrument
        /// </summary>
        PpmBinBased = 1
    }

    #endregion

    /// <summary>
    /// Global parameters
    /// </summary>
    public class GlobalParam
    {

        /// <summary>
        /// Parameter Type
        /// </summary>
        public GlobalParamKeyType ParamType { get; private set; }

        /// <summary>
        /// Parameter Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// .NET data type
        /// </summary>
        public Type DataType { get; private set; }

        /// <summary>
        /// Parameter Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Parameter value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paramType">Frame parameter definition</param>
        /// <param name="value">Parameter value</param>
        public GlobalParam(GlobalParamKeyType paramType, string value)
        {
            InitializeByType(paramType);
            Value = value;
        }

        /// <summary>
        /// Customized ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Value == null)
                return ParamType + " (" + DataType + ")";

            return Value;
        }

        /// <summary>
        /// Initialize this global parameter using the param type enum value
        /// </summary>
        /// <param name="paramType">Param key type enum</param>
        private void InitializeByType(GlobalParamKeyType paramType)
        {
            ParamType = paramType;

            switch (paramType)
            {

                case GlobalParamKeyType.InstrumentName:
                    InitializeByType("InstrumentName", typeof(string), "Instrument name");
                    break;

                case GlobalParamKeyType.DateStarted:
                    // Format has traditionally been M/d/yyyy hh:mm:ss tt
                    // For example, 6/4/2014 12:56:44 PM
                    InitializeByType("DateStarted", typeof(string), "Time that the data acquisition started");
                    break;

                case GlobalParamKeyType.NumFrames:
                    InitializeByType("NumFrames", typeof(int), "Number of frames in the dataset");
                    break;

                case GlobalParamKeyType.TimeOffset:
                    InitializeByType("TimeOffset", typeof(int), "Time offset from 0 (in nanoseconds). All bin numbers must be offset by this amount");
                    break;

                case GlobalParamKeyType.BinWidth:
                    InitializeByType("BinWidth", typeof(double), "Width of TOF bins (in ns)");
                    break;

                case GlobalParamKeyType.Bins:
                    InitializeByType("Bins", typeof(int), "Total number of TOF bins in frame");
                    break;

                case GlobalParamKeyType.TOFCorrectionTime:
                    InitializeByType("TOFCorrectionTime", typeof(float), "TOF correction time");
                    break;

                case GlobalParamKeyType.TOFIntensityType:
                    InitializeByType("TOFIntensityType", typeof(string), "Data type of intensity in each TOF record (ADC is int, TDC is short, FOLDED is float)");
                    break;

                case GlobalParamKeyType.DatasetType:
                    InitializeByType("DatasetType", typeof(string), "Type of dataset (HMS, HMSn, or HMS-HMSn)");
                    break;

                case GlobalParamKeyType.PrescanTOFPulses:
                    InitializeByType("PrescanTOFPulses", typeof(string), "Prescan TOF pulses");
                    break;

                case GlobalParamKeyType.PrescanAccumulations:
                    InitializeByType("PrescanAccumulations", typeof(string), "Number of prescan accumulations");
                    break;

                case GlobalParamKeyType.PrescanTICThreshold:
                    InitializeByType("PrescanTICThreshold", typeof(string), "Prescan TIC threshold");
                    break;

                case GlobalParamKeyType.PrescanContinuous:
                    InitializeByType("PrescanContinuous", typeof(int), "Prescan Continuous flag (0 is false, 1 is true)");
                    break;

                case GlobalParamKeyType.PrescanProfile:
                    InitializeByType("PrescanProfile", typeof(string), "Profile used when PrescanContinuous is 1");
                    break;

                case GlobalParamKeyType.InstrumentClass:
                    InitializeByType("InstrumentClass", typeof(int), "Instrument class (0 for TOF, 1 for ppm bin-based)");
                    break;

                case GlobalParamKeyType.PpmBinBasedStartMz:
                    InitializeByType("PpmBinBasedStartMz", typeof(float), "Starting m/z value for ppm bin-based mode");
                    break;

                case GlobalParamKeyType.PpmBinBasedEndMz:
                    InitializeByType("PpmBinBasedEndMz", typeof(float), "Ending m/z value for ppm bin-based mode");
                    break;

                case GlobalParamKeyType.Unknown:
                    throw new ArgumentOutOfRangeException(nameof(paramType), "Cannot initialiaze a global parameter of type Unknown: " + (int)paramType);

                default:
                    throw new ArgumentOutOfRangeException(nameof(paramType), "Unrecognized global param enum for paramType: " + (int)paramType);
            }

        }

        private void InitializeByType(string name, Type dataType, string description)
        {
            Name = name;
            DataType = dataType;
            Description = description;
        }

    }
}
