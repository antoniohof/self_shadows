/*
 * LoggingSystem.cs
 *
 * Project: Log2CSV - Simple Logging System for Unity applications
 *
 * Supported Unity version: 5.4.1f1 Personal (tested)
 *
 * Author: Nico Reski
 * Web: http://reski.nicoversity.com
 * Twitter: @nicoversity
 */

using UnityEngine;
using System.Collections;
using System.IO;

public class LoggingSystem : MonoBehaviour {

	#region FIELDS

	// static log file names and formatters
	private static string LOGFILE_DIRECTORY = "log2csv_logfiles";
	private static string LOGFILE_NAME_BASE = "_log_file.csv";
	private static string LOGFILE_NAME_TIME_FORMAT = "yyyy-MM-dd_HH-mm-ss";	// prefix of the logfile, created when application starts (year - month - day - hour - minute - second)

	// logfile reference of the current session
	private string logFile;

	// bool representing whether the logging system should be used or not (set in the Unity Inspector)
	public bool activeLogging;


	public ExperienceController experienceReference;


	#endregion



	#region START_UPDATE

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Awake () {

        if (this.activeLogging)
		{
			// check if directory exists (and create it if not)
			string dir = LOGFILE_DIRECTORY
				+ "/"
				+ experienceReference.subjectID.ToString();

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

			// create file for this session using time prefix based on standard UTC time
			this.logFile = dir + "/"
				+ System.DateTime.UtcNow.ToString(LOGFILE_NAME_TIME_FORMAT)
				//+ System.DateTime.UtcNow.AddHours(2.0).ToString(LOGFILE_NAME_TIME_FORMAT)	// manually adjust time zone, e.g. + 2 UTC hours for summer time in location Stockholm/Sweden
				+ LOGFILE_NAME_BASE;
			File.Create(this.logFile);

			if(File.Exists(this.logFile)) Debug.Log("[LoggingSystem] LogFile created at " + this.logFile);
			else Debug.LogError("[LoggingSystem] Error creating LogFile");
		}

    }

    private void Start()
    {

        writeMessageWithTimestampToLog("Start Study - Logging, ID: " + experienceReference.subjectID);
        string timeNow = System.DateTime.UtcNow.ToString();
        writeMessageWithTimestampToLog("Initial time :" + timeNow);
    }

    #endregion



    #region WRITE_TO_LOG

    /// <summary>
    /// Writes the message to the log file on disk.
    /// </summary>
    /// <param name="message">string representing the message to be written.</param>
    public void writeMessageToLog(string message)
	{
		if(this.activeLogging)
		{
			if(File.Exists(this.logFile))
			{
				TextWriter tw = new StreamWriter(this.logFile, true);
				tw.WriteLine(message);
				tw.Dispose(); 
			}
		}
	}

	/// <summary>
	/// Writes the message including timestamp to the log file on disk.
	/// </summary>
	/// <param name="message">string representing the message to be written.</param>
	public void writeMessageWithTimestampToLog(string message)
	{
		writeMessageToLog(experienceReference.timestamp.ToString() + ";" + message);
	}


	#endregion
}